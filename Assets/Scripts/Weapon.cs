namespace RPGDB
{
    [System.Serializable]
    public class Weapon
    {
        public string Name;
        public int id; // Primary Key
        public string Category;
        public string Type;
        public int Level;
        public int Price;
        public string Range;
        public string Capacity;
        public string Damage;
        public string Critical;
        public int Usage;
        public string Bulk;
        public string Special;
    }
}
