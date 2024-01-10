/// <author>Samia Bari</author>
/// <file>MinesweeperService.cs</file>
/// <summary>
/// Description: This MinesweeperService class implements and provides methods to start initially or resume the Minesweeper game.
/// </summary>
namespace Minsweeper.Service
{
    using Minsweeper.IRepository;
    using Minsweeper.IService;
    using Minsweeper.Repository;

    /// <summary>
    /// This MinesweeperService class implements and provides methods to start initially or resume the Minesweeper game.
    /// </summary>
    public class MinesweeperService : IMinesweeperService
    {
        private readonly InputValidatorService validator;
        private int gridSize;
        private int numberOfMines;

        public MinesweeperService()
        {
            validator = new InputValidatorService();
        }

        /// <summary>
        /// Starts or resumes the Minesweeper game.
        /// </summary>
        public void Start()
        {
            // Loops to through the game.
            while (true)
            {
                InitialInput();
                IBoardRepository boardRepo = new BoardRepository();
                IBoardService game = new BoardService(gridSize, numberOfMines, boardRepo);
                game.StartGame();

                Console.WriteLine("Press any key to play again...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        /// <summary>
        /// Takes initial inputs to generate the game grid.
        /// This method collects user inputs required for generating the game grid, such as grid size
        /// and the number of mines to be placed on the grid.
        /// </summary>
        public void InitialInput()
        {
            string input;
            Console.WriteLine("Welcome to Minesweeper!");

            // Loops until user doesn't give valid input.
            do
            {
                Console.WriteLine("Enter the size of the grid (e.g. 4 for a 4x4 grid): ");
                input = Console.ReadLine();
            } while (!validator.CheckValidGridSizeInput(input));

            gridSize = Convert.ToInt32(input);

            // Loops until user doesn't give valid input.
            do
            {
                Console.WriteLine("Enter the number of mines to place on the grid (maximum is 35% of the total squares): ");
                input = Console.ReadLine();
            } while (!validator.CheckValidMineNumberInput(input));

            numberOfMines = Convert.ToInt32(input);
        }
    }
}
