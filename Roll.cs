using System;
namespace RpgDB
{
    public class Roll
    {
        private Random Random = new Random();

        public int d20()
        {
            return Random.Next(1, 21);
        }

        public int d10()
        {
            return Random.Next(1, 11);
        }

        public int d8()
        {
            return Random.Next(1, 9);
        }

        public int d6()
        {
            return Random.Next(1, 7);
        }

        public int d4()
        {
            return Random.Next(1, 5);
        }

        public int d2()
        {
            return Random.Next(1, 3);
        }
    }
}
