using UnityEngine;

namespace RpgDB
{
    public class Weapon : RpgDBObject, IRpgDBEntry
    {
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