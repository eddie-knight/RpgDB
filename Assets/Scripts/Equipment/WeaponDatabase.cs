using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class WeaponDatabase : Database
    {
        public string[] MeleeCategories = { "1h_melee", "2h_melee", "solarian_crystals" };
        public string[] RangedCategories = { "small_arms", "longarms", "snipers", "heavy_weapons", "thrown" };

        public static List<IRpgObject> MeleeWeapons = new List<IRpgObject>();
        public static List<IRpgObject> RangedWeapons = new List<IRpgObject>();
        public static List<Weapon> AllWeapons = new List<Weapon>();

        public void Start()
        {
            List<Weapon> MeleeWeaponsList = new List<Weapon>();
            List<Weapon> RangedWeaponsList = new List<Weapon>();

            // Data is only loaded on first instantiation of Prefabs
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
        public override void AddObject(JToken item, List<IRpgObject> list, string category)
        {
            Weapon weapon = new Weapon();
            weapon.ConvertObject(item, category);
            list.Add(weapon);
        }

        // TODO: Fix case-sensitivity on all search functions

        // Return one weapon with exact name
        public Weapon FindWeaponByName(string text)
        {
            return AllWeapons.Find(x => x.Name.Equals(text));
        }

        // Return all melee weapons with text in name
        public static List<Weapon> SearchMeleeWeaponsByName(string text)
        {
            return MeleeWeapons.FindAll(x => x.Name.Contains(text)).Cast<Weapon>().ToList();
        }

        // Return all ranged weapons with text in name
        public static List<Weapon> SearchRangedWeaponsByName(string text)
        {
            return MeleeWeapons.FindAll(x => x.Name.Contains(text)).Cast<Weapon>().ToList();
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
