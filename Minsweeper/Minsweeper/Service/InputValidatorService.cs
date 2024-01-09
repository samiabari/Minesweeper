using Minsweeper.IService;
using System.Text.RegularExpressions;

namespace Minsweeper.Service
{
    public class InputValidatorService : IInputValidatorService
    {
        private string pattern = @"^\d+$";
        private int gridSize;

        #region Checking Square Selection Validity
        public bool CheckValidSquareInput(string input, int gridSize, bool[,] revealedSquare)
        {
            if (!CheckString(input))
            {
                return false;
            }

            int row = input[0] - (char.IsUpper(input[0]) ? 'A' : 'a');
            int col = int.Parse(input.Substring(1)) - 1;

            if (row < 0 || row >= gridSize || col < 0 || col >= gridSize)
            {
                Console.WriteLine("Incorrect input, out of grid size.");
                return false;
            }

            if (revealedSquare[row, col])
            {
                Console.WriteLine("Already revealed square.");
                return false;
            }

            return true;
        }

        #endregion

        #region input type format validation
        public bool CheckInputIsInt(string input)
        {
            bool result = true;
            if (string.IsNullOrEmpty(input))
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

        public bool CheckString(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length > 1 && input.Length < 4 && char.IsLetter(input[0]))
                {
                    string numberPart = input.Substring(1);
                    if (CheckInputIsInt(numberPart))
                    {
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input.");
                }
            }
            else
            {
                Console.WriteLine("Incorrect input.");
            }
            return false;
        }

        #endregion

        #region grid Validation
        public bool CheckValidGridSizeInput(string input)
        {
            int size;
            if (CheckInputIsInt(input))
            {
                size = Convert.ToInt32(input);
                if (CheckValidGridSize(size))
                {
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

        public bool CheckValidGridSize(int size)
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
            else
            {
                gridSize = size;
                return true;
            }
        }

        #endregion

        #region Number of mines input Validations
        public bool CheckValidMineNumber(int number, int gridSize)
        {
            int totalSquares = gridSize * gridSize;
            double maxMineDouble = totalSquares * (35.0 / 100);
            int maximumMineNumber = (int)Math.Round(maxMineDouble);
            if (maximumMineNumber < 1)
            {
                maximumMineNumber = 1;
            }

            if (number <= 0)
            {
                Console.WriteLine("There must be at least 1 mine.");
                return false;
            }
            else if (number > maximumMineNumber)
            {
                Console.WriteLine("Maximum number is 35% of total squares.");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckValidMineNumberInput(string? input)
        {
            int number;
            if (CheckInputIsInt(input))
            {
                number = Convert.ToInt32(input);
                if (CheckValidMineNumber(number, gridSize))
                {
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

        #endregion
    }
}
