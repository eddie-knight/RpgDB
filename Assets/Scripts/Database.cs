using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

namespace TextRPG
{
    public class Database : MonoBehaviour
    {
        public static string JsonHome = @"json/";

        // Convert JSON file to JObject
        public static JObject GetJsonFromFile(string file_path)
        {
            string file_output = "";
            using (StreamReader file = new StreamReader(JsonHome + file_path + ".json"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    file_output += line;
                }
            }
            return JObject.Parse(file_output);
        }

        // Convert JToken object to Weapon object
        public static Weapon ConvertWeapon(JToken data)
        {
            Weapon weapon = new Weapon();
            foreach (KeyValuePair<string, JToken> content in (JObject) data)
            {
                var field = weapon.GetType().GetField(content.Key);
                if ((object) field.FieldType == typeof(string))
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
                Database.AddWeapon(data, Weapons, title);
            }
        }
    }
}