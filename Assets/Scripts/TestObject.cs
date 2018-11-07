using UnityEngine;
using System.Collections;

namespace RpgDB
{
    public class TestObject : MonoBehaviour
    {
        WeaponDatabase Weapons;
        AmmunitionDatabase Ammunition;
        ArmorDatabase Armor;
        UpgradeDatabase Upgrades;
        ClassDatabase Classes;
        // Use this for initialization
        void Awake()
        {
            Weapons = gameObject.AddComponent(typeof(WeaponDatabase)) as WeaponDatabase;
            Ammunition = gameObject.AddComponent(typeof(AmmunitionDatabase)) as AmmunitionDatabase;
            Armor = gameObject.AddComponent(typeof(ArmorDatabase)) as ArmorDatabase;
            Upgrades = gameObject.AddComponent(typeof(UpgradeDatabase)) as UpgradeDatabase;
            Classes = gameObject.AddComponent(typeof(ClassDatabase)) as ClassDatabase;
        }

        private void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}