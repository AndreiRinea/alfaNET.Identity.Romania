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

public class CountyTest
{
    [Fact]
    public void ToString_Returns_Name()
    {
        var name = County.BucurestiSector2.ToString();
        Assert.Equal("București - Sector 2", name);
    }

    [Fact]
    public void GetHashCode_Returns_UniqueValue_ForAllCounties()
    {
        var hashCodes = new HashSet<int>();
        var countyCount = 0;
        foreach (var county in County.List())
        {
            var hashCode = county.GetHashCode();
            hashCodes.Add(hashCode);
            countyCount++;
        }
        Assert.Equal(countyCount, hashCodes.Count);
    }

    [Fact]
    public void OpNotEqual_ReturnsTrue_ForDifferentCounties()
    {
        var result = County.Cluj != County.Iasi;
        Assert.True(result);
    }

    [Fact]
    public void BetweenMinAndMaxCode_ThereAre_AllCounties()
    {
        var counties = new HashSet<County>();
        for (var i = County.MinCode; i <= County.MaxCode; i++)
        {
            var county = County.GetByCode(i);
            if (county == null) continue;
            counties.Add(county);
        }
        Assert.Equal(County.List().Count(),  counties.Count);
    }
}