using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;

namespace RpgDB
{
    public class ClassDatabase : Database
    {
        public string[] classCategories = { "character_classes" };

        public static List<IRpgDBEntry> ClassList = new List<IRpgDBEntry>();
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
        public override void AddObject(JToken item, List<IRpgDBEntry> list, string category)
        {
            CharacterClass characterClass = new CharacterClass();
            characterClass.ConvertObject(item);
            list.Add(characterClass);
        }

        // TODO: Build Search Functions.

        public CharacterClass FindClassByName(string text)
        {
            return Classes.Find(x => x.Name.Equals(text));
        }
    }
}
