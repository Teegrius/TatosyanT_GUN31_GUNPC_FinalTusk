using System;
using System.Collections.Generic;
using FinalTask.Core.Models;
using FinalTask.Casino;

namespace FinalTask.Games.DiceGame
{
    public class DiceGame : CasinoGameBase
    {
        private readonly List<Dice> _dices = new();
        private readonly int _numDices;

        public DiceGame(int numDices, int min, int max)
        {
            if (numDices <= 0) throw new ArgumentException("Number of dices must be > 0");
            _numDices = numDices;

            for (int i = 0; i < numDices; i++)
                _dices.Add(new Dice(min, max));

            FactoryMethod();
        }

        protected override void FactoryMethod() { }

        public override void PlayGame()
        {
            int playerSum = Roll();
            int computerSum = Roll();

            Console.WriteLine($"Your sum: {playerSum}, Computer sum: {computerSum}");

            if (playerSum > computerSum)
                OnWinInvoke();
            else if (playerSum < computerSum)
                OnLooseInvoke();
            else
                OnDrawInvoke();
        }

        private int Roll()
        {
            int sum = 0;
            foreach (var dice in _dices)
                sum += dice.Number;
            return sum;
        }
    }
}
