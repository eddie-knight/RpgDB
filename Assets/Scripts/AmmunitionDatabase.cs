using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class AmmuntionDatabase : Database
    {
        public static string[] ammunitionCategories = { "ammunition" };

        public static List<Ammunition> AmmunitionList;
        public List<Ammunition> Ammunition;

        public void Start()
        {
            // Data is only loaded on first instantiation of Prefabs
            if (Ammunition.Count == 0)
            {
            	// Database.LoadData()
                LoadData(ammunitionCategories);
                Ammunition = AmmunitionList;
            }
        }

        // Add Object to Ammunition List
        public override void AddObject(JToken item, string category)
        {
            Ammunition convertedObject = ConvertObject(item);
            convertedObject.Category = category;
            AmmunitionList.Add(convertedObject);
        }

        // Convert JToken object to Ammunition object
        public Ammunition ConvertObject(JToken item)
        {
            Ammunition ammunition = new Ammunition();
            foreach (KeyValuePair<string, JToken> content in (JObject)item)
            {
                var field = ammunition.GetType().GetField(content.Key);
                if ((object)field.FieldType == typeof(string))
                    field.SetValue(ammunition, content.Value.Value<string>());
                else if (field.FieldType == typeof(int))
                    field.SetValue(ammunition, content.Value.Value<int>());
            }
            return ammunition;
        }

        // TODO: Build Search Functions. Static list needed?

    }
}
