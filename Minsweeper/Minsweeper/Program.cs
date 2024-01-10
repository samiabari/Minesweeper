namespace Minsweeper
{
    using Minsweeper.IService;
    using Minsweeper.Service;

    internal class Program
    {
        static void Main(string[] args)
        {
            // Calling game to start
            IMinesweeperService game = new MinesweeperService();
            game.Start();
        }
    }
}
