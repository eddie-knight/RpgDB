using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TextRPG
{
    public class WeaponDatabase : Database
    {
        public List<Weapon> Weapons;

        public void Start()
        {
            string jsonHome = @"json/";
            string oneHandMeleePath = jsonHome + @"1h_melee.json";
            string twoHandMeleePath = jsonHome + @"2h_melee.json";

            JToken jsonObject = Database.GetJsonFromFile(oneHandMeleePath);
            foreach (JToken data in jsonObject["1-hand Melee"])
            {
                Database.LoadMeleeItems(data, Weapons);
            }

            jsonObject = Database.GetJsonFromFile(twoHandMeleePath);
            foreach (JToken data in jsonObject["2-hand Melee"])
            {
                Database.LoadMeleeItems(data, Weapons);
            }
        }
    }
}
