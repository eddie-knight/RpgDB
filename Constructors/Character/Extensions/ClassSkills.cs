using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RpgDB
{
    [System.Serializable]
    public sealed class ClassSkills
    {
        public int Level { get; set; }
        public int Base_Attack_Bonus { get; set; }
        public int Fort_Save_Bonus { get; set; }
        public int Ref_Save_Bonus { get; set; }
        public int Will_Save_Bonus { get; set; }
        public string Class_Features { get; set; }

        public int Spells_per_Day_lvl1 { get; set; }
        public int Spells_per_Day_lvl2 { get; set; }
        public int Spells_per_Day_lvl3 { get; set; }
        public int Spells_per_Day_lvl4 { get; set; }
        public int Spells_per_Day_lvl5 { get; set; }
        public int Spells_per_Day_lvl6 { get; set; }

        public override string ToString()
        {
            return "Skills for a level " + Level + " character.";
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
