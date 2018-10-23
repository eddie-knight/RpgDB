using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

namespace TextRPG
{
    public class Database : MonoBehaviour
    {
        // The JsonHome path may change
        private string JsonHome = 'json/';

        public static JObject GetJsonFromFile(string file_path)
        {
            string file_output = "";
            using (StreamReader file = new StreamReader(JsonHome + file_path + '.json'))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    file_output += line;
                }
            }
            return JObject.Parse(file_output);
        }


        // 
        // Melee Weapons
        //
        public static void AddMeleeWeapons(JToken data, List<Weapon> Weapons)
        {
            Weapon weapon = new Weapon();
            weapon.Name = (string)data["Name"];
            weapon.Type = (string)data["Type"];
            weapon.Level = (int)data["Level"];
            weapon.Price = (int)data["Price"];
            weapon.Damage = (string)data["Damage"];
            weapon.Critical = (string)data["Critical"]; ;
            weapon.Bulk = (string)data["Bulk"];
            weapon.Special = (string)data["Special"];

            Weapons.Add(weapon);
        }

        public static void LoadMeleeWeaponData(string title, List<Weapon> Weapons)
        {
            JToken jsonObject = Database.GetJsonFromFile(title);
            foreach (JToken data in jsonObject[title])
            {
                Database.AddMeleeWeapons(data, Weapons);
            }
        }
        // 
        // Ranged Weapons
        //
        public static void AddRangedWeapons(JToken data, List<Weapon> Weapons)
        {
            Weapon weapon = new Weapon();
            weapon.Name = (string)data["Name"];
            weapon.Type = (string)data["Type"];
            weapon.Level = (int)data["Level"];
            weapon.Price = (int)data["Price"];
            weapon.Damage = (string)data["Damage"];
            weapon.Critical = (string)data["Critical"]; ;
            weapon.Capacity = (string)data["Capacity"]; ;
            weapon.Usage = (string)data["Usage"]; ;
            weapon.Bulk = (string)data["Bulk"];
            weapon.Special = (string)data["Special"];

            Weapons.Add(weapon);
        }

        public static void LoadRangedWeaponData(string title, List<Weapon> Weapons)
        {
            JToken jsonObject = Database.GetJsonFromFile(title);
            foreach (JToken data in jsonObject[title])
            {
                Database.AddRangedWeapons(data, Weapons);
            }
        }
    }
}