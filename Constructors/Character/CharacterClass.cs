using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

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

        public List<string> Proficiencies = new List<string>();
        public List<ClassSkills> ClassSkills = new List<ClassSkills>();
        public List<IRpgDBEntry> Inventory = new List<IRpgDBEntry>();

        public void Awake()
        {
            LoadSkills();
        }

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

        public void AddObject(JToken item)
        {
            ClassSkills classSkills = new ClassSkills();
            classSkills.ConvertObject(item);
            ClassSkills.Add(classSkills);
        }

        public void LoadSkills()
        {
            JToken jsonObject = Database.GetJsonFromFile(Name);
            foreach (JToken data in jsonObject[Name])
            {
                AddObject(data);
            }
        }

        // Load data from "{{name}}.json"
        // Create List of ClassStats
    }
}
