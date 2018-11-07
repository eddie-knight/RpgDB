namespace RpgDB
{
    [System.Serializable]
    public class Armor : RpgDBObject, IRpgDBEntry
    {
      public int EAC_Bonus { get; set; }
      public int KAC_Bonus { get; set; }
      public int Maximum_Dex_Bonus { get; set; }
      public int Armor_Check_Penalty { get; set; }
      public int Speed_Adjustment { get; set; }
      public int Upgrade_Slots { get; set; }
    }
}
