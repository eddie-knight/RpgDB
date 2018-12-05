using UnityEngine;
using System;
using System.Collections.Generic;

namespace RpgDB
{
    // Characters are built from their selected associated class
    // This does not currently allow for multiclassing
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }

        // These may be functions that look for the class value or
        // the highest value from multiple classes
        public int Hit_Points;
        public int Resolve_Points;
        public int Base_Save;
        public int BAB;


        // Anywhere Character.Class is used, it should check whether
        // Class = "Multiclass" and subsequently handle Character.Multiclass
        public CharacterClass Class;
        public List<CharacterClass> Multiclass = new List<CharacterClass>();

        // Provide roll functionality within this class
        private Roll Roll = new Roll();

        // This requires functions that look for modifiers
        // in appropriate places and return the highest modifier value
        public Modifiers Modifiers = new Modifiers();

        public Abilities Abilities = new Abilities();
        public Skills SkillRanks = new Skills();

        public Armor Armor { get; set; }
        public Weapon Right_Hand { get; set; }
        public Weapon Left_Hand { get; set; }

        // TODO:
        // In order to use "professions", we need function to specify the ability,
        // and create a reference to the value of the appropriate variable.


        //
        // Helper function(s)

        public int mod(int abilty)
        {
            return (int)Math.Floor((double)(abilty - 10) / 2);
        }

        public Character()
        {
            // This is for instantiating a Character without providing class.
        }

        public Character(CharacterClass characterClass)
        {
            Level = characterClass.Level;
            Class = characterClass;
        }

        // Creates Multiclass Character
        public Character(List<CharacterClass> characterClasses)
        {
            Class = null;
            Multiclass = characterClasses;
            foreach (CharacterClass characterClass in characterClasses)
            {
                Level += characterClass.Level;
                // TODO:
                // Find and store highest modifiers
            }
        }

        public Character Clone()
        {
            Character character = new Character
            {
                // TODO:
                // Copy all values
            };
            return character;
        }

        // need single class and multiclass
        public Character ApplyAbilityScores(Character character)
        {
            // 1. Apply Ability Scores
            return character;
        }


        public void LevelUp(CharacterClass characterClass)
        {
            // 1. Apply Ability Scores
            // 2. Add New Class Features
            // 3. Add New Feats or Theme Benefits
            // 4. Invest Skill Ranks
        }


        //
        // Display Calculated Character Stats

        public int Initiative()
        {
            return mod(Abilities.DEX) + Modifiers.Initiative;
        }

        public int EAC()
        {
            return Armor.EAC_Bonus + mod(Abilities.DEX) + Modifiers.EAC;
        }

        public int KAC()
        {
            return Armor.KAC_Bonus + mod(Abilities.DEX) + Modifiers.KAC;
        }

        public int AC_Vs_Combat()
        {
            return KAC() + 8;
        }

        public int Fortitude()
        {
            return Base_Save + mod(Abilities.CON) + Modifiers.Fortitude;
        }

        public int Reflex()
        {
            return Base_Save + mod(Abilities.DEX) + Modifiers.Reflex;
        }

        public int Will()
        {
            return Base_Save + mod(Abilities.WIS) + Modifiers.Will;
        }

        public int MeleeAttack()
        {
            return BAB + mod(Abilities.STR) + Modifiers.Melee;
        }

        public int RangedAttack()
        {
            return BAB + mod(Abilities.DEX) + Modifiers.Ranged;
        }

        public int ThrownAttack()
        {
            return BAB + mod(Abilities.STR) + Modifiers.Thrown;
        }


        //
        // Display Total Ability Scores

        public int Acrobatics()
        {
            return SkillRanks.Acrobatics + mod(Abilities.DEX) + Modifiers.Skills.Acrobatics;
        }

        public int Athletics()
        {
            return SkillRanks.Athletics + mod(Abilities.STR) + Modifiers.Skills.Athletics;
        }

        public int Bluff()
        {
            return SkillRanks.Bluff + mod(Abilities.CHA) + Modifiers.Skills.Bluff;
        }

        public int Computers()
        {
            return SkillRanks.Computers + mod(Abilities.INT) + Modifiers.Skills.Computers;
        }

        public int Culture()
        {
            return SkillRanks.Culture + mod(Abilities.INT) + Modifiers.Skills.Culture;
        }

        public int Diplomacy()
        {
            return SkillRanks.Diplomacy + mod(Abilities.CHA) + Modifiers.Skills.Diplomacy;
        }

        public int Disguise()
        {
            return SkillRanks.Disguise + mod(Abilities.CHA) + Modifiers.Skills.Disguise;
        }

        public int Engineering()
        {
            return SkillRanks.Engineering + mod(Abilities.INT) + Modifiers.Skills.Engineering;
        }

        public int Intimidate()
        {
            return SkillRanks.Intimidate + mod(Abilities.CHA) + Modifiers.Skills.Intimidate;
        }

        public int Life_Science()
        {
            return SkillRanks.Life_Science + mod(Abilities.INT) + Modifiers.Skills.Life_Science;
        }

        public int Medicine()
        {
            return SkillRanks.Medicine + mod(Abilities.INT) + Modifiers.Skills.Medicine;
        }

        public int Mysticism()
        {
            return SkillRanks.Mysticism + mod(Abilities.WIS) + Modifiers.Skills.Mysticism;
        }

        public int Perception()
        {
            return SkillRanks.Perception + mod(Abilities.WIS) + Modifiers.Skills.Perception;
        }

        public int Physical_Science()
        {
            return SkillRanks.Physical_Science + mod(Abilities.INT) + Modifiers.Skills.Physical_Science;
        }

        public int Piloting()
        {
            return SkillRanks.Piloting + mod(Abilities.INT) + Modifiers.Skills.Piloting;
        }

        public int Sense_Motive()
        {
            return SkillRanks.Sense_Motive + mod(Abilities.WIS) + Modifiers.Skills.Sense_Motive;
        }

        public int Slight_of_Hand()
        {
            return SkillRanks.Slight_of_Hand + mod(Abilities.DEX) + Modifiers.Skills.Slight_of_Hand;
        }

        public int Stealth()
        {
            return SkillRanks.Stealth + mod(Abilities.DEX) + Modifiers.Skills.Stealth;
        }

        public int Survival()
        {
            return SkillRanks.Survival + mod(Abilities.WIS) + Modifiers.Skills.Stealth;
        }


        //
        // Combat Functions

        bool AttackCheck(Weapon weapon, int defense)
        {
            // TODO: Move roll result out to somewhere accessible to display
            int modifier = 0;
            int roll = Roll.d20();

            if (weapon.Melee || weapon.Thrown)
                modifier = mod(Abilities.STR);
            else if (weapon.Ranged)
                modifier = mod(Abilities.DEX);

            // TODO: Add proficiency modifiers (helper class?)
            return (roll + modifier) > defense;
        }

        public int Attack(Weapon weapon, int targetKAC)
        {
            int damage = 0;

            if (AttackCheck(weapon, targetKAC))
            {
                // Parse strings such as "2d8" and "1d10"
                string[] damageString = weapon.Damage.Split('d');

                int rollCount = Int32.Parse(damageString[0]);
                int damageDie = Int32.Parse(damageString[1]);

                for (int i = 0; i < rollCount; i++)
                {
                    damage += Roll.rollDie(damageDie);
                }
            }
            return damage;
        }

    }
}