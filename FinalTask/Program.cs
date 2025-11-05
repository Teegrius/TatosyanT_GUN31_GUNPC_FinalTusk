using FinalTask.Casino;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "🎰 Final Task Casino";

            var casino = new Casino.Casino();
            casino.StartGame();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
