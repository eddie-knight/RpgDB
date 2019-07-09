using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class ArmorDatabase : Database
    {
        [HideInInspector] public string[] ArmorCategories = { "light_armor", "heavy_armor" };

        public static List<IRpgDBEntry> ArmorList = new List<IRpgDBEntry>();
        public static List<Armor> All = new List<Armor>();

        public void Awake()
        {
            if (All.Count < 1)
            {
                LoadData(ArmorCategories, ArmorList);
                All = ArmorList.Cast<Armor>().ToList();
            }
        }

        // Add Object to Armor List
        public override void AddObject(JToken item, List<IRpgDBEntry> list, string category)
        {
            Armor armor = new Armor();
            armor.ConvertObject(item, category);
            list.Add(armor);
        }

        public Armor GetByName(string text)
        {
            return All.Find(x => x.Name.Equals(text));
        }

    }
}
