using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;

namespace RpgDB
{
    public class ClassDatabase : Database
    {
        public string classCategory = "character_classes";

        public static List<IRpgDBEntry> ClassList = new List<IRpgDBEntry>();
        public static List<CharacterClass> All = new List<CharacterClass>();

        public void Awake()
        {
            if (All.Count < 1)
            {
                LoadData(classCategory, ClassList);
                All = ClassList.Cast<CharacterClass>().ToList();
            }
        }

        // Add Object to Class List
        public override void AddObject(JToken item, List<IRpgDBEntry> list, string category)
        {
            CharacterClass characterClass = new CharacterClass();
            characterClass.ConvertObject(item);
            list.Add(characterClass);
        }

        public CharacterClass GetByName(string text)
        {
            CharacterClass retrievedClass = All.Find(x => x.Name.Equals(text));
            retrievedClass.Awake();
            return retrievedClass;
        }

        public Character CreateCharacterByClass(string characterClass)
        {
            CharacterClass classObject = GetByName(characterClass);
            Character character = new Character(classObject);
            return character;
        }

    }
}
