using UnityEngine;
using System.Collections;

namespace RpgDB
{
    public sealed class GameDatabase : MonoBehaviour
    {
        public WeaponDatabase Weapons;
        public AmmunitionDatabase Ammunition;
        public ArmorDatabase Armor;
        public UpgradeDatabase Upgrades;
        public ClassDatabase Classes;
        public ExtensionsDatabase Extensions;

        void Awake()
        {
            Weapons = gameObject.AddComponent(typeof(WeaponDatabase)) as WeaponDatabase;
            Ammunition = gameObject.AddComponent(typeof(AmmunitionDatabase)) as AmmunitionDatabase;
            Armor = gameObject.AddComponent(typeof(ArmorDatabase)) as ArmorDatabase;
            Upgrades = gameObject.AddComponent(typeof(UpgradeDatabase)) as UpgradeDatabase;
            Extensions = gameObject.AddComponent(typeof(ExtensionsDatabase)) as ExtensionsDatabase;
            Classes = gameObject.AddComponent(typeof(ClassDatabase)) as ClassDatabase;
        }

        private void Start()
        {
        }
    }
}