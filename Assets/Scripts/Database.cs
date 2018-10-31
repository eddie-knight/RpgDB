using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections;

namespace RpgDB
{
    public abstract class Database : MonoBehaviour
    {
        // Relative location of directory containing JSON files
        public static string JsonHome = @"json/";
        
        public abstract void AddObject(JToken item, string category);

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

        // Get All Object Data from JSON and Add to List
        public void LoadDataFromJson(string category)
        {
            JToken jsonObject = GetJsonFromFile(category);
            foreach (JToken item in jsonObject[category])
            {
                AddObject(item, category);
            }
        }

        // Convert JSON file data into a List format
        public void LoadData(string[] categories)
        {
            foreach (string category in categories)
            {
                LoadDataFromJson(category);
            }
        }
        
    }
}