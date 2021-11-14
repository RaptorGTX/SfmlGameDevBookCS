using System;

namespace SfmlGameDevelopmentBook
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game game = new Game();
                game.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
