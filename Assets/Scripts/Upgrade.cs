namespace RpgDB
{
    [System.Serializable]
    public class Upgrade : RpgDBObject, IRpgObject
    {
        public int Slots { get; set; }
        public string Armor_Type { get; set; }
    }
}
