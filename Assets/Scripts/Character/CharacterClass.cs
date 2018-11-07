using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RpgDB
{
    [System.Serializable]
    public class CharacterClass : IRpgDBEntry
    {
        public string Name { get; set; }
        public int id { get; set; } // Primary Key
        public string Description { get; set; }
        public int Hit_Points { get; set; }
        public int Stamina_Points { get; set; }
        public string Key_Ability_Score_Text { get; set; }
        public string Key_Ability_Score { get; set; }
        public int Skill_Ranks_per_Level { get; set; }

        public string Category = "Class";

        public override string ToString()
        {
            return Name + " [" + Category + "]";
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
