using UnityEngine;
using System.Collections;

namespace RpgDB
{
    public sealed class GameDatabase : MonoBehaviour
    {
        [HideInInspector] public WeaponDatabase Weapons;
        [HideInInspector] public AmmunitionDatabase Ammunition;
        [HideInInspector] public ArmorDatabase Armor;
        [HideInInspector] public UpgradeDatabase Upgrades;
        [HideInInspector] public ClassDatabase Classes;
        [HideInInspector] public ExtensionsDatabase Extensions;

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