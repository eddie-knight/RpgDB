namespace RPGDB
{
    [System.Serializable]
    public class Weapon : RpgObject
    {
        public string Type;
        public string Range;
        public string Capacity;
        public string Damage;
        public string Critical;
        public int Usage;
    }
}
