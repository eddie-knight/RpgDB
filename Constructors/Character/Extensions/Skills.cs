namespace RpgDB
{
    public class Skills
    {
        public int Acrobatics;
        public int Athletics;
        public int Bluff;
        public int Computers;
        public int Culture;
        public int Diplomacy;
        public int Disguise;
        public int Engineering;
        public int Intimidate;
        public int Life_Science;
        public int Medicine;
        public int Mysticism;
        public int Perception;
        public int Physical_Science;
        public int Piloting;
        public int Profession_1;
        public int Profession_2;
        public int Sense_Motive;
        public int Slight_of_Hand;
        public int Stealth;
        public int Survival;

        public int Allocated()
        {
            return Acrobatics + Athletics + Bluff + Computers + Culture + 
                Diplomacy + Disguise + Engineering + Intimidate + Life_Science + 
                Medicine + Mysticism + Perception + Physical_Science + 
                Piloting + Profession_1 + Profession_2 + Sense_Motive + 
                Slight_of_Hand + Stealth + Survival;
        }
    }
}
