// Copyright 2025 Andrei Rinea
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Text;

namespace alfaNET.Identity.Romania.Cnp;

/// <summary>
/// A Personal Numeric Code (CNP) class that enables validation and data extraction
/// </summary>
public class PersonalNumericCode : IEquatable<PersonalNumericCode>
{
    private struct PossibleDate
    {
        public readonly short Year;
        public readonly byte Month;
        public readonly byte Day;

        // ReSharper disable once ConvertToPrimaryConstructor
        public PossibleDate(short year, byte month, byte day)
        {
            Year = year;
            Month = month;
            Day = day;
        }
    }

    private static readonly byte[] ValidationConstant = [2, 7, 9, 1, 4, 6, 3, 5, 8, 2, 7, 9];
    private static readonly DateOnly MaxDateForSector7And8 = new(1979, 12, 19);

    #region State

    private readonly byte[] _digits;

    #endregion

    /// <summary>
    /// Constructs an instance from an Int64 value
    /// </summary>
    /// <param name="value">The CNP value. For example 1800101420010</param>
    /// <exception cref="ArgumentException">Value is not of 13 digits</exception>
    public PersonalNumericCode(long value)
    {
        if (value is < 1000000000000 or > 9999999999999)
        {
            throw new ArgumentException("invalid value " + value);
        }

        _digits = new byte[13];
        var i = 12;
        while (value > 0)
        {
            var digit = value % 10;
            _digits[i] = (byte)digit;
            value /= 10;
            i--;
        }
    }

    /// <summary>
    /// Constructs an instance from a byte array, each byte representing a digit.
    /// </summary>
    /// <param name="digits">Leftmost digit being array element 0.</param>
    /// <exception cref="ArgumentNullException">parameter 'digits' is null</exception>
    /// <exception cref="ArgumentOutOfRangeException">the length of the digits array is not 13</exception>
    /// <exception cref="ArgumentException">at least one of the digits has an invalid value (</exception>
    public PersonalNumericCode(byte[] digits)
    {
        ArgumentNullException.ThrowIfNull(digits);
        if (digits.Length != 13)
        {
            throw new ArgumentOutOfRangeException(nameof(digits), "value must be 13 digits");
        }

        for (var i = 0; i < 13; i++)
        {
            if (digits[i] > 9)
            {
                throw new ArgumentException("digit at index " + i + " has invalid value of " + digits[i]);
            }
        }

        _digits = (byte[])digits.Clone();
    }

    private byte ComputeChecksum()
    {
        var sum = 0;
        for (var i = 0; i < 12; i++)
        {
            sum += _digits[i] * ValidationConstant[i];
        }

        var controlDigit = sum % 11;
        controlDigit = controlDigit == 10 ? 1 : controlDigit;
        return (byte)controlDigit;
    }

    private byte GetCountyCode()
    {
        var countyFirstDigit = _digits[7];
        var countySecondDigit = _digits[8];
        var countyCode = (byte)(countyFirstDigit * 10 + countySecondDigit);
        return countyCode;
    }

    private short GetSequentialNumberData()
    {
        var sequentialNumberFirstDigit = _digits[9];
        var sequentialNumberSecondDigit = _digits[10];
        var sequentialNumberThirdDigit = _digits[11];
        return (short)(
            sequentialNumberFirstDigit * 100 +
            sequentialNumberSecondDigit * 10 +
            sequentialNumberThirdDigit);
    }

    private bool IsSexValid()
    {
        var sexDigit = _digits[0];
        return sexDigit is >= 1 and <= 8;
    }

    private PossibleDate GetDateComponents()
    {
        short year = 0;
        year += _digits[0] switch
        {
            1 or 2 => 1900,
            3 or 4 => 1800,
            5 or 6 => 2000,
            _ => 0
        };
        year += (short)(_digits[1] * 10);
        year += _digits[2];
        var month = (byte)(_digits[3] * 10 + _digits[4]);
        var day = (byte)(_digits[5] * 10 + _digits[6]);
        return new PossibleDate(year, month, day);
    }

    private static ValidationErrors ValidateDate(PossibleDate dateComponents)
    {
        var errors = ValidationErrors.None;
        var hasInvalidDateComponent = false;
        if (dateComponents.Month is < 1 or > 12)
        {
            errors |= ValidationErrors.InvalidMonth;
            hasInvalidDateComponent = true;
        }

        if (dateComponents.Day is < 1 or > 31)
        {
            errors |= ValidationErrors.InvalidDay;
            hasInvalidDateComponent = true;
        }

        if (!hasInvalidDateComponent &&
            dateComponents.Day > DateTime.DaysInMonth(dateComponents.Year, dateComponents.Month))
        {
            errors |= ValidationErrors.InvalidDate;
        }

        return errors;
    }

    /// <summary>
    /// Validates the actual Personal Numeric Code value
    /// </summary>
    /// <returns>A list of validation errors, presented as a flagged enum. Value 'None' means no errors.</returns>
    public ValidationErrors Validate()
    {
        var errors = ValidationErrors.None;

        if (!IsSexValid())
        {
            errors |= ValidationErrors.InvalidSexDigit;
        }

        var dateComponents = GetDateComponents();
        var dateErrors = ValidateDate(dateComponents);
        errors |= dateErrors;

        var countyCode = GetCountyCode();
        var county = County.GetByCode(countyCode);
        if (county == null)
        {
            errors |= ValidationErrors.InvalidCounty;
        }

        if (GetSequentialNumberData() == 0)
        {
            errors |= ValidationErrors.InvalidSequentialNumber;
        }

        if (dateErrors == ValidationErrors.None &&
            (county == County.BucurestiSector7 ||
             county == County.BucurestiSector8))
        {
            var date = new DateOnly(dateComponents.Year, dateComponents.Month, dateComponents.Day);
            if (date > MaxDateForSector7And8)
            {
                errors |= ValidationErrors.InvalidDateForCounty;
            }
        }

        var computedControlDigit = ComputeChecksum();
        var suppliedControlDigit = _digits[12];
        if (suppliedControlDigit != computedControlDigit)
        {
            errors |= ValidationErrors.ChecksumError;
        }

        return errors;
    }

    /// <summary>
    /// Get the actual list of digits of the Personal Numeric Code
    /// </summary>
    /// <returns>The list of digits as a byte array. The item at index 0 is the leftmost digit.</returns>
    public byte[] GetDigits()
    {
        return (byte[])_digits.Clone();
    }

    /// <summary>
    /// Returns the sex of the person
    /// </summary>
    /// <returns>Male or Female</returns>
    /// <exception cref="InvalidOperationException">The value of the Personal Numeric Code is invalid - at least at the
    /// sex digit level</exception>
    public Sex GetSex()
    {
        if (!IsSexValid())
        {
            throw new InvalidOperationException("PersonalNumericCode is invalid - at least the sex digit");
        }

        return _digits[0] % 2 == 1 ? Sex.Male : Sex.Female;
    }

    /// <summary>
    /// Gets the calendar date associated with the current Personal Numeric Code.
    /// </summary>
    /// <returns>A DateTime for the date of the Personal Numeric Code</returns>
    /// <exception cref="InvalidOperationException">The current Personal Numeric Code has not a valid date.</exception>
    public DateTime GetDate()
    {
        var dateComponents = GetDateComponents();
        var errors = ValidateDate(dateComponents);
        if (errors != ValidationErrors.None)
        {
            throw new InvalidOperationException("PersonalNumericCode is invalid: " + errors);
        }

        return new DateTime(dateComponents.Year, dateComponents.Month, dateComponents.Day);
    }

    /// <summary>
    /// Gets the county associated with the current Personal Numeric Code.
    /// See <see cref="alfaNET.Identity.Romania.Cnp.County"/>
    /// </summary>
    /// <returns>An instance of County associated with the current Personal Numeric Code</returns>
    /// <exception cref="InvalidOperationException">The current Personal Numeric Code has not a valid county
    /// code</exception>
    public County GetCounty()
    {
        var countyCode = GetCountyCode();
        var county = County.GetByCode(countyCode);
        if (county == null)
        {
            throw new InvalidOperationException("PersonalNumericCode has invalid county code: " + countyCode);
        }
        return county;
    }

    /// <summary>
    /// Gets the sequential number within the current Personal Numeric Code. This has valid values within 001 to 999.
    /// </summary>
    /// <returns>An Int16 containing the sequential number within the Personal Numeric Code</returns>
    /// <exception cref="InvalidOperationException">The current Personal Numeric Code has not a valid sequential
    /// number</exception>
    public short GetSequentialNumber()
    {
        var sequentialNumberData = GetSequentialNumberData();
        if (sequentialNumberData == 0)
        {
            throw new InvalidOperationException("PersonalNumericCode has invalid sequential number" +
                                                sequentialNumberData);
        }
        return sequentialNumberData;
    }

    /// <summary>
    /// Get the actual Personal Numeric Code as an Int64 value
    /// </summary>
    /// <returns>The integer representation of the Personal Numeric Code</returns>
    public long GetValueAsLong()
    {
        return _digits[00] * 1000000000000 +
               _digits[01] * 100000000000 +
               _digits[02] * 10000000000 +
               _digits[03] * 1000000000 +
               _digits[04] * 100000000 +
               _digits[05] * 10000000 +
               _digits[06] * 1000000 +
               _digits[07] * 100000 +
               _digits[08] * 10000 +
               _digits[09] * 1000 +
               _digits[10] * 100 +
               _digits[11] * 10 +
               _digits[12] * 1;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        var result = new StringBuilder(13, 13);
        for (var i = 0; i < 13; i++)
        {
            result.Append(_digits[i]);
        }

        return result.ToString();
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            for (var i = 0; i < 13; i++)
            {
                hash = hash * 31 + _digits[i];
            }

            return hash;
        }
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return Equals(obj as PersonalNumericCode);
    }

    /// <inheritdoc />
    public bool Equals(PersonalNumericCode? other)
    {
        if (ReferenceEquals(this, other)) return true;
        return other != null &&
               _digits.SequenceEqual(other._digits);
    }

    public static bool operator ==(PersonalNumericCode? left, PersonalNumericCode? right) => Equals(left, right);

    public static bool operator !=(PersonalNumericCode? left, PersonalNumericCode? right) => !Equals(left, right);
}