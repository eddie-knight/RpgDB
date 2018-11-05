using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class ArmorDatabase : Database
    {
        public string[] ArmorCategories = { "light_armor, heavy_armor" };

        public static List<IRpgObject> ArmorList = new List<IRpgObject>();
        public static List<Armor> Armor = new List<Armor>();

        public void Start()
        {
            // Data is only loaded on first instantiation of Prefabs
            if (Armor.Count < 1)
            {
                LoadData(ArmorCategories, ArmorList);
                Armor = ArmorList.Cast<Armor>().ToList();
            }
        }

        // Add Object to Armor List
        public override void AddObject(JToken item, List<IRpgObject> list, string category)
        {
            Armor armor = new Armor();
            armor.ConvertObject(item, category);
            list.Add(armor);
        }
    }
}
