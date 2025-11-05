using System;
using FinalTask.Core.Interfaces;
using FinalTask.Core.Models;
using FinalTask.Core.Services;
using FinalTask.Games.Blackjack;
using FinalTask.Games.DiceGame;

namespace FinalTask.Casino
{
    public class Casino : IGame
    {
        private readonly ISaveLoadService<PlayerProfile> _saveLoadService;
        private PlayerProfile _player;
        private readonly string _savePath = "saves";

        private BlackjackGame _blackjack;
        private DiceGame _diceGame;

        public Casino()
        {
            _saveLoadService = new FileSystemSaveLoadService<PlayerProfile>(_savePath);
            _blackjack = new BlackjackGame(6);
            _diceGame = new DiceGame(2, 1, 6);
        }

        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("🎰 Welcome to Final Task Casino!");

            LoadOrCreateProfile();

            if (_player.Bank <= 0)
            {
                Console.WriteLine("No money? Kicked!");
                return;
            }

            Console.WriteLine($"\nHello, {_player.Name}! Your current balance: {_player.Bank}$");

            Console.WriteLine("\nChoose a game:");
            Console.WriteLine("1 - Blackjack (21)");
            Console.WriteLine("2 - Dice Game");
            Console.Write("\nEnter choice: ");

            var choice = Console.ReadLine();

            Console.Write("\nEnter your bet: ");
            if (!int.TryParse(Console.ReadLine(), out int bet) || bet <= 0 || bet > _player.Bank)
            {
                Console.WriteLine("Invalid bet! Returning to main menu.");
                return;
            }

            switch (choice)
            {
                case "1":
                    PlayBlackjack(bet);
                    break;
                case "2":
                    PlayDice(bet);
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

            SaveProfile();
            Console.WriteLine($"\nGoodbye, {_player.Name}!");
        }

        private void PlayBlackjack(int bet)
        {
            _blackjack.OnWin += () =>
            {
                Console.WriteLine("🎉 You win!");
                _player.Bank += bet;
            };
            _blackjack.OnLoose += () =>
            {
                Console.WriteLine("💀 You lost!");
                _player.Bank -= bet;
            };
            _blackjack.OnDraw += () =>
            {
                Console.WriteLine("🤝 Draw!");
            };

            _blackjack.PlayGame();
        }

        private void PlayDice(int bet)
        {
            _diceGame.OnWin += () =>
            {
                Console.WriteLine("🎉 You win!");
                _player.Bank += bet;
            };
            _diceGame.OnLoose += () =>
            {
                Console.WriteLine("💀 You lost!");
                _player.Bank -= bet;
            };
            _diceGame.OnDraw += () =>
            {
                Console.WriteLine("🤝 Draw!");
            };

            _diceGame.PlayGame();
        }

        private void LoadOrCreateProfile()
        {
            Console.Write("\nEnter your player name: ");
            var name = Console.ReadLine();

            try
            {
                _player = _saveLoadService.LoadData(name);
                Console.WriteLine("Profile loaded successfully.");
            }
            catch
            {
                Console.WriteLine("No profile found. Creating a new one...");
                _player = new PlayerProfile(name, 1000);
                _saveLoadService.SaveData(_player, name);
            }
        }

        private void SaveProfile()
        {
            _saveLoadService.SaveData(_player, _player.Name);
            Console.WriteLine("Profile saved successfully.");
        }
    }
}
