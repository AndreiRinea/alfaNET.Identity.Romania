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

using System.Runtime.InteropServices;
using alfaNET.Identity.Romania.Cnp;

namespace alfaNET.Identity.Romania.Tests.Cnp;

public class PersonalNumericCodeTest
{
    private readonly PersonalNumericCode _validCode1 = new PersonalNumericCode(1800101420010);
    private readonly PersonalNumericCode _validCode2 = new PersonalNumericCode(1810101420019);
    private readonly byte[] _code1ByteArray = [1, 8, 0, 0, 1, 0, 1, 4, 2, 0, 0, 1, 0];

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
    public void A_Valid_CNP_IsValidated()
    {
        var validationResult = _validCode1.Validate();
        Assert.Equal(ValidationError.None, validationResult);
    }

    [Theory]
    [InlineData(1800101420011,ValidationError.ChecksumError)]
    [InlineData(1800101990014,ValidationError.InvalidCounty)]
    public void Invalid_CNP_IsNotValidated(long cnp, ValidationError expectedError)
    {
        var actualError = new PersonalNumericCode(cnp).Validate();
        Assert.Equal(expectedError, actualError);
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
}