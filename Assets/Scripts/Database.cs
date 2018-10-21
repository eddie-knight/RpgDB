using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

namespace TextRPG
{
    public class Database : MonoBehaviour
    {
        public static JObject GetJsonFromFile(string file_path)
        {
            string file_output = "";
            using (StreamReader file = new StreamReader(file_path))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    file_output += line;
                }
            }
            return JObject.Parse(file_output);
        }

        public static void LoadMeleeItems(JToken data, List<Weapon> Weapons)
        {
            Weapon weapon = new Weapon();
            weapon.Name = (string)data["Name"];
            weapon.Type = (string)data["Type"];
            weapon.Level = (int)data["Level"];
            weapon.Price = (string)data["Price"];
            weapon.Damage = (string)data["Damage"];
            weapon.Critical = (string)data["Critical"]; ;
            weapon.Bulk = (string)data["Bulk"];
            weapon.Special = (string)data["Special"];

            Weapons.Add(weapon);
        }
    }
}