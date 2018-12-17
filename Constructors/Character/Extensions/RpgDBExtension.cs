using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RpgDB
{
    [System.Serializable]
    public class RpgDBExtension : IRpgDBEntry
    {
        public string Name { get; set; }
        public int id { get; set; } // Primary Key
        public string Description { get; set; }

        public override string ToString()
        {
            return Name + " [id: " + id + "]";
        }

        public void ConvertObject(JToken item)
        {
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
