using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using RpgDB;

namespace RpgDB
{
    // Characters are built from their selected associated class
    // This does not currently allow for multiclassing
    public class Character : CharacterClass
    {
        public string Class;

        public int STR;
        public int DEX;
        public int CON;
        public int INT;
        public int WIS;
        public int CHA;
        public int Resolve_Points;

        public int Base_Save;
        public int BAB;

        public int EAC_Mod;
        public int KAC_Mod;
        public int Fortitude_Mod;
        public int Reflex_Mod;
        public int Will_Mod;
        public int Melee_Mod;
        public int Ranged_Mod;
        public int Thrown_Mod;
        public int Initiative_Mod;

        // TODO:
        // In order to use "professions", we need a way to specify the ability,
        // and create a reference to the value of the appropriate variable.

        public Roll Roll = new Roll();
        public Abilities AbilityRanks = new Abilities();
        public Abilities AbilityMods = new Abilities();

        public Armor Armor { get; set; }
        public Weapon Right_Hand { get; set; }
        public Weapon Left_Hand { get; set; }

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
            Class = characterClass.Name;
            Description = characterClass.Description;
            Hit_Points = characterClass.Hit_Points;
            Stamina_Points = characterClass.Stamina_Points;
            Key_Ability_Score_Text = characterClass.Key_Ability_Score_Text;
            Key_Ability_Score = characterClass.Key_Ability_Score;
            Skill_Ranks_per_Level = characterClass.Skill_Ranks_per_Level;
            Category = characterClass.Category;
            Proficiencies = characterClass.Proficiencies;
            ClassSkills = characterClass.ClassSkills;
        }

        public Character Clone()
        {
            Character character = new Character
            {
                Description = this.Description,
                Hit_Points = this.Hit_Points,
                Stamina_Points = this.Stamina_Points,
                Key_Ability_Score_Text = this.Key_Ability_Score_Text,
                Key_Ability_Score = this.Key_Ability_Score,
                Skill_Ranks_per_Level = this.Skill_Ranks_per_Level,
                Category = this.Category,
                Proficiencies = this.Proficiencies,
                ClassSkills = this.ClassSkills
            };

            return character;
        }

        //
        // Display Calculated Character Stats

        public int Initiative()
        {
            return mod(DEX) + Initiative_Mod;
        }

        public int EAC()
        {
            return Armor.EAC_Bonus + mod(DEX) + EAC_Mod;
        }

        public int KAC()
        {
            return Armor.KAC_Bonus + mod(DEX) + KAC_Mod;
        }

        public int AC_Vs_Combat()
        {
            return KAC() + 8;
        }

        public int Fortitude()
        {
            return Base_Save + mod(CON) + Fortitude_Mod;
        }

        public int Reflex()
        {
            return Base_Save + mod(DEX) + Reflex_Mod;
        }

        public int Will()
        {
            return Base_Save + mod(WIS) + Will_Mod;
        }

        public int MeleeAttack()
        {
            return BAB + mod(STR) + Melee_Mod;
        }

        public int RangedAttack()
        {
            return BAB + mod(DEX) + Ranged_Mod;
        }

        public int ThrownAttack()
        {
            return BAB + mod(STR) + Thrown_Mod;
        }

        //
        // Display Total Ability Scores

        public int Acrobatics()
        {
            return AbilityRanks.Acrobatics + mod(DEX) + AbilityMods.Acrobatics;
        }

        public int Athletics()
        {
            return AbilityRanks.Athletics + mod(STR) + AbilityMods.Athletics;
        }

        public int Bluff()
        {
            return AbilityRanks.Bluff + mod(CHA) + AbilityMods.Bluff;
        }

        public int Computers()
        {
            return AbilityRanks.Computers + mod(INT) + AbilityMods.Computers;
        }

        public int Culture()
        {
            return AbilityRanks.Culture + mod(INT) + AbilityMods.Culture;
        }

        public int Diplomacy()
        {
            return AbilityRanks.Diplomacy + mod(CHA) + AbilityMods.Diplomacy;
        }

        public int Disguise()
        {
            return AbilityRanks.Disguise + mod(CHA) + AbilityMods.Disguise;
        }

        public int Engineering()
        {
            return AbilityRanks.Engineering + mod(INT) + AbilityMods.Engineering;
        }

        public int Intimidate()
        {
            return AbilityRanks.Intimidate + mod(CHA) + AbilityMods.Intimidate;
        }

        public int Life_Science()
        {
            return AbilityRanks.Life_Science + mod(INT) + AbilityMods.Life_Science;
        }

        public int Medicine()
        {
            return AbilityRanks.Medicine + mod(INT) + AbilityMods.Medicine;
        }

        public int Mysticism()
        {
            return AbilityRanks.Mysticism + mod(WIS) + AbilityMods.Mysticism;
        }

        public int Perception()
        {
            return AbilityRanks.Perception + mod(WIS) + AbilityMods.Perception;
        }

        public int Physical_Science()
        {
            return AbilityRanks.Physical_Science + mod(INT) + AbilityMods.Physical_Science;
        }

        public int Piloting()
        {
            return AbilityRanks.Piloting + mod(INT) + AbilityMods.Piloting;
        }

        public int Sense_Motive()
        {
            return AbilityRanks.Sense_Motive + mod(WIS) + AbilityMods.Sense_Motive;
        }

        public int Slight_of_Hand()
        {
            return AbilityRanks.Slight_of_Hand + mod(DEX) + AbilityMods.Slight_of_Hand;
        }

        public int Stealth()
        {
            return AbilityRanks.Stealth + mod(DEX) + AbilityMods.Stealth;
        }

        public int Survival()
        {
            return AbilityRanks.Survival + mod(WIS) + AbilityMods.Stealth;
        }

        //
        // Combat Functions

        bool AttackCheck(Weapon weapon, int defense)
        {
            // TODO: Move roll result out to somewhere accessible to display
            int modifier = 0;
            int roll = Roll.d20();

            if (weapon.Melee || weapon.Thrown)
                modifier = mod(STR);
            else if (weapon.Ranged)
                modifier = mod(DEX);

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