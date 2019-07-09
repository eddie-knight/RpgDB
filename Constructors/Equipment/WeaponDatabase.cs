using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;

namespace RpgDB
{
    public class WeaponDatabase : Database
    {
        [HideInInspector] public string[] MeleeCategories = { "1h_melee", "2h_melee", "solarian_crystals" };
        [HideInInspector] public string[] RangedCategories = { "small_arms", "longarms", "snipers", "heavy_weapons" };
        [HideInInspector] public string ThrownCategory = "thrown";

        // TODO: Prove that this is able to hold data in a prefab
        public static List<Weapon> All = new List<Weapon>();

        public void Awake()
        {
            // These only live long enough to build the list "All"
            // TODO: Create logic not rely on interface lists for LoadData
            List<IRpgDBEntry> MeleeWeapons = new List<IRpgDBEntry>();
            List<IRpgDBEntry> RangedWeapons = new List<IRpgDBEntry>();
            List<IRpgDBEntry> ThrownWeapons = new List<IRpgDBEntry>();
            List<Weapon> MeleeWeaponsList = new List<Weapon>();
            List<Weapon> RangedWeaponsList = new List<Weapon>();
            List<Weapon> ThrownWeaponsList = new List<Weapon>();

            if (MeleeWeaponsList.Count < 1)
            {
                LoadData(MeleeCategories, MeleeWeapons);
                MeleeWeaponsList = MeleeWeapons.Cast<Weapon>().ToList();
            }
            if (RangedWeaponsList.Count < 1)
            {
                LoadData(RangedCategories, RangedWeapons);
                RangedWeaponsList = RangedWeapons.Cast<Weapon>().ToList();
            }
            if (ThrownWeaponsList.Count < 1)
            {
                LoadData(ThrownCategory, ThrownWeapons);
                ThrownWeaponsList = RangedWeapons.Cast<Weapon>().ToList();
            }
            if (All.Count < 1)
                All = MeleeWeaponsList.Union(RangedWeaponsList).Union(ThrownWeaponsList).ToList();
        }

        // Convert JToken to Weapon object, then add to provided list
        public override void AddObject(JToken item, List<IRpgDBEntry> list, string category)
        {
            Weapon weapon = new Weapon();
            weapon.ConvertObject(item, category);
            list.Add(weapon);
        }

        // TODO: Fix case-sensitivity on all search functions

        // Return one weapon with exact name
        public Weapon GetByName(string text)
        {
            return All.Find(x => x.Name.Equals(text));
        }

        // Return all weapons with text in name
        public static List<Weapon> SearchWeaponsByName(string text)
        {
            return All.FindAll(x => x.Name.Contains(text));
        }

        // Return all weapons with text in type
        public static List<Weapon> SearchWeaponsByType(string text)
        {
            return All.FindAll(x => x.Type.Contains(text));
        }

    }
}
