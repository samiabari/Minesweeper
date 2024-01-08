using System.Text.RegularExpressions;

namespace Minsweeper
{
    class Minsweeper
    {
        string pattern = @"^-?\d+$";
        int gridSize;
        int numberOfMines;
        public void StartGame()
        {
            while (true)
            {

                InitialInput();
                Board game = new Board(gridSize, numberOfMines);
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
            } while (!CheckValidIntNGridSize(input));
            
            do
            {
                Console.WriteLine("Enter the number of mines to place on the grid (maximum is 35% of the total squares): ");
                input = Console.ReadLine();
            } while (!CheckValidIntNMineNumber(input));

        }

        public bool CheckInputIsInt(string input)
        {
            bool result = true;
            if (String.IsNullOrEmpty(input))
            {
                result = false;
                Console.WriteLine("Incorrect input");

            }
            else if (!Regex.IsMatch(input, pattern))
            {
                result = false;
                Console.WriteLine("Incorrect input");

            }
            return result;

        }
        private bool CheckValidIntNGridSize(string input)
        {
            int size;
            if (CheckInputIsInt(input))
            {
                size = Convert.ToInt32(input);
                if (CheckValidGridSize(size))
                {
                    gridSize = size;
                    return true;
                }
                else {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        private bool CheckValidIntNMineNumber(string input)
        {
            int number;
            if (CheckInputIsInt(input))
            {
                number = Convert.ToInt32(input);
                if (CheckValidMineNumber(number))
                {
                    numberOfMines = number;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool CheckValidGridSize(int size)
        {
            if (size < 2)
            {
                Console.WriteLine("Minimum size of grid is 2.");
                return false;
            }
            else if (size > 10)
            {
                Console.WriteLine("Maximum size of grid is 10.");
                return false;
            }
            else {
                return true;
            }
        }

        private bool CheckValidMineNumber(int number)
        {
            int totalSquares = gridSize * gridSize;
            double maxMineDouble= totalSquares * (35.0 / 100);
            int maximumMineNumber = (int)Math.Round(maxMineDouble);
            if (maximumMineNumber < 1)
            {
                maximumMineNumber= 1;
            }
            
            if (number <= 0)
            {
                Console.WriteLine("There must be at least 1 mine.");
                return false;
            }
            else if (number > maximumMineNumber)
            {
                Console.WriteLine("Maximum number is 35% of total sqaures.");
                return false;
            }
            else
            {
                return true;
            }
        }

       
    }
}
