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

namespace alfaNET.Identity.Romania.Cnp;

/// <summary>
/// List of errors that can be detected while validating a Personal Numeric Code (CNP)
/// </summary>
[Flags]
public enum ValidationErrors
{
    /// <summary>
    /// Valid, no errors.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// The checksum digit does not match.
    /// </summary>
    ChecksumError = 1,
    
    /// <summary>
    /// The sex digit (the first digit) is invalid. It needs to be in the range of 1 to 8.
    /// </summary>
    InvalidSexDigit = 2,
    
    /// <summary>
    /// The month digits have an invalid value. They need to be within the range of 01 to 12.
    /// </summary>
    InvalidMonth = 4,
    
    /// <summary>
    /// The day digits have an invalid value. They need to be within the range of 01 to 31.
    /// </summary>
    InvalidDay = 8,
    
    /// <summary>
    /// The year/month/date combination is not valid. Possible cases: day value outside of possible values for month
    /// (e.g. 31st of April), day value outside of the month days for that year (February, 29th 2025) etc.
    /// </summary>
    InvalidDate = 16,
    
    /// <summary>
    /// The county code is not valid. See county codes at <see cref="alfaNET.Identity.Romania.Cnp.County"/>.
    /// </summary>
    InvalidCounty = 32,
    
    /// <summary>
    /// Sequential number is outside of allowed values (001..999)
    /// </summary>
    InvalidSequentialNumber = 64,
    
    /// <summary>
    /// The date is outside the allowed range for that particular county.
    /// e.g. Sector 7 and 8 of Bucharest have been valid only till December 19th of 1979
    /// </summary>
    InvalidDateForCounty = 128
}