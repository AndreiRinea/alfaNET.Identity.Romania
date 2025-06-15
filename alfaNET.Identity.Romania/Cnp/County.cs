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
namespace alfaNET.Identity.Romania.Cnp;

public class County
{
    private readonly byte _code;
    private readonly string _name;

    private County(byte code, string name)
    {
        _code = code;
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }

    public override int GetHashCode()
    {
        return _code;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not County other) return false;
        return _code == other._code;
    }
    
    public static bool operator ==(County? left, County? right) => Equals(left, right);
    public static bool operator !=(County? left, County? right) => !Equals(left, right);
    
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

    public static County? GetByCode(byte code)
    {
        switch (code)
        {
            case 01: return Alba;
            case 02: return Arad;
            case 03: return Arges;
            case 04: return Bacau;
            case 05: return Bihor;
            case 06: return BistritaNasaud;
            case 07: return Botosani;
            case 08: return Brasov;
            case 09: return Braila;
            case 10: return Buzau;
            case 11: return CarasSeverin;
            case 12: return Cluj;
            case 13: return Constanta;
            case 14: return Covasna;
            case 15: return Dambovita;
            case 16: return Dolj;
            case 17: return Galati;
            case 18: return Gorj;
            case 19: return Harghita;
            case 20: return Hunedoara;
            case 21: return Ialomita;
            case 22: return Iasi;
            case 23: return Ilfov;
            case 24: return Maramures;
            case 25: return Mehedinti;
            case 26: return Mures;
            case 27: return Neamt;
            case 28: return Olt;
            case 29: return Prahova;
            case 30: return SatuMare;
            case 31: return Salaj;
            case 32: return Sibiu;
            case 33: return Suceava;
            case 34: return Teleorman;
            case 35: return Timis;
            case 36: return Tulcea;
            case 37: return Vaslui;
            case 38: return Valcea;
            case 39: return Vrancea;
            case 40: return Bucuresti;
            case 41: return BucurestiSector1;
            case 42: return BucurestiSector2;
            case 43: return BucurestiSector3;
            case 44: return BucurestiSector4;
            case 45: return BucurestiSector5;
            case 46: return BucurestiSector6;
            case 47: return BucurestiSector7;
            case 48: return BucurestiSector8;
            case 51: return Calarasi;
            case 52: return Giurgiu;
            case 70: return CodUnic;
            default: return null;
        }
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