using UnityEngine;
using System.Collections;

namespace RpgDB
{
    public class TestObject : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            WeaponDatabase Weapons = gameObject.AddComponent(typeof(WeaponDatabase)) as WeaponDatabase;
            AmmunitionDatabase Ammunition = gameObject.AddComponent(typeof(AmmunitionDatabase)) as AmmunitionDatabase;
            ArmorDatabase Armor = gameObject.AddComponent(typeof(ArmorDatabase)) as ArmorDatabase;
            UpgradeDatabase Upgrades = gameObject.AddComponent(typeof(UpgradeDatabase)) as UpgradeDatabase;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}