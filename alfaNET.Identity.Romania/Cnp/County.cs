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

using System.Collections.Generic;

namespace alfaNET.Identity.Romania.Cnp
{
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
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = obj as County;
            if (other == null)
            {
                return false;
            }

            return _code == other._code;
        }

        public static bool operator ==(County left, County right) => Equals(left, right);
        public static bool operator !=(County left, County right) => !Equals(left, right);

        /// <summary>
        /// An enumeration of all available counties
        /// </summary>
        /// <returns>An array presented as an IEnumerable</returns>
        public static IEnumerable<County> List() =>
        new[] { 
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
        };

        /// <summary>
        /// Obtains a county by the numerical ID
        /// </summary>
        /// <param name="code">the numerical ID of the county</param>
        /// <returns>The matching county or null if no counties match the ID</returns>
        public static County GetByCode(byte code)
        {
            switch (code)
            {
                case 01:
                    return Alba;
                case 02:
                    return Arad;
                case 03:
                    return Arges;
                case 04:
                    return Bacau;
                case 05:
                    return Bihor;
                case 06:
                    return BistritaNasaud;
                case 07:
                    return Botosani;
                case 08:
                    return Brasov;
                case 09:
                    return Braila;
                case 10:
                    return Buzau;
                case 11:
                    return CarasSeverin;
                case 12:
                    return Cluj;
                case 13:
                    return Constanta;
                case 14:
                    return Covasna;
                case 15:
                    return Dambovita;
                case 16:
                    return Dolj;
                case 17:
                    return Galati;
                case 18:
                    return Gorj;
                case 19:
                    return Harghita;
                case 20:
                    return Hunedoara;
                case 21:
                    return Ialomita;
                case 22:
                    return Iasi;
                case 23:
                    return Ilfov;
                case 24:
                    return Maramures;
                case 25:
                    return Mehedinti;
                case 26:
                    return Mures;
                case 27:
                    return Neamt;
                case 28:
                    return Olt;
                case 29:
                    return Prahova;
                case 30:
                    return SatuMare;
                case 31:
                    return Salaj;
                case 32:
                    return Sibiu;
                case 33:
                    return Suceava;
                case 34:
                    return Teleorman;
                case 35:
                    return Timis;
                case 36:
                    return Tulcea;
                case 37:
                    return Vaslui;
                case 38:
                    return Valcea;
                case 39:
                    return Vrancea;
                case 40:
                    return Bucuresti;
                case 41:
                    return BucurestiSector1;
                case 42:
                    return BucurestiSector2;
                case 43:
                    return BucurestiSector3;
                case 44:
                    return BucurestiSector4;
                case 45:
                    return BucurestiSector5;
                case 46:
                    return BucurestiSector6;
                case 47:
                    return BucurestiSector7;
                case 48:
                    return BucurestiSector8;
                case 51:
                    return Calarasi;
                case 52:
                    return Giurgiu;
                case 70:
                    return CodUnic;
                default:
                    return null;
            }
        }

        public static readonly County Alba = new County(01, "Alba");
        public static readonly County Arad = new County(02, "Arad");
        public static readonly County Arges = new County(03, "Argeș");
        public static readonly County Bacau = new County(04, "Bacău");
        public static readonly County Bihor = new County(05, "Bihor");
        public static readonly County BistritaNasaud = new County(06, "Bistrița-Năsăud");
        public static readonly County Botosani = new County(07, "Botoșani");
        public static readonly County Brasov = new County(08, "Brașov");
        public static readonly County Braila = new County(09, "Brăila");
        public static readonly County Buzau = new County(10, "Buzău");
        public static readonly County CarasSeverin = new County(11, "Caraș-Severin");
        public static readonly County Cluj = new County(12, "Cluj");
        public static readonly County Constanta = new County(13, "Constanța");
        public static readonly County Covasna = new County(14, "Covasna");
        public static readonly County Dambovita = new County(15, "Dâmbovița");
        public static readonly County Dolj = new County(16, "Dolj");
        public static readonly County Galati = new County(17, "Galați");
        public static readonly County Gorj = new County(18, "Gorj");
        public static readonly County Harghita = new County(19, "Harghita");
        public static readonly County Hunedoara = new County(20, "Hunedoara");
        public static readonly County Ialomita = new County(21, "Ialomița");
        public static readonly County Iasi = new County(22, "Iași");
        public static readonly County Ilfov = new County(23, "Ilfov");
        public static readonly County Maramures = new County(24, "Maramureș");
        public static readonly County Mehedinti = new County(25, "Mehedinți");
        public static readonly County Mures = new County(26, "Mureș");
        public static readonly County Neamt = new County(27, "Neamț");
        public static readonly County Olt = new County(28, "Olt");
        public static readonly County Prahova = new County(29, "Prahova");
        public static readonly County SatuMare = new County(30, "Satu Mare");
        public static readonly County Salaj = new County(31, "Sălaj");
        public static readonly County Sibiu = new County(32, "Sibiu");
        public static readonly County Suceava = new County(33, "Suceava");
        public static readonly County Teleorman = new County(34, "Teleorman");
        public static readonly County Timis = new County(35, "Timiș");
        public static readonly County Tulcea = new County(36, "Tulcea");
        public static readonly County Vaslui = new County(37, "Vaslui");
        public static readonly County Valcea = new County(38, "Vâlcea");
        public static readonly County Vrancea = new County(39, "Vrancea");
        public static readonly County Bucuresti = new County(40, "București");
        public static readonly County BucurestiSector1 = new County(41, "București - Sector 1");
        public static readonly County BucurestiSector2 = new County(42, "București - Sector 2");
        public static readonly County BucurestiSector3 = new County(43, "București - Sector 3");
        public static readonly County BucurestiSector4 = new County(44, "București - Sector 4");
        public static readonly County BucurestiSector5 = new County(45, "București - Sector 5");
        public static readonly County BucurestiSector6 = new County(46, "București - Sector 6");
        public static readonly County Calarasi = new County(51, "Călărași");
        public static readonly County Giurgiu = new County(52, "Giurgiu");
        public static readonly County BucurestiSector7 = new County(47, "Bucuresti - Sector 7 (desființat)");
        public static readonly County BucurestiSector8 = new County(48, "Bucuresti - Sector 8 (desființat)");

        public static readonly County CodUnic = new County(70,
            "Cod unic pentru orice înregistrare, indiferent de județul/locul unde a avut loc nașterea");
    }
}