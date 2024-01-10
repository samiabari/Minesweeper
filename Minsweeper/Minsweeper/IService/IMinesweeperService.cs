/// <author>Samia Bari</author>
/// <file>IMinesweeperService.cs</file>
/// <summary>
/// Description: This interface provides methods to start initially or resume the Minesweeper game.
/// </summary>
namespace Minsweeper.IService
{
    /// <summary>
    /// This interface provides methods to start initially or resume the Minesweeper game.
    /// </summary>
    public interface IMinesweeperService
    {
        /// <summary>
        /// Starts or resumes the Minesweeper game.
        /// </summary>
        void Start();

        /// <summary>
        /// Takes initial inputs to generate the game grid.
        /// This method collects user inputs required for generating the game grid, such as grid size
        /// and the number of mines to be placed on the grid.
        /// </summary>
        void InitialInput();
    }
}
