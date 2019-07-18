using UnityEngine;

namespace RpgDB
{
    [CreateAssetMenu(fileName = "Weapon_", menuName = "ScriptableObjects/RpgDB Objects/Weapon")]
    public class Weapon : RpgDBObject, IRpgDBEntry
    {
        [Header("--Weapon--")]
        public bool Melee;
        public bool Ranged;
        public bool Thrown;
        public string Type;
        public string Range;
        public string Capacity;
        public string Damage;
        public string Critical;
        public int Usage;
        public string Special;
    }
}