using Minsweeper.IRepository;
using Minsweeper.IService;
using Minsweeper.Repository;

namespace Minsweeper.Service
{
    public class MinesweeperService : IMinesweeperService
    {
        private readonly InputValidatorService validator;
        private int gridSize;
        private int numberOfMines;

        public MinesweeperService()
        {
            validator = new InputValidatorService();
        }

        public void StartGame()
        {
            while (true)
            {
                InitialInput();
                IBoardRepository boardRepo = new BoardRepository();
                IBoardService game = new BoardService(gridSize, numberOfMines, boardRepo);
                game.Start();

                Console.WriteLine("Press any key to play again...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void InitialInput()
        {
            string input;
            Console.WriteLine("Welcome to Minesweeper!");
            do
            {
                Console.WriteLine("Enter the size of the grid (e.g. 4 for a 4x4 grid): ");
                input = Console.ReadLine();
            } while (!validator.CheckValidGridSizeInput(input));

            gridSize = Convert.ToInt32(input);

            do
            {
                Console.WriteLine("Enter the number of mines to place on the grid (maximum is 35% of the total squares): ");
                input = Console.ReadLine();
            } while (!validator.CheckValidMineNumberInput(input));

            numberOfMines = Convert.ToInt32(input);
        }
    }
}
