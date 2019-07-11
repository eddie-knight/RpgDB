using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;

namespace RpgDB
{
    public sealed class ClassDatabase : Database
    {
        [HideInInspector] public string classCategory = "character_classes";
        [HideInInspector] public string proficienciesCategory = "class_proficiencies";

        public static List<CharacterClass> All = new List<CharacterClass>();
        public static List<IRpgDBEntry> ClassList = new List<IRpgDBEntry>();
        public void Awake()
        {
            // These are alive just long enough to pass through LoadData
            List<IRpgDBEntry> ProficiencyList = new List<IRpgDBEntry>();

            // This list is for holding proficiency data to pass to new classes
            List<Proficiency> Proficiencies = new List<Proficiency>();
            
            if (All.Count < 1)
            {
                LoadData(classCategory, ClassList);
                All = ClassList.Cast<CharacterClass>().ToList();
            }
            if (Proficiencies.Count < 1)
            {
                LoadData(proficienciesCategory, ProficiencyList);
                Proficiencies = ProficiencyList.Cast<Proficiency>().ToList();
                foreach(Proficiency data in Proficiencies)
                {
                    AddProficiencyIdToClass(data);
                }
            }
        }

        public void AddProficiencyIdToClass(Proficiency data)
        {
            // Find class and modify it in place
            All.Find(x => x.id.Equals(data.class_id)).Proficiencies.Add(data.feat_id);
        }

        // Add Object to Class List
        public override void AddObject(JToken item, List<IRpgDBEntry> list, string category)
        {
            if (category == "character_classes")
            {
                CharacterClass characterClass = new CharacterClass();
                characterClass.ConvertObject(item);
                list.Add(characterClass);
            }
            if (category == "class_proficiencies")
            {
                Proficiency proficiency = new Proficiency();
                proficiency.ConvertObject(item);
                list.Add(proficiency);
            }
        }

        public static CharacterClass GetByName(string text)
        {
            CharacterClass retrievedClass = All.Find(x => x.Name.Equals(text));
            retrievedClass.Awake();
            return retrievedClass;
        }


        public Character CreateCharacter(List<CharacterClass> characterClasses, ExtensionsDatabase extensions)
        {
            Character character = new Character(characterClasses, extensions);
            return character;
        }

        public Character CreateCharacter(GameObject characterObject, string characterClass, int level, ExtensionsDatabase extensions)
        {
            CharacterClass classObject = GetByName(characterClass);
            classObject.Level = level;
            Character temporaryCharacter = new Character(classObject, extensions);

            Character character = characterObject.AddComponent<Character>();
            character.Clone(temporaryCharacter);
            return character;
        }
    }
}
