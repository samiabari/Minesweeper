/// <author>Samia Bari</author>
/// <file>BoardService.cs</file>
/// <summary>
/// Description: This Service class implements IBoardService and provides method to start game play.
/// </summary>
namespace Minsweeper.Service
{
    using Minsweeper.IRepository;
    using Minsweeper.IService;

    /// <summary>
    /// This BoardService implements IBoardService and provides method to start game play by letting users to take square input.
    /// </summary>
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository boardRepository;
        private readonly InputValidatorService validator;
        private int[,] grid;
        private bool[,] revealedSquare;
        private int gridSize;
        private int numberOfMines;
        private int mineLessSquare;
        private int userInputCount = 0;
        private int selectedRow;
        private int selectedCol;

        public BoardService(int size, int mines, IBoardRepository repository)
        {
            gridSize = size;
            numberOfMines = mines;
            grid = new int[size, size];
            revealedSquare = new bool[size, size];
            mineLessSquare = size * size - mines;
            boardRepository = repository;
            validator = new InputValidatorService();

            boardRepository.CreateGrid(size, grid, revealedSquare);
            boardRepository.PlaceMines(grid, revealedSquare, numberOfMines);
        }

        /// <summary>
        /// This method contains the functionality of letting users to take square input.
        /// </summary>
        public void StartGame()
        {
            string selectedSquare;

            // Loop continues until all mine-less squares are revealed.
            while (mineLessSquare > 0)
            {
                DisplayGrid();
                userInputCount = 1;

                // Loop continues until user does valid square selection.
                do
                {
                    Console.Write("\nSelect a square to reveal (e.g. A1): ");
                    selectedSquare = Console.ReadLine().ToUpper();
                } while (!validator.CheckValidSquareInput(selectedSquare, gridSize, revealedSquare));

                // Gets selected row and column from the given input. it support lowercase input.
                selectedRow = selectedSquare[0] - (char.IsUpper(selectedSquare[0]) ? 'A' : 'a');
                selectedCol = int.Parse(selectedSquare.Substring(1)) - 1;

                // Check if the selected square contains a mine.
                if (grid[selectedRow, selectedCol] == -1)
                {
                    RevealSquare(selectedRow, selectedCol);  // If a mine found it reveals the whole board.
                    DisplayGrid();  // Displays the board.
                    userInputCount = 0; //Resumes the game by setting in the initial stage.
                    Console.WriteLine("\nOh no, you detonated a mine! Game over.");
                    return;
                }

                // If not mines found and unrevealed then shows the square. and adjacent squares.

                if (grid[selectedRow, selectedCol] != -1 && !revealedSquare[selectedRow, selectedCol])
                {
                    Console.WriteLine("This square contains " + grid[selectedRow, selectedCol] + " adjacent mines.");
                    RevealSquare(selectedRow, selectedCol);
                }
            }

            // If all the squares revealed except the mines then the game resumes.
            DisplayGrid();
            userInputCount = 0;
            Console.WriteLine("\nCongratulations, you have won the game!");
        }

        /// <summary>
        /// This method contains the functionality to reveal selected square input.
        /// </summary>
        private void RevealSquare(int row, int col)
        {
            if (revealedSquare[row, col])
            {
                return; // If the square is already revealed.
            }

            revealedSquare[row, col] = true;  //sets the selected square as revealed.
            mineLessSquare--;

            // if 0 containing square is selected then reveals all the adjacent 0's until mine's adjucent squares found.
            if (grid[row, col] == 0)
            {
                // Setting the bounds to check mine adjacent squares.
                int lowerBoundRow = Math.Max(0, row - 1);
                int lowerBoundCol = Math.Max(0, col - 1);
                int upperBoundRow = Math.Min(gridSize - 1, row + 1);
                int upperBoundCol = Math.Min(gridSize - 1, col + 1);

                for (int i = lowerBoundRow; i <= upperBoundRow; i++)
                {
                    for (int j = lowerBoundCol; j <= upperBoundCol; j++)
                    {
                        RevealSquare(i, j);
                    }
                }
            }
            else if (grid[selectedRow, selectedCol] == -1) // If selected square is a mine then loops all the way to reveal the whole board.
            {
                int lowerBoundRow = Math.Max(0, row - 1);
                int lowerBoundCol = Math.Max(0, col - 1);
                int upperBoundRow = Math.Min(gridSize - 1, row + 1);
                int upperBoundCol = Math.Min(gridSize - 1, col + 1);

                for (int i = lowerBoundRow; i <= upperBoundRow; i++)
                {
                    for (int j = lowerBoundCol; j <= upperBoundCol; j++)
                    {
                        RevealSquare(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// This method contains the functionality to Display the grid.
        /// </summary>
        private void DisplayGrid()
        {
            // Check if the user is in the starting point of the game.
            if (userInputCount == 0)
            {
                Console.WriteLine("\nHere is your minefield:");
            }
            else
            {
                Console.WriteLine("\nHere is your updated minefield:");
            }
            Console.Write("  ");

            // Shows the column tag.
            for (int i = 1; i <= gridSize; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < gridSize; i++)
            {
                // Shows the row tag.
                Console.Write((char)('A' + i) + " ");

                for (int j = 0; j < gridSize; j++)
                {
                    // Checks if the selected matrix is revealed or not or if user selected the mine containing square.
                    if (revealedSquare[i, j] || grid[selectedRow, selectedCol] == -1)
                    {
                        // If user selected mine containing square then shows all the mine with M.
                        if (grid[i, j] == -1)
                        {
                            Console.Write("M ");
                        }
                        else
                        {
                            // shows the Value of the adjacent squares.
                            Console.Write(grid[i, j] + " ");
                        }
                    }
                    else
                    {
                        // Unselected squares denoted with '_'.
                        Console.Write("_ ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
