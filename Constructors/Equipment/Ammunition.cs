namespace RpgDB
{
    [System.Serializable]
    public class Ammunition : RpgDBObject, IRpgDBEntry
    {
        public string Charges { get; set; }
        public string Special { get; set; }
    }
}
