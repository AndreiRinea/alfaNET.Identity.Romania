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

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable IdentifierTypo
// ReSharper disable ConvertToConstant.Global
namespace alfaNET.Identity.Romania.Cnp;

/// <summary>
/// Enumeration of all counties used in a Personal Numeric Code (CNP) of Romania
/// </summary>
public class County
{
    /// <summary>
    /// To prevent clustering we will be using Donald Knuth's constant to disperse the values across the Int32 space
    /// </summary>
    private const int KnuthMultiplier = unchecked((int)0x9E3779B1);

    /// <summary>
    /// The lowest valid county numerical ID
    /// </summary>
    public static readonly byte MinCode = 01;
    
    /// <summary>
    /// The highest valid county numerical ID
    /// </summary>
    public static readonly byte MaxCode = 70;
    
    private readonly byte _code;
    private readonly string _name;

    private County(byte code, string name)
    {
        _code = code;
        _name = name;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return _name;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        unchecked
        {
            // better spread of values across the Int32 space to prevent clustering in HashTables or similar structures
            return _code * KnuthMultiplier;
        }
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
        {
            return true;
        }
        if (obj is not County other)
        {
            return false;
        }
        return _code == other._code;
    }
    
    public static bool operator ==(County? left, County? right) => Equals(left, right);
    public static bool operator !=(County? left, County? right) => !Equals(left, right);
    
    /// <summary>
    /// An enumeration of all available counties
    /// </summary>
    /// <returns>An array presented as an IEnumerable</returns>
    public static IEnumerable<County> List() => [
        Alba,
        Arad,
        Arges,
        Bacau,
        Bihor,
        BistritaNasaud,
        Botosani,
        Brasov,
        Braila,
        Buzau,
        CarasSeverin,
        Cluj,
        Constanta,
        Covasna,
        Dambovita,
        Dolj,
        Galati,
        Gorj,
        Harghita,
        Hunedoara,
        Ialomita,
        Iasi,
        Ilfov,
        Maramures,
        Mehedinti,
        Mures,
        Neamt,
        Olt,
        Prahova,
        SatuMare,
        Salaj,
        Sibiu,
        Suceava,
        Teleorman,
        Timis,
        Tulcea,
        Vaslui,
        Valcea,
        Vrancea,
        Bucuresti,
        BucurestiSector1,
        BucurestiSector2,
        BucurestiSector3,
        BucurestiSector4,
        BucurestiSector5,
        BucurestiSector6,
        BucurestiSector7,
        BucurestiSector8,
        Calarasi,
        Giurgiu,
        CodUnic
     ];

    /// <summary>
    /// Obtains a county by the numerical ID
    /// </summary>
    /// <param name="code">the numerical ID of the county</param>
    /// <returns>The matching county or null if no counties match the ID</returns>
    public static County? GetByCode(byte code)
    {
        return code switch
        {
            01 => Alba,
            02 => Arad,
            03 => Arges,
            04 => Bacau,
            05 => Bihor,
            06 => BistritaNasaud,
            07 => Botosani,
            08 => Brasov,
            09 => Braila,
            10 => Buzau,
            11 => CarasSeverin,
            12 => Cluj,
            13 => Constanta,
            14 => Covasna,
            15 => Dambovita,
            16 => Dolj,
            17 => Galati,
            18 => Gorj,
            19 => Harghita,
            20 => Hunedoara,
            21 => Ialomita,
            22 => Iasi,
            23 => Ilfov,
            24 => Maramures,
            25 => Mehedinti,
            26 => Mures,
            27 => Neamt,
            28 => Olt,
            29 => Prahova,
            30 => SatuMare,
            31 => Salaj,
            32 => Sibiu,
            33 => Suceava,
            34 => Teleorman,
            35 => Timis,
            36 => Tulcea,
            37 => Vaslui,
            38 => Valcea,
            39 => Vrancea,
            40 => Bucuresti,
            41 => BucurestiSector1,
            42 => BucurestiSector2,
            43 => BucurestiSector3,
            44 => BucurestiSector4,
            45 => BucurestiSector5,
            46 => BucurestiSector6,
            47 => BucurestiSector7,
            48 => BucurestiSector8,
            51 => Calarasi,
            52 => Giurgiu,
            70 => CodUnic,
            _ => null
        };
    }
    
    public static readonly County Alba = new(01, "Alba");
    public static readonly County Arad = new(02, "Arad");
    public static readonly County Arges = new(03, "Argeș");
    public static readonly County Bacau = new(04, "Bacău");
    public static readonly County Bihor = new(05, "Bihor");
    public static readonly County BistritaNasaud = new(06, "Bistrița-Năsăud");
    public static readonly County Botosani = new(07, "Botoșani");
    public static readonly County Brasov = new(08, "Brașov");
    public static readonly County Braila = new(09, "Brăila");
    public static readonly County Buzau = new(10, "Buzău");
    public static readonly County CarasSeverin = new(11, "Caraș-Severin");
    public static readonly County Cluj = new(12, "Cluj");
    public static readonly County Constanta = new(13, "Constanța");
    public static readonly County Covasna = new(14, "Covasna");
    public static readonly County Dambovita = new(15, "Dâmbovița");
    public static readonly County Dolj = new(16, "Dolj");
    public static readonly County Galati = new(17, "Galați");
    public static readonly County Gorj = new(18, "Gorj");
    public static readonly County Harghita = new(19, "Harghita");
    public static readonly County Hunedoara = new(20, "Hunedoara");
    public static readonly County Ialomita = new(21, "Ialomița");
    public static readonly County Iasi = new(22, "Iași");
    public static readonly County Ilfov = new(23, "Ilfov");
    public static readonly County Maramures = new(24, "Maramureș");
    public static readonly County Mehedinti = new(25, "Mehedinți");
    public static readonly County Mures = new(26, "Mureș");
    public static readonly County Neamt = new(27, "Neamț");
    public static readonly County Olt = new(28, "Olt");
    public static readonly County Prahova = new(29, "Prahova");
    public static readonly County SatuMare = new(30, "Satu Mare");
    public static readonly County Salaj = new(31, "Sălaj");
    public static readonly County Sibiu = new(32, "Sibiu");
    public static readonly County Suceava = new(33, "Suceava");
    public static readonly County Teleorman = new(34, "Teleorman");
    public static readonly County Timis = new(35, "Timiș");
    public static readonly County Tulcea = new(36, "Tulcea");
    public static readonly County Vaslui = new(37, "Vaslui");
    public static readonly County Valcea = new(38, "Vâlcea");
    public static readonly County Vrancea = new(39, "Vrancea");
    public static readonly County Bucuresti = new(40, "București");
    public static readonly County BucurestiSector1 = new(41, "București - Sector 1");
    public static readonly County BucurestiSector2 = new(42, "București - Sector 2");
    public static readonly County BucurestiSector3 = new(43, "București - Sector 3");
    public static readonly County BucurestiSector4 = new(44, "București - Sector 4");
    public static readonly County BucurestiSector5 = new(45, "București - Sector 5");
    public static readonly County BucurestiSector6 = new(46, "București - Sector 6");
    public static readonly County Calarasi = new(51, "Călărași");
    public static readonly County Giurgiu = new(52, "Giurgiu");
    public static readonly County BucurestiSector7 = new(47, "Bucuresti - Sector 7 (desființat)");
    public static readonly County BucurestiSector8 = new(48, "Bucuresti - Sector 8 (desființat)");
    public static readonly County CodUnic = new(70, "Cod unic pentru orice înregistrare, indiferent de județul/locul unde a avut loc nașterea");
}