namespace RpgDB
{
    [System.Serializable]
    public class Weapon : RpgDBObject, IRpgDBEntry
    {
        public bool Melee { get; set; }
        public bool Ranged { get; set; }
        public bool Thrown { get; set; }
        public string Type { get; set; }
        public string Range { get; set; }
        public string Capacity { get; set; }
        public string Damage { get; set; }
        public string Critical { get; set; }
        public int Usage { get; set; }
        public string Special { get; set; }
    }
}