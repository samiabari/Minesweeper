using Minsweeper.Models;

/// <author>Samia Bari</author>
/// <file>IBoardRepository.cs</file>
/// <summary>
/// Description: This interface provides methods to create the game grid and place mines within it.
/// </summary>
namespace Minsweeper.IRepository
{
    /// <summary>
    /// Interface to create the board/grid by adding mines and adjacent square values for the Minesweeper game.
    /// </summary>
    public interface IBoardRepository
    {
        /// <summary>
        /// Creates the grid as per user's given grid size. and initialize it by 0 and setting unrevealed.
        /// </summary>
        /// <param name="size"> size of the grid. </param>
        /// <param name="grid"> two-dimensional array to represent the game board.</param>
        /// <param name="revealedSquare"> two-dimensional array to represent the revealed square, same size as the grid size. </param>
        Board CreateGrid(Board board);

        /// <summary>
        /// initially places the mines randomely and increase the values of adjacent squares.
        /// </summary>
        /// <param name="grid"> two-dimensional array to represent the game board. </param>
        /// <param name="revealedSquare"> two-dimensional array to represent the revealed square, same size as the grid size. </param>
        /// <param name="numberOfMines"> Number of mines given by the user. </param>
        Board PlaceMines(Board board);
    }
}
