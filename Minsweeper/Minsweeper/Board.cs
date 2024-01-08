namespace Minsweeper
{
    class Board
    {
        private int[,] grid;
        private bool[,] revealedSquare;
        private int gridSize;
        private int numberOfMines;
        private int mineLessSquare;
        private int userInputCount = 0;
        private int selectedRow;
        private int selectedCol;
        public Board(int size, int mines)
        {
            gridSize = size;
            numberOfMines = mines;
            grid = new int[size, size];
            revealedSquare = new bool[size, size];
            mineLessSquare = size * size - mines;

            CreateGrid();
            PlaceMines();
        }

        private void CreateGrid()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = 0;
                    revealedSquare[i, j] = false;
                }
            }
        }

        private void PlaceMines()
        {
            Random random = new Random();

            int minesPlaced = 0;
            while (minesPlaced < numberOfMines)
            {
                int x = random.Next(gridSize);
                int y = random.Next(gridSize);

                if (grid[x, y] != -1)
                {
                    grid[x, y] = -1;
                    IncrementAdjacentSquares(x, y);
                    minesPlaced++;
                }
            }
        }

        private void IncrementAdjacentSquares(int x, int y)
        {
            int startX = Math.Max(0, x - 1);
            int startY = Math.Max(0, y - 1);
            int endX = Math.Min(gridSize - 1, x + 1);
            int endY = Math.Min(gridSize - 1, y + 1);

            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (grid[i, j] != -1)
                    {
                        grid[i, j]++;
                    }
                }
            }
        }

        public void Start()
        {
            string selectedSquare;
            while (mineLessSquare > 0)
            {

                DisplayGrid();
                userInputCount = 1;
                
                do {
                    Console.Write("\nSelect a square to reveal (e.g. A1): ");
                    selectedSquare = Console.ReadLine().ToUpper();
                } while (!CheckValidSquareInput(selectedSquare));
                
                if (revealedSquare[selectedRow, selectedCol])
                {
                    Console.WriteLine("This square has already been uncovered.");
                    continue;
                }

                if (grid[selectedRow, selectedCol] == -1)
                {
                    userInputCount = 0;
                    Console.WriteLine("Oh no, you detonated a mine! Game over.");
                    return;
                }

                RevealSquare(selectedRow, selectedCol);
            }
           
            DisplayGrid();
            userInputCount = 0;
            Console.WriteLine("\nCongratulations, you have won the game!");
        }

        private bool CheckValidSquareInput(string input)
        {
            if (input.Length != 2 || !char.IsLetter(input[0]) || !char.IsDigit(input[1]))
            {
                Console.WriteLine("Incorrect input.");
                return false;
            }
            else {
                int x = input[0] - 'A';
                int y = input[1] - '1';

                if (x < 0 || x >= gridSize || y < 0 || y >= gridSize)
                {
                    Console.WriteLine("Incorrect input, Out of grid Size");
                    return false;
                }
                else {
                    selectedRow = x;
                    selectedCol = y;
                    return true;
                }
            }
        }

        

        private void RevealSquare(int x, int y)
        {
            if (revealedSquare[selectedRow, selectedCol])
            {
                return;
            }

            revealedSquare[x, y] = true;
            
            if (grid[x, y] != -1)
            {
                Console.WriteLine("This square contains " + grid[x, y] + " adjacent mines.");
            }
            
            mineLessSquare--;

            if (grid[x, y] == 0)
            {
                int startX = Math.Max(0, x - 1);
                int startY = Math.Max(0, y - 1);
                int endX = Math.Min(gridSize - 1, x + 1);
                int endY = Math.Min(gridSize - 1, y + 1);

                for (int i = startX; i <= endX; i++)
                {
                    for (int j = startY; j <= endY; j++)
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
                            Console.Write("* ");
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
