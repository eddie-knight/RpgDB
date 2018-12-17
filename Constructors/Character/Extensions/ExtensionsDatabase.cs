using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;

namespace RpgDB
{
    public sealed class ExtensionsDatabase : Database
    {
        public string modifierCategory = "modifiers";
        public string featsCategory = "feats";

        public static List<Modifier> Modifiers = new List<Modifier>();
        public static List<Feat> Feats = new List<Feat>();

        public void Awake()
        {
            List<IRpgDBEntry> ModifiersList = new List<IRpgDBEntry>();
            List<IRpgDBEntry> FeatsList = new List<IRpgDBEntry>();

            if (Modifiers.Count < 1)
            {
                LoadData(modifierCategory, ModifiersList);
                Modifiers = ModifiersList.Cast<Modifier>().ToList();
            }
            if (Feats.Count < 1)
            {
                LoadData(featsCategory, FeatsList);
                Feats = FeatsList.Cast<Feat>().ToList();
            }
        }

        // Add Object to Class List
        public override void AddObject(JToken item, List<IRpgDBEntry> list, string category)
        {
            if (category == "modifiers")
            {
                Modifier content = new Modifier();
                content.ConvertObject(item);
                list.Add(content);
            }
            else if (category == "feats")
            {
                Feat content = new Feat();
                content.ConvertObject(item);
                list.Add(content);
            }
            else
            {
                Debug.Log("Invalid Category. Modify ExtensionDatabase.AddObject to handle this type of data.");
            }

        }

        public Feat GetFeatByName(string text)
        {
            return Feats.Find(x => x.Name.Equals(text));
        }

        public Feat GetFeatById(int id)
        {
            return Feats.Find(x => x.id.Equals(id));
        }

        public Modifier GetModifierById(int id)
        {
            return Modifiers.Find(x => x.id.Equals(id));
        }
    }
}
