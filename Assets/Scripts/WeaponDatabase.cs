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
            string[] meleeTypes = ["1h_melee, 2h_melee"];
            string[] rangedTypes = ["small_arms","longarms","snipers","heavy_weapons","thrown"]

            for (type in meleeTypes)
            {
                Database.LoadMeleeWeaponData(type, MeleeWeapons);
            }

            for (type in rangedTypes)
            {
                Database.LoadRangedWeaponData(type, RangedWeapons);
            }
        }
    }
}
