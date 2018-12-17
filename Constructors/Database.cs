using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RpgDB
{
    public abstract class Database : MonoBehaviour
    {
        // Location of directory containing JSON files
        // Relative to project home
        public static string JsonHome = @"Assets/RpgDB/json/";

        public abstract void AddObject(JToken item, List<IRpgDBEntry> list, string category=null);

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
                file.Dispose();
            }
            return JObject.Parse(file_output);
        }

        // Get All Object Data from JSON and Add to List
        public void LoadDataFromJson(string category, List<IRpgDBEntry> list)
        {
            JToken jsonObject = GetJsonFromFile(category);
            foreach (JToken data in jsonObject[category])
            {
                AddObject(data, list, category);
            }
        }

        // Convert JSON file data into a List format
        public void LoadData(string[] categories, List<IRpgDBEntry> list)
        {
            foreach (string category in categories)
            {
                LoadDataFromJson(category, list);
                Debug.Log("Loading Data for " + category + ".");
            }
        }

        // Convert JSON file data into a List format
        public void LoadData(string category, List<IRpgDBEntry> list)
        {
            LoadDataFromJson(category, list);
        }

    }
}