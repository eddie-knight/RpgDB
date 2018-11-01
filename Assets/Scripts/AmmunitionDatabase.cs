using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class AmmunitionDatabase : Database
    {
        public static string[] ammunitionCategories = { "ammunition" };

        public static List<IRpgObject> AmmunitionList = new List<IRpgObject>();
        public List<Ammunition> Ammunition = new List<Ammunition>();

        public void Start()
        {
            // Data is only loaded on first instantiation of Prefabs
            if (Ammunition.Count < 1)
            {
                LoadData(ammunitionCategories, AmmunitionList);
                Ammunition = AmmunitionList.Cast<Ammunition>().ToList();
            }
        }

        // Add Object to Ammunition List
        public override void AddObject(JToken item, List<IRpgObject> list, string category)
        {
            Ammunition convertedObject = (Ammunition)ConvertObject(item, category);
            convertedObject.Category = category;
            list.Add(convertedObject);
        }

        // Convert JToken object to Ammunition object
        public override IRpgObject ConvertObject(JToken item, string category)
        {
            Ammunition ammunition = new Ammunition();
            foreach (KeyValuePair<string, JToken> content in (JObject)item)
            {
                // TODO: This is giving an error in spite of being identical to the non-buggy WeaponDatabase.
                var field = ammunition.GetType().GetProperty(content.Key);
                if ((object)field.PropertyType == typeof(string))
                    field.SetValue(ammunition, content.Value.Value<string>(), null);
                else if (field.PropertyType == typeof(int))
                    field.SetValue(ammunition, content.Value.Value<int>(), null);
            }
            return ammunition;
        }

        // TODO: Build Search Functions.

    }
}
