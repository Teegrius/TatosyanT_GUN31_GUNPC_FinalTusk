using System;
using System.Collections.Generic;
using FinalTask.Core.Models;
using FinalTask.Casino;

namespace FinalTask.Games.Blackjack
{
    public class BlackjackGame : CasinoGameBase
    {
        private readonly int _numOfCards;
        private Queue<Card> _deck;

        public BlackjackGame(int numOfCards)
        {
            if (numOfCards <= 0) throw new ArgumentException("Number of cards must be > 0");
            _numOfCards = numOfCards;
            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            var cards = new List<Card>();
            var suits = Enum.GetValues(typeof(CardSuit));
            var ranks = Enum.GetValues(typeof(CardRank));

            foreach (CardSuit suit in suits)
            {
                foreach (CardRank rank in ranks)
                {
                    cards.Add(new Card(suit, rank));
                }
            }

            Shuffle(cards);
        }

        private void Shuffle(List<Card> cards)
        {
            var rnd = new Random();
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
            _deck = new Queue<Card>(cards);
        }

        public override void PlayGame()
        {
            int playerScore = DrawCard() + DrawCard();
            int computerScore = DrawCard() + DrawCard();

            Console.WriteLine($"Your score: {playerScore}, Computer score: {computerScore}");

            if (playerScore > computerScore)
                OnWinInvoke();
            else if (playerScore < computerScore)
                OnLooseInvoke();
            else
                OnDrawInvoke();
        }

        private int DrawCard()
        {
            if (_deck.Count == 0) throw new InvalidOperationException("Deck is empty");
            return (int)_deck.Dequeue().Rank;
        }
    }
}
