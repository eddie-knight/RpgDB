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
        public List<IRpgDBEntry> ClassSkills = new List<IRpgDBEntry>();

        public void Awake()
        {
            
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

        public void AddObject(JToken item, List<ClassSkills> list, string category)
        {
            ClassSkills classSkills = new ClassSkills();
            classSkills.ConvertObject(item);
            list.Add(classSkills);
        }

        public void LoadDataFromJson(string category, List<ClassSkills> list)
        {
            JToken jsonObject = Database.GetJsonFromFile(category);
            foreach (JToken data in jsonObject[category])
            {
                AddObject(data, list, category);
            }
        }

        public void LoadData(string[] categories, List<ClassSkills> list)
        {
            foreach (string category in categories)
            {
                if (category != null)
                    LoadDataFromJson(category, list);
            }
        }
        // Load data from "{{name}}.json"
        // Create List of ClassStats
    }
}
