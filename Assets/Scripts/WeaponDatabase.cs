using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TextRPG
{
    public class WeaponDatabase : Database
    {
        public List<Weapon> MeleeWeapons;
        public List<Weapon> RangedWeapons;

        public void Start()
        {
            Database.LoadMeleeWeaponData("1h_melee", MeleeWeapons)
            Database.LoadMeleeWeaponData("2h_melee", MeleeWeapons)
            Database.LoadRangedWeaponData("small_arms", RangedWeapons)
            Database.LoadRangedWeaponData("longarms", RangedWeapons)
            Database.LoadRangedWeaponData("snipers", RangedWeapons)
        }
    }
}
