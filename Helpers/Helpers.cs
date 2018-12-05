using System;
namespace RpgDB
{
    public class Helpers
    {
        private Random Random = new Random();

        public int RollDie(int die)
        {
            return Random.Next(0, die);
        }
    }
}
