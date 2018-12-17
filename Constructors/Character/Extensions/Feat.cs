namespace RpgDB
{
    public class Feat : RpgDBExtension
    {
        public int modifier_id { get; set; }
        public string Tagline { get; set; }
        public string Prerequisite_Text { get; set; }
        public string Benefit { get; set; }
        public string Extra_Text { get; set; }
        public int Combat_Feat { get; set; }
    };
}
