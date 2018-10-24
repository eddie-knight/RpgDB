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
        public string[] meleeCategories = { "1h_melee", "2h_melee" };

        public List<Weapon> RangedWeapons;
        public string[] rangedCategories = { "small_arms", "longarms", "snipers", "heavy_weapons", "thrown" };

        public void Start()
        {
            // Weapon data is only loaded on first instantiation of Prefabs
            if(MeleeWeapons.Count == 0)
                LoadWeaponData(meleeCategories, MeleeWeapons);
            if (RangedWeapons.Count == 0)
                LoadWeaponData(rangedCategories, RangedWeapons);
        }

        public void LoadWeaponData(string[] types, List<Weapon>list)
        {
            // Debugging Tip:
            // If an error occurs due to a JSON entry, run the script
            // and look at which item is the last entered to the db.
            // The next item on the list caused the error.
            foreach (string type in types)
            {
                Database.LoadWeaponData(type, list);
            }
        }
    }
}
