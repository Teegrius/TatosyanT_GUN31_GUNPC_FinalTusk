using System;

namespace FinalTask.Core.Models
{
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(int number, int min, int max)
            : base($"Wrong dice number {number}. Must be between {min} and {max}.")
        {
        }
    }
}
