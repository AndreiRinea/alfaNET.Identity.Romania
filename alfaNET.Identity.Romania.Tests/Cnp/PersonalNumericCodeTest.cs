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

using alfaNET.Identity.Romania.Cnp;

namespace alfaNET.Identity.Romania.Tests.Cnp;

public class PersonalNumericCodeTest
{
    private const long Code1 = 1800101420010;
    private const long Code2 = 1810101420019;
    private const long ValidCnpStartingWith3 = 3810326400011;
    private const long ValidCnpStartingWith4 = 4770521400018;
    private const long ValidCnpStartingWith5 = 5100521410011;
    private const long ValidCnpStartingWith6 = 6140913420018;
    private const long ValidCnpStartingWith7 = 7800101420011;
    private const long ValidCnpStartingWith8 = 8810101420011;

    private readonly byte[] _code1ByteArray = [1, 8, 0, 0, 1, 0, 1, 4, 2, 0, 0, 1, 0];

    private readonly PersonalNumericCode _validCode1 = new(Code1);
    private readonly PersonalNumericCode _validCode2 = new(Code2);

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(180010142001)]
    [InlineData(18001014200100)]
    public void LongInt_Constructor_Rejects_OutOfRange_Values(long value)
    {
        Assert.Throws<ArgumentException>(() => new PersonalNumericCode(value));
    }

    [Fact]
    public void LongInt_Constructor_InitiatesCorrectly()
    {
        var digits = _validCode1.GetDigits();
        Assert.Equal(_code1ByteArray, digits);
    }

    [Fact]
    public void ByteArrayConstructor_Rejects_NullArray()
    {
        Assert.Throws<ArgumentNullException>(() => new PersonalNumericCode(null!));
    }

    [Theory]
    [InlineData(new byte[] { })]
    [InlineData(new byte[] { 1, 1, 1, 1 })]
    [InlineData(new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 })]
    public void ByteArrayConstructor_Rejects_ArrayOfBadLength(byte[] digits)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PersonalNumericCode(digits));
    }

    [Fact]
    public void ByteArrayConstructor_Rejects_BadDigitsValue()
    {
        Assert.Throws<ArgumentException>(() => new PersonalNumericCode([1, 8, 0, 0, 1, 0, 1, 4, 2, 0, 10, 1, 0]));
    }

    [Fact]
    public void ByteArrayConstructor_ConstructsWell_ForGoodArray()
    {
        _ = new PersonalNumericCode(_code1ByteArray);
    }

    [Theory]
    [InlineData(Code1)]
    [InlineData(Code2)]
    [InlineData(ValidCnpStartingWith3)]
    [InlineData(ValidCnpStartingWith4)]
    [InlineData(ValidCnpStartingWith5)]
    [InlineData(ValidCnpStartingWith6)]
    [InlineData(ValidCnpStartingWith7)]
    [InlineData(ValidCnpStartingWith8)]
    public void Valid_CNPs_AreValidated(long cnpValue)
    {
        var personalNumericCode = new PersonalNumericCode(cnpValue);
        var errors = personalNumericCode.Validate();
        Assert.Equal(ValidationErrors.None, errors);
    }

    [Theory]
    [InlineData(1800101420011, ValidationErrors.ChecksumError)]
    [InlineData(1800101990014, ValidationErrors.InvalidCounty)]
    public void Invalid_CNP_IsNotValidated(long cnp, ValidationErrors expectedErrors)
    {
        var actualError = new PersonalNumericCode(cnp).Validate();
        Assert.Equal(expectedErrors, actualError);
    }

    [Fact]
    public void GetDigits_ReturnsCorrectValue()
    {
        var digits = _validCode1.GetDigits();
        Assert.Equal(_code1ByteArray, digits);
    }

    [Fact]
    public void ToString_ReturnsCorrectValue()
    {
        var str = _validCode1.ToString();
        Assert.Equal("1800101420010", str);
    }

    [Fact]
    public void GetHashCode_ReturnsCorrectValue()
    {
        var hashCode = _validCode1.GetHashCode();
        Assert.Equal(-68890393, hashCode);
    }

    [Fact]
    public void Equals_ReturnsTrue_ForSameInstance()
    {
        var eq = _validCode1.Equals(_validCode1);
        Assert.True(eq);
    }

    [Fact]
    public void Equals_ReturnsFalse_ForNull()
    {
        Assert.False(_validCode1.Equals(null));
    }

    [Fact]
    public void Equals_ReturnsTrue_ForDifferentInstance_WhichIsEqual()
    {
        var differentInstance = new PersonalNumericCode(1800101420010);
        Assert.True(_validCode1.Equals(differentInstance));
    }

    [Fact]
    public void Equals_ReturnsFalse_ForDifferentInstance_WhichIsNotEqual()
    {
        Assert.False(_validCode1.Equals(_validCode2));
    }

    [Fact]
    public void GetValueAsLong_ReturnsCorrectValue()
    {
        var value = _validCode1.GetValueAsLong();
        Assert.Equal(Code1, value);
    }

    [Fact]
    public void Equals_ReturnsTrue_ForDifferentInstances_WithSameValue()
    {
        var cnp1 = new PersonalNumericCode(1800101420010);
        object cnp2 = new PersonalNumericCode(1800101420010);
        var result = cnp1.Equals(cnp2);
        Assert.True(result);
    }

    [Fact]
    public void OpEquals_ReturnsTrue_ForDifferentInstances_WithSameValue()
    {
        var cnp1 = new PersonalNumericCode(1800101420010);
        var cnp2 = new PersonalNumericCode(1800101420010);
        var result = cnp1 == cnp2;
        Assert.True(result);
    }

    [Theory]
    [InlineData(1802001400014)]
    [InlineData(1801301400014)]
    [InlineData(1800001400012)]
    public void Validate_Rejects_BadMonth(long cnpValue)
    {
        var cnp = new PersonalNumericCode(cnpValue);
        var errors = cnp.Validate();
        Assert.Equal(ValidationErrors.InvalidMonth, errors);
    }
    
    [Theory]
    [InlineData(1800141420012)]
    [InlineData(1800132420011)]
    [InlineData(1800100420018)]
    public void Validate_Rejects_BadDay(long cnpValue)
    {
        var cnp = new PersonalNumericCode(cnpValue);
        var errors = cnp.Validate();
        Assert.Equal(ValidationErrors.InvalidDay, errors);
    }

    [Fact]
    public void Validate_Rejects_InvalidSequentialNumber()
    {
        var personalNumericCode = new PersonalNumericCode(1800101420002);
        var errors = personalNumericCode.Validate();
        Assert.Equal(ValidationErrors.InvalidSequentialNumber, errors);
    }

    [Fact]
    public void Validate_Rejects_MultipleErrors()
    {
        var personalNumericCode = new PersonalNumericCode(1800101990006);
        var errors = personalNumericCode.Validate();
        Assert.Equal(ValidationErrors.InvalidSequentialNumber | ValidationErrors.InvalidCounty, errors);
    }
}