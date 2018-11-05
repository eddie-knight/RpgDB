namespace RpgDB
{
    [System.Serializable]
    public class Ammunition : RpgDBObject, IRpgObject
    {
        public string Charges { get; set; }
        public string Special { get; set; }
    }
}
