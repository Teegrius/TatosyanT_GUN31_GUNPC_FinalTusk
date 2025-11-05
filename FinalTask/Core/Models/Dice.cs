using System;

namespace FinalTask.Core.Models
{
    public struct Dice
    {
        private readonly int _min;
        private readonly int _max;
        private static readonly Random Random = new Random();

        public int Number => Random.Next(_min, _max + 1);

        public Dice(int min, int max)
        {
            if (min < 1 || max < min || max > int.MaxValue)
                throw new WrongDiceNumberException(min, 1, max);

            _min = min;
            _max = max;
        }
    }
}
