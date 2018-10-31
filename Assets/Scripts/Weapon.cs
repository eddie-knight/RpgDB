namespace RpgDB
{
    public class Weapon : IRpgObject
    {

        public string Name { get; set; }
        public int id { get; set; } // Primary Key
        public string Category { get; set; }
        public int Level { get; set; }
        public int Price { get; set; }
        public string Bulk { get; set; }
        public string Special { get; set; }

        public string Type { get; set; }
        public string Range { get; set; }
        public string Capacity { get; set; }
        public string Damage { get; set; }
        public string Critical { get; set; }
        public int Usage { get; set; }

    }
}