using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class ClassDatabase : Database
    {
        public string[] classCategories = { "" };

        public static List<IRpgObject> ClassList = new List<IRpgObject>();
        public static List<CharacterClass> Classes = new List<CharacterClass>();

        public void Start()
        {
            // Data is only loaded on first instantiation of Prefabs
            if (Classes.Count < 1)
            {
                LoadData(classCategories, ClassList);
                Classes = ClassList.Cast<CharacterClass>().ToList();
            }
        }

        // Add Object to Ammunition List
        public override void AddObject(JToken item, List<IRpgObject> list, string category)
        {
            CharacterClass characterClass = new CharacterClass();
            characterClass.ConvertObject(item, category);
            list.Add(characterClass);
        }

        // TODO: Build Search Functions.

    }
}
