using UnityEngine;
using System;
using System.Collections.Generic;

namespace RpgDB
{
    public class Character : MonoBehaviour
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public string Gender { get; set; }
        public string Size = "Unsupported";
        public string Race = "Unsupported";
        public string Theme = "Unsupported";
        public string Home_World = "Unsupported";
        public string Speed = "Unsupported";
        public string Alignment = "Unsupported";
        public string Diety = "Unsupported";
        public string Description = "";

        public int Base_Save;
        public int Temporary_Hit_Points;
        public int Damage_Taken;

        // Anywhere Character.Class is used here, it should check whether
        // Class = "Multiclass" and subsequently handle Character.Multiclass
        public CharacterClass Class;
        public List<CharacterClass> Multiclass = new List<CharacterClass>();

        // Provide roll functionality within this class
        private Roll Roll = new Roll();

        // This requires functions that look for modifiers
        // in appropriate places and return the highest modifier value
        // Check for modifiers on levelling and equip/unequip
        public CharacterModifiers Modifiers = new CharacterModifiers();

        public List<Feat> Feats = new List<Feat>();

        public Abilities Abilities = new Abilities();
        public Skills SkillRanks = new Skills();

        public Armor Armor = new Armor { Name = "None" };
        public Weapon Right_Hand = new Weapon { Name = "None" };
        public Weapon Left_Hand = new Weapon { Name = "None" };

        // TODO:
        // In order to use "professions", we need function to specify the ability,
        // and create a reference to the value of the appropriate variable.

        //
        //
        // Base Stats

        public int Key_Ability_Score()
        {
            int value = 0;
            if (Class != null)
            {
                if (Class.Name == "Soldier")
                    value = soldierHighestScore();
                else
                {
                    var field = Abilities.GetType().GetField(Class.Key_Ability_Score);
                    value = (int)field.GetValue(Abilities);
                }
            }
            else
                foreach (CharacterClass characterClass in Multiclass)
                {
                    int value_check = 0;
                    if (characterClass.Name == "Soldier")
                        value_check = soldierHighestScore();
                    else
                    {
                        var field = Abilities.GetType().GetField(characterClass.Key_Ability_Score);
                        value_check = (int)field.GetValue(Abilities);
                    }
                    if (value < value_check)
                        value = value_check;
                }
            return value;
        }


        public int Hit_Points()
        {
            int class_hp = 0;

            if (Class != null)
                class_hp = Class.Hit_Points;
            else
                foreach (CharacterClass characterClass in Multiclass)
                {
                    // Store value if HP is greater than previous highest
                    class_hp = class_hp > characterClass.Hit_Points ? class_hp : characterClass.Hit_Points;
                }
            return class_hp * Level + Temporary_Hit_Points;// TODO: + race.Hit_Points
        }


        public int Stamina_Points()
        {
            int class_stamina = 0;

            if (Class != null)
                class_stamina = Class.Hit_Points;
            else
                foreach (CharacterClass characterClass in Multiclass)
                {
                    // Store value if HP is greater than previous highest
                    class_stamina = class_stamina > characterClass.Stamina_Points ? class_stamina : characterClass.Stamina_Points;
                }
            // TODO: This should be applied at each level-up, or gaining CON will retroactively grant more SP
            return (class_stamina + mod(Abilities.CON)) * Level;// TODO: + race.Hit_Points

        }


        public int Resolve_Points()
        {
            return (int)Math.Ceiling((double)Level / 2) + mod(Key_Ability_Score());
        }


        public int Skill_Points_Available()
        {
            if (Class != null)
                return (Class.Skill_Ranks_per_Level * Level) - SkillRanks.Allocated();

            int ranks = 0;
            foreach (CharacterClass characterClass in Multiclass)
            {
                ranks = ranks > characterClass.Skill_Ranks_per_Level ? ranks : characterClass.Skill_Ranks_per_Level;
            }
            return 0;
        }


        public int BAB()
        {
            if (Class != null)
                return Class.ClassSkills.Find(x => x.Level.Equals(Level)).Base_Attack_Bonus;
            int class_BAB;
            int highest_BAB = 0;
            foreach (CharacterClass characterClass in Multiclass)
            {
                // Store value if HP is greater than previous highest
                class_BAB = Class.ClassSkills.Find(x => x.Level.Equals(Level)).Base_Attack_Bonus;
                highest_BAB = highest_BAB > class_BAB ? class_BAB : highest_BAB;
            }
            return highest_BAB;
        }



        //
        //
        // Helper functions

        public int soldierHighestScore()
        {
            // Return the higher value of STR or DEX
            return Abilities.STR > Abilities.DEX ? Abilities.STR : Abilities.DEX;
        }

        public int mod(int abilty)
        {
            return (int)Math.Floor((double)(abilty - 10) / 2);
        }



        //
        //
        // Character Builders

        public Character()
        {
            // This is for instantiating a Character without providing class.
        }


        public Character(CharacterClass characterClass, ExtensionsDatabase extensions)
        {
            Level = characterClass.Level;
            Class = characterClass;
            foreach(int proficiency_id in characterClass.Proficiencies)
            {
                Feat proficiency = extensions.GetFeatById(proficiency_id);
                Feats.Add(proficiency);
            }
        }


        // Multiclass Character
        public Character(List<CharacterClass> characterClasses, ExtensionsDatabase extensions)
        {
            List<int> unique_proficiency_ids = new List<int>();
            Class = null;
            Multiclass = characterClasses;
            foreach (CharacterClass characterClass in characterClasses)
            {
                Level += characterClass.Level;

                // Compile proficiency lists
                foreach (int proficiency_id in characterClass.Proficiencies)
                    if (!unique_proficiency_ids.Contains(proficiency_id))
                        unique_proficiency_ids.Add(proficiency_id);
                // Add proficiencies from each class without duplication
                foreach(int id in unique_proficiency_ids)
                {
                    Feat proficiency = extensions.GetFeatById(id);
                    Feats.Add(proficiency);
                }
            }
        }


        public Character Clone()
        {
            // TODO: Test this functionality
            Character character = new Character
            {
                Name = Name,
                Level = Level,
                Base_Save = Base_Save,
                Class = Class,
                Multiclass = Multiclass,
                Feats = Feats,
                SkillRanks = SkillRanks,
                Armor = Armor,
                Right_Hand = Right_Hand,
                Left_Hand = Left_Hand,
                Abilities = Abilities,
                Modifiers = Modifiers
            };
            return character;
        }



        //
        //
        // Calculated Character Stats

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
            return BAB() + mod(Abilities.STR) + Modifiers.Melee;
        }

        public int RangedAttack()
        {
            return BAB() + mod(Abilities.DEX) + Modifiers.Ranged;
        }

        public int ThrownAttack()
        {
            return BAB() + mod(Abilities.STR) + Modifiers.Thrown;
        }



        //
        //
        // Calculated Ability Scores

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
                // TODO: If melee or thrown, add mod(STR) to damage
            }
            return damage;
        }

        public void ApplyDamage(int damage)
        {
            Damage_Taken = Damage_Taken + damage;
            // TODO: Check for <= 0
        }

        public void ApplyHealing(int healing)
        {
            int health = Damage_Taken - healing;
            if (health > 0)
                Damage_Taken = health;
            else
                Damage_Taken = 0;
        }

    }
}