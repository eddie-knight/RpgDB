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
            // Debugging Tip:
            // If an error occurs due to a JSON entry, run the script
            // and look at which item is the last entered to the db.
            // The next item on the list caused the error.
            foreach (string type in types)
            {
                LoadWeaponData(type, list);
            }
        }

    }
}
