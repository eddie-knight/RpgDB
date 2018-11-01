namespace RpgDB
{
    [System.Serializable]
    public class Ammunition : IRpgObject
    {
        public string Name { get; set; }
        public int id { get; set; } // Primary Key
        public string Category { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }
        public string Bulk { get; set; }
        public string Special { get; set; }

        public string Charges { get; set; }
    }
}
