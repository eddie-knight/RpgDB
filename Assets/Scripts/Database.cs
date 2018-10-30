using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

namespace RPGDB
{
    public class Database : MonoBehaviour
    {
        // Relative location of directory containing JSON files
        public static string JsonHome = @"json/";

        abstract public static var ConvertObject();

        abstract public static void LoadDataFromJson();

        // Add Object to Given List
        public static void AddObject(JToken weapon, var item, string category, int id)
        {
            Weapon convertedObject = ConvertObject(item);
            convertedObject.id = id;
            Weapons.Add(convertedObject);
        }

        // Get All Object Data from JSON and Add to List
        public static void LoadDataFromJson(string category, var list)
        {
            JToken jsonObject = Database.GetJsonFromFile(category);
            int id = 1; 
            foreach (JToken data in jsonObject[category])
            {
                AddObject(data, list, category, id);
                id++;
            }
        }

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

        // Convert JSON file data into a List format
        public void LoadData(string[] categories, List<Weapon> list)
        {
            foreach (string category in categories)
            {
                LoadDataFromJson(category, list);
            }
        }
    }
}