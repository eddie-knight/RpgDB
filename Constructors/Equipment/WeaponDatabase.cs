using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;

namespace RpgDB
{
    public class WeaponDatabase : Database
    {
        public string[] MeleeCategories = { "1h_melee", "2h_melee", "solarian_crystals" };
        public string[] RangedCategories = { "small_arms", "longarms", "snipers", "heavy_weapons", "thrown" };

        public static List<IRpgDBEntry> MeleeWeapons = new List<IRpgDBEntry>();
        public static List<IRpgDBEntry> RangedWeapons = new List<IRpgDBEntry>();

        public static List<Weapon> AllWeapons = new List<Weapon>();

        public void Awake()
        {
            List<Weapon> MeleeWeaponsList = new List<Weapon>();
            List<Weapon> RangedWeaponsList = new List<Weapon>();

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
            if (AllWeapons.Count < 1)
                AllWeapons = MeleeWeaponsList.Union(RangedWeaponsList).ToList();
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
            return AllWeapons.Find(x => x.Name.Equals(text));
        }

        // Return all weapons with text in name
        public static List<Weapon> SearchWeaponsByName(string text)
        {
            return AllWeapons.FindAll(x => x.Name.Contains(text));
        }

        // Return all weapons with text in type
        public static List<Weapon> SearchWeaponsByType(string text)
        {
            return AllWeapons.FindAll(x => x.Type.Contains(text));
        }

    }
}
