using Minsweeper.IService;
using Minsweeper.Service;

namespace Minsweeper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Calling game to start
            IMinesweeperService game = new MinesweeperService();
            game.StartGame();
        }
    }
}
