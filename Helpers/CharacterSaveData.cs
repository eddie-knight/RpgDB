using UnityEngine;
using System.Collections;
using System;

namespace RpgDB
{
    [Serializable]
    public class CharacterSaveData
    {
        public string Name;
        public int Level;
        public string Class;
        string[] MultiClass;
        public int[] Abilities = new int[6];
        public int[] SkillRanks = new int[19];

        public CharacterSaveData(Character character)
        {
            Name = character.Name;
            Level = character.Level;
            SetClass(character);
            SetAbilities(character.Abilities);
            SetSkillRanks(character.SkillRanks);
        }

        public Character LoadSaveData()
        {
            GameObject databaseObject = GameObject.Find("GameDatabase");
            GameDatabase database = databaseObject.GetComponent<GameDatabase>();
            
            Character character = new Character();

            if (Class != null)
                character = database.Classes.CreateCharacter(Class, Level, database.Extensions);
            // TODO: Multiclass needs levels for each class in order to load
            // else if (MultiClass != null)
            // database.Classes.CreateCharacter(MultiClass, Level, database.Extensions)

            character.Name = Name;
            character.SkillRanks = LoadSkillRanks();
            character.Abilities = LoadAbilities();
            return character;
        }

        void SetClass(Character character)
        {
            int classCount = character.Multiclass.Count;
            MultiClass = new string[classCount];

            if (character.Class != null)
            {
                Class = character.Class.Name;
            }
            else
            {
                int i = 0;
                foreach (CharacterClass characterClass in character.Multiclass)
                {
                    MultiClass[i] = characterClass.Name;
                    i++;
                }
            }
        }

        void SetAbilities(Abilities abilities)
        {
            Abilities[0] = abilities.STR;
            Abilities[1] = abilities.DEX;
            Abilities[2] = abilities.CON;
            Abilities[3] = abilities.INT;
            Abilities[5] = abilities.WIS;
            Abilities[5] = abilities.CHA;
        }

        void SetSkillRanks(Skills skillRanks)
        {
            SkillRanks[0] = skillRanks.Acrobatics;
            SkillRanks[1] = skillRanks.Athletics;
            SkillRanks[2] = skillRanks.Bluff;
            SkillRanks[3] = skillRanks.Computers;
            SkillRanks[4] = skillRanks.Culture;
            SkillRanks[5] = skillRanks.Diplomacy;
            SkillRanks[6] = skillRanks.Disguise;
            SkillRanks[7] = skillRanks.Engineering;
            SkillRanks[8] = skillRanks.Intimidate;
            SkillRanks[9] = skillRanks.Life_Science;
            SkillRanks[10] = skillRanks.Medicine;
            SkillRanks[11] = skillRanks.Mysticism;
            SkillRanks[12] = skillRanks.Perception;
            SkillRanks[13] = skillRanks.Physical_Science;
            SkillRanks[14] = skillRanks.Piloting;
            SkillRanks[15] = skillRanks.Sense_Motive;
            SkillRanks[16] = skillRanks.Slight_of_Hand;
            SkillRanks[17] = skillRanks.Stealth;
            SkillRanks[18] = skillRanks.Survival;
        }

        Skills LoadSkillRanks()
        {
            return new Skills
            {
                Acrobatics = SkillRanks[0],
                Athletics = SkillRanks[1],
                Bluff = SkillRanks[2],
                Computers = SkillRanks[3],
                Culture = SkillRanks[4],
                Diplomacy = SkillRanks[5],
                Disguise = SkillRanks[6],
                Engineering = SkillRanks[7],
                Intimidate = SkillRanks[8],
                Life_Science = SkillRanks[9],
                Medicine = SkillRanks[10],
                Mysticism = SkillRanks[11],
                Perception = SkillRanks[12],
                Physical_Science = SkillRanks[13],
                Piloting = SkillRanks[14],
                Sense_Motive = SkillRanks[15],
                Slight_of_Hand = SkillRanks[16],
                Stealth = SkillRanks[17],
                Survival = SkillRanks[18]
            };
        }

        Abilities LoadAbilities()
        {
            return new Abilities
            {
                STR = Abilities[0],
                DEX = Abilities[1],
                CON = Abilities[2],
                INT = Abilities[3],
                WIS = Abilities[5],
                CHA = Abilities[5]
            };
        }
    }
}