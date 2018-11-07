namespace RpgDB
{
    [System.Serializable]
    public class Weapon : RpgDBObject, IRpgDBEntry
    {
        public string Type { get; set; }
        public string Range { get; set; }
        public string Capacity { get; set; }
        public string Damage { get; set; }
        public string Critical { get; set; }
        public int Usage { get; set; }
        public string Special { get; set; }
    }
}