using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class AmmunitionDatabase : Database
    {
        public string[] ammunitionCategories = { "ammunition" };

        public static List<IRpgDBEntry> AmmunitionList = new List<IRpgDBEntry>();
        public static List<Ammunition> Ammunition = new List<Ammunition>();

        public void Start()
        {
            // Data is only loaded on first instantiation of Prefabs
            if (Ammunition.Count < 1)
            {
                LoadData(ammunitionCategories, AmmunitionList);
                Ammunition = AmmunitionList.Cast<Ammunition>().ToList();
            }
        }

        // Add Object to Ammunition List
        public override void AddObject(JToken item, List<IRpgDBEntry> list, string category)
        {
            Ammunition ammunition = new Ammunition();
            ammunition.ConvertObject(item, category);
            list.Add(ammunition);
        }

        // TODO: Build Search Functions.

    }
}
