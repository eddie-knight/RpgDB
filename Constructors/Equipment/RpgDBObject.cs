using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RpgDB
{
    public class RpgDBObject : ScriptableObject
    {
        [Header("--Base--")]
        public string Name;
        public int id; // Primary Key
        public string Category;
        public int Level;
        public int Price;
        public string Bulk;
        public Sprite sprite;

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
        }
    }
}
