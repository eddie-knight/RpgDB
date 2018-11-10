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

        public void Awake()
        {
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

        public Ammunition GetByName(string text)
        {
            return Ammunition.Find(x => x.Name.Equals(text));
        }
    }
}
