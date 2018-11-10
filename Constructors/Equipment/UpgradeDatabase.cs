using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class UpgradeDatabase : Database
    {
        public string[] UpgradesCategories = { "upgrades" };

        public static List<IRpgDBEntry> UpgradesList = new List<IRpgDBEntry>();
        public static List<Upgrade> Upgrades = new List<Upgrade>();

        public void Awake()
        {
            if (Upgrades.Count < 1)
            {
                LoadData(UpgradesCategories, UpgradesList);
                Upgrades = UpgradesList.Cast<Upgrade>().ToList();
            }
        }

        // Add Object to Upgrades List
        public override void AddObject(JToken item, List<IRpgDBEntry> list, string category)
        {
            Upgrade Upgrade = new Upgrade();
            Upgrade.ConvertObject(item, category);
            list.Add(Upgrade);
        }

        public Upgrade GetByName(string text)
        {
            return Upgrades.Find(x => x.Name.Equals(text));
        }

    }
}
