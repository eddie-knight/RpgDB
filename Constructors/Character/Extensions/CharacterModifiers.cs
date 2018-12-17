namespace RpgDB
{
    public sealed class CharacterModifiers
    {
        // TODO: Compile any permenant and temporary modifiers
        public int EAC { get; set; }
        public int KAC { get; set; }
        public int Fortitude { get; set; }
        public int Reflex { get; set; }
        public int Will { get; set; }
        public int Melee { get; set; }
        public int Ranged { get; set; }
        public int Thrown { get; set; }
        public int Initiative { get; set; }

        // Skills should only have a single modifier applied
        public Skills Skills = new Skills();
    }
}