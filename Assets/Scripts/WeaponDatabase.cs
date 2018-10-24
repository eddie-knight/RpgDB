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
            string[] meleeTypes = { "1h_melee", "2h_melee" };
            string[] rangedTypes = {"small_arms", "longarms", "snipers", "heavy_weapons", "thrown"};
            // Debug Tip:
            // If an error occurs due to a JSON entry, run the script
            // and look at which item is the last entered to the db

            foreach (string type in meleeTypes)
            {
                Database.LoadMeleeWeaponData(type, MeleeWeapons);
            }

            foreach (string type in rangedTypes)
            {
                Database.LoadRangedWeaponData(type, RangedWeapons);
            }
        }
    }
}
