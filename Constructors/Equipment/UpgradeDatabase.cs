using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace RpgDB
{
    public class UpgradeDatabase : Database
    {
        public string UpgradesCategory = "upgrades";

        public static List<IRpgDBEntry> UpgradesList = new List<IRpgDBEntry>();
        public static List<Upgrade> All = new List<Upgrade>();

        public void Awake()
        {
            if (All.Count < 1)
            {
                LoadData(UpgradesCategory, UpgradesList);
                All = UpgradesList.Cast<Upgrade>().ToList();
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
            return All.Find(x => x.Name.Equals(text));
        }

    }
}
