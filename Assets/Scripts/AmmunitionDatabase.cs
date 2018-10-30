using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RPGDB
{
    public class AmmuntionDatabase : Database
    {
        public List<Weapon> Ammunition;
        // ? public static List<Weapon> AmmunitionList;
        public static string[] ammunitionCategories = { "ammunition" };

        public void Start()
        {
            // Data is only loaded on first instantiation of Prefabs
            if (Ammunition.Count == 0)
            {
            	// Database.LoadData()
                LoadData(ammunitionCategories, Ammunition);
            }
        }

        // Convert JToken object to Ammunition object
        public static Ammunition ConvertObject(JToken data)
        {
            Ammunition ammunition = new Ammunition();
            foreach (KeyValuePair<string, JToken> content in (JObject)data)
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
