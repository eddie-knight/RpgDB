using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RpgDB
{
    [System.Serializable]
    public class RpgDBObject : IRpgObject
    {
        public string Name { get; set; }
        public int id { get; set; } // Primary Key
        public string Category { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }
        public string Bulk { get; set; }

        public override string ToString ()
        {
            return Name + " [Level " + Level + " " + Category + "]";
        }
        
        public void ConvertObject(JToken item, string category)
        {
            Category = category;
            foreach (KeyValuePair<string, JToken> content in (JObject)item)
            {
                var field = this.GetType().GetProperty(content.Key);
                if ((object)field.PropertyType == typeof(string))
                    field.SetValue(this, content.Value.Value<string>(), null);
                else if (field.PropertyType == typeof(int))
                    field.SetValue(this, content.Value.Value<int>(), null);
            }
            Debug.Log(this.ToString());
        }
    }
}
