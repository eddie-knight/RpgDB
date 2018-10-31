using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class WeaponDatabase : Database
    {
        public static string[] meleeCategories = { "1h_melee", "2h_melee" };
        public static string[] rangedCategories = { "small_arms", "longarms", "snipers", "heavy_weapons", "thrown" };

        public static List<Weapon> MeleeWeaponsList;
        public List<Weapon> MeleeWeapons;

        public static List<Weapon> RangedWeaponsList;
        public List<Weapon> RangedWeapons;

        public static List<Weapon> AllWeapons;

        public void Start()
        {
            // Data is only loaded on first instantiation of Prefabs
            if (MeleeWeaponsList.Count < 1)
            {
                // Database.LoadData()
                LoadData(meleeCategories);
                MeleeWeapons = MeleeWeaponsList;
            }
            if (RangedWeaponsList.Count < 1)
            {
                LoadData(rangedCategories);
                RangedWeapons = RangedWeaponsList;
            }
            AllWeapons = MeleeWeapons.Union(RangedWeapons).ToList();
        }

        // Add Object to Given List
        public override void AddObject(JToken item, string category)
        {
            Weapon convertedObject = ConvertObject(item);
            convertedObject.Category = category;
            if (rangedCategories.Contains(category))
                MeleeWeaponsList.Add(convertedObject);
            else
                RangedWeaponsList.Add(convertedObject);

        }

        // Convert JToken object to Weapon object
        public Weapon ConvertObject(JToken item)
        {
            Weapon weapon = new Weapon();
            foreach (KeyValuePair<string, JToken> content in (JObject)item)
            {
                var field = weapon.GetType().GetField(content.Key);
                if ((object)field.FieldType == typeof(string))
                    field.SetValue(weapon, content.Value.Value<string>());
                else if (field.FieldType == typeof(int))
                    field.SetValue(weapon, content.Value.Value<int>());
            }
            return weapon;
        }

        // TODO: Fix case-sensitivity on all search functions

        // If provided category exists, find all weapons with category
        public static List<Weapon> SearchWeaponsByCategory(string category)
        {
            return meleeCategories.Contains(category) || rangedCategories.Contains(category)
                                  ? AllWeapons.FindAll(x => x.Category.Contains(category))
                                      : null;
        }

        // Return one weapon with exact name
        public static Weapon FindWeaponByName(string text)
        {
            return AllWeapons.Find(x => x.Name.Equals(text));
        }

        // Return all melee weapons with text in name
        public static List<Weapon> SearchMeleeWeaponsByName(string text)
        {
            return MeleeWeaponsList.FindAll(x => x.Name.Contains(text));
        }

        // Return all ranged weapons with text in name
        public static List<Weapon> SearchRangedWeaponsByName(string text)
        {
            return MeleeWeaponsList.FindAll(x => x.Name.Contains(text));
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
