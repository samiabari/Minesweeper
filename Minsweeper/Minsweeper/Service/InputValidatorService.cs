/// <author>Samia Bari</author>
/// <file>InputValidatorService.cs</file>
/// <summary>
/// Description: This Class implements IInputValidatorService and provides method to validate user inputs.
/// </summary>
namespace Minsweeper.Service
{
    using System.Text.RegularExpressions;
    using Minsweeper.IService;

    /// <summary>
    /// This Class implements IInputValidatorService and provides method to validate user inputs.
    /// </summary>
    public class InputValidatorService : IInputValidatorService
    {
        private string pattern = @"^\d+$";
        private int gridSize;

        /// <summary>
        /// Checks if the input string corresponds to a valid square on the game grid.
        /// </summary>
        /// <param name="input">The user input to reveal the grid square (e.g., A1, B2).</param>
        /// <param name="gridSize">The size of the game grid.</param>
        /// <param name="revealedSquare">The grid to maintain revealed and unrevealed squares.</param>
        /// <returns>True if the input is a valid square, otherwise false.</returns>
        public bool CheckValidSquareInput(string input, int gridSize, bool[,] revealedSquare)
        {
            // checks given string is in valid format or not.
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

        /// <summary>
        /// Checks if the input string is an integer.
        /// </summary>
        /// <param name="input">The user input string.</param>
        /// <returns>True if the input is an integer, otherwise false.</returns>
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

        /// <summary>
        /// Checks the validity of a string input.
        /// </summary>
        /// <param name="input">The user input string.</param>
        /// <returns>True if the input is valid, otherwise false.</returns>
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

        /// <summary>
        /// Checks if the input string represents a valid grid size.
        /// </summary>
        /// <param name="input">The user input string for grid size.</param>
        /// <returns>True if the grid size input is valid, otherwise false.</returns>
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

        /// <summary>
        /// Checks if the provided size is a valid grid size for the game.
        /// </summary>
        /// <param name="size">The grid size.</param>
        /// <returns>True if the size is valid, otherwise false.</returns>
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

        /// <summary>
        /// Checks if the provided number of mines is valid for the given grid size.
        /// </summary>
        /// <param name="number">The number of mines.</param>
        /// <param name="gridSize">The size of the game grid.</param>
        /// <returns>True if the number of mines is valid, otherwise false.</returns>
        public bool CheckValidMineNumber(int mineNumber, int gridSize)
        {
            int totalSquares = gridSize * gridSize;
            double maxMineDouble = totalSquares * (35.0 / 100);
            int maximumMineNumber = (int)Math.Round(maxMineDouble);
            if (maximumMineNumber < 1)
            {
                maximumMineNumber = 1;
            }

            if (gridSize < 0)
            {
                return false;
            }

            if (mineNumber <= 0)
            {
                Console.WriteLine("There must be at least 1 mine.");
                return false;
            }
            else if (mineNumber > maximumMineNumber)
            {
                Console.WriteLine("Maximum number is 35% of total squares.");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Checks if the input string is a valid number of mines for the grid.
        /// </summary>
        /// <param name="input">The user input for the number of mines.</param>
        /// <returns>True if the input for number of mines is valid, otherwise false.</returns>
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
    }
}
