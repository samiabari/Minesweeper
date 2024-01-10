/// <author>Samia Bari</author>
/// <file>IInputValidatorService.cs</file>
/// <summary>
/// Description: This interface provides method to validate user inputs.
/// </summary>
namespace Minsweeper.IService
{
    /// <summary>
    /// This interface provides method to validate user inputs.
    /// </summary>
    public interface IInputValidatorService
    {
        /// <summary>
        /// Checks if the input string corresponds to a valid square on the game grid.
        /// </summary>
        /// <param name="input">The user input to reveal the grid square (e.g., A1, B2).</param>
        /// <param name="gridSize">The size of the game grid.</param>
        /// <param name="revealedSquare">The grid to maintain revealed and unrevealed squares.</param>
        /// <returns>True if the input is a valid square, otherwise false.</returns>
        bool CheckValidSquareInput(string input, int gridSize, bool[,] revealedSquare);

        /// <summary>
        /// Checks if the input string is an integer.
        /// </summary>
        /// <param name="input">The user input string.</param>
        /// <returns>True if the input is an integer, otherwise false.</returns>
        bool CheckInputIsInt(string input);

        /// <summary>
        /// Checks the validity of a string input.
        /// </summary>
        /// <param name="input">The user input string.</param>
        /// <returns>True if the input is valid, otherwise false.</returns>
        bool CheckString(string input);

        /// <summary>
        /// Checks if the input string represents a valid grid size.
        /// </summary>
        /// <param name="input">The user input string for grid size.</param>
        /// <returns>True if the grid size input is valid, otherwise false.</returns>
        bool CheckValidGridSizeInput(string input);

        /// <summary>
        /// Checks if the provided size is a valid grid size for the game.
        /// </summary>
        /// <param name="size">The grid size.</param>
        /// <returns>True if the size is valid, otherwise false.</returns>
        bool CheckValidGridSize(int size);

        /// <summary>
        /// Checks if the provided number of mines is valid for the given grid size.
        /// </summary>
        /// <param name="number">The number of mines.</param>
        /// <param name="gridSize">The size of the game grid.</param>
        /// <returns>True if the number of mines is valid, otherwise false.</returns>
        bool CheckValidMineNumber(int number, int gridSize);

        /// <summary>
        /// Checks if the input string is a valid number of mines for the grid.
        /// </summary>
        /// <param name="input">The user input for the number of mines.</param>
        /// <returns>True if the input for number of mines is valid, otherwise false.</returns>
        bool CheckValidMineNumberInput(string? input);
    }
}
