using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace TextRPG
{
    public class WeaponDatabase : Database
    {
        public List<Weapon> MeleeWeapons;
        public static List<Weapon> MeleeWeaponsList;

        public static string[] meleeCategories = { "1h_melee", "2h_melee" };

        public List<Weapon> RangedWeapons;
        public static List<Weapon> RangedWeaponsList;
        public static string[] rangedCategories = { "small_arms", "longarms", "snipers", "heavy_weapons", "thrown" };

        public static List<Weapon> AllWeaponsList;

        public void Start()
        {
            // Weapon data is only loaded on first instantiation of Prefabs
            if (MeleeWeapons.Count == 0)
            {
                LoadWeaponData(meleeCategories, MeleeWeapons);
                MeleeWeaponsList = MeleeWeapons;
            }
            if (RangedWeapons.Count == 0)
            {
                LoadWeaponData(rangedCategories, RangedWeapons);
                RangedWeaponsList = RangedWeapons;
            }
            AllWeaponsList = MeleeWeaponsList.Union(RangedWeaponsList).ToList();
        }

        // Convert JToken object to Weapon object
        public static Weapon ConvertWeapon(JToken data)
        {
            Weapon weapon = new Weapon();
            foreach (KeyValuePair<string, JToken> content in (JObject)data)
            {
                var field = weapon.GetType().GetField(content.Key);
                if ((object)field.FieldType == typeof(string))
                    field.SetValue(weapon, content.Value.Value<string>());
                else if (field.FieldType == typeof(int))
                    field.SetValue(weapon, content.Value.Value<int>());
            }
            return weapon;
        }

        // Add Weapon to List
        public static void AddWeapon(JToken weapon, List<Weapon> Weapons, string title)
        {
            Weapon convertedWeapon = ConvertWeapon(weapon);
            convertedWeapon.Category = title;
            Weapons.Add(convertedWeapon);
        }

        // Get All Weapon Data from JSON and add to List
        public static void LoadWeaponData(string title, List<Weapon> Weapons)
        {
            JToken jsonObject = Database.GetJsonFromFile(title);
            foreach (JToken data in jsonObject[title])
            {
                AddWeapon(data, Weapons, title);
            }
        }

        // Convert JSON file data into a List format
        public void LoadWeaponData(string[] types, List<Weapon> list)
        {
            foreach (string type in types)
            {
                LoadWeaponData(type, list);
            }
        }


        // SEARCH FUNCTIONALITY
        // These functions will return a List with any appropriate responses.
        // TODO: Fix case-sensitivity on all search functions

        // If provided category exists, find all weapons with category
        public static List<Weapon> SearchWeaponsByCategory(string category)
        {
            return meleeCategories.Contains(category) || rangedCategories.Contains(category)
                                  ? AllWeaponsList.FindAll(x => x.Category.Contains(category))
                                      : null;
        }

        // Return one weapon with exact name
        public static Weapon FindWeaponByName(string text)
        {
            return AllWeaponsList.Find(x => x.Name.Equals(text));
        }

        // Return all melee weapons with text in name
        public static List<Weapon> SearchMeleeWeaponsByName(string text)
        {
            return MeleeWeaponsList.FindAll(x => x.Name.Contains(text));
        }

        // Return all ranged weapons with text in name
        public static List<Weapon> SearchRangedWeaponsByName(string text)
        {
            return RangedWeaponsList.FindAll(x => x.Name.Contains(text));
        }

        // Return all weapons with text in name
        public static List<Weapon> SearchWeaponsByName(string text)
        {
            return AllWeaponsList.FindAll(x => x.Name.Contains(text));
        }

        // Return all weapons with text in type
        public static List<Weapon> SearchWeaponsByType(string text)
        {
            return AllWeaponsList.FindAll(x => x.Type.Contains(text));
        }

    }
}
