namespace RpgDB
{
    [System.Serializable]
    public class Weapon : RpgDBObject, IRpgObject
    {
        public string Type { get; set; }
        public string Range { get; set; }
        public string Capacity { get; set; }
        public string Damage { get; set; }
        public string Critical { get; set; }
        public int Usage { get; set; }
    }
}