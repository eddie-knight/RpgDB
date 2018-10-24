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
    }
}