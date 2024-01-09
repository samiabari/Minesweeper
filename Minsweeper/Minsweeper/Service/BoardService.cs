using Minsweeper.IRepository;
using Minsweeper.IService;

namespace Minsweeper.Service
{
    public class BoardService : IBoardService
    {
        private int[,] grid;
        private bool[,] revealedSquare;
        private int gridSize;
        private int numberOfMines;
        private int mineLessSquare;
        private int userInputCount = 0;
        private int selectedRow;
        private int selectedCol;
        private readonly IBoardRepository boardRepository;
        private readonly InputValidatorService validator;

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

        public void Start()
        {
            string selectedSquare;
            while (mineLessSquare > 0)
            {
                DisplayGrid();
                userInputCount = 1;

                do
                {
                    Console.Write("\nSelect a square to reveal (e.g. A1): ");
                    selectedSquare = Console.ReadLine().ToUpper();
                } while (!validator.CheckValidSquareInput(selectedSquare, gridSize, revealedSquare));

                selectedRow = selectedSquare[0] - (char.IsUpper(selectedSquare[0]) ? 'A' : 'a');
                selectedCol = int.Parse(selectedSquare.Substring(1)) - 1;

                if (grid[selectedRow, selectedCol] == -1)
                {
                    userInputCount = 0;
                    Console.WriteLine("Oh no, you detonated a mine! Game over.");
                    return;
                }

                if (grid[selectedRow, selectedCol] != -1 && !revealedSquare[selectedRow, selectedCol])
                {
                    Console.WriteLine("This square contains " + grid[selectedRow, selectedCol] + " adjacent mines.");
                    RevealSquare(selectedRow, selectedCol);
                }
            }

            DisplayGrid();
            userInputCount = 0;
            Console.WriteLine("\nCongratulations, you have won the game!");
        }

        private void RevealSquare(int row, int col)
        {
            if (revealedSquare[row, col])
            {
                return; // If the square is already revealed.
            }

            revealedSquare[row, col] = true;
            mineLessSquare--;

            if (grid[row, col] == 0)
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

        private void DisplayGrid()
        {
            if (userInputCount == 0)
            {
                Console.WriteLine("\nHere is your minefield:");
            }
            else
            {
                Console.WriteLine("\nHere is your updated minefield:");
            }
            Console.Write("  ");

            for (int i = 1; i <= gridSize; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < gridSize; i++)
            {
                Console.Write((char)('A' + i) + " ");

                for (int j = 0; j < gridSize; j++)
                {
                    if (revealedSquare[i, j])
                    {
                        if (grid[i, j] == -1)
                        {
                            Console.Write("_ ");
                        }
                        else
                        {
                            Console.Write(grid[i, j] + " ");
                        }
                    }
                    else
                    {
                        Console.Write("_ ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
