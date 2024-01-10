/// <author>Samia Bari</author>
/// <file>BoardRepository.cs</file>
/// <summary>
/// Description: This Repository implements IBoardRepository and provides methods to create the game grid and place mines within it.
/// </summary>
namespace Minsweeper.Repository
{
    using Minsweeper.IRepository;

    /// <summary>
    /// This Repository implements IBoardRepository.
    /// Create the board/grid by adding mines and adjacent square values for the game.
    /// </summary>
    public class BoardRepository : IBoardRepository
    {
        /// <summary>
        /// Creates the grid as per user's given grid size. and initialize it by 0 and setting unrevealed.
        /// </summary>
        /// <param name="size"> size of the grid. </param>
        /// <param name="grid"> two-dimensional array to represent the game board.</param>
        /// <param name="revealedSquare"> two-dimensional array to represent the revealed square, same size as the grid size. </param>
        public void CreateGrid(int size, int[,] grid, bool[,] revealedSquare)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // places 0 initially for all the squares and makes non revealed.
                    grid[i, j] = 0;
                    revealedSquare[i, j] = false;
                }
            }
        }

        /// <summary>
        /// initially places the mines randomely and increase the values of adjacent squares.
        /// </summary>
        /// <param name="grid"> two-dimensional array to represent the game board. </param>
        /// <param name="revealedSquare"> two-dimensional array to represent the revealed square, same size as the grid size. </param>
        /// <param name="numberOfMines"> Number of mines given by the user. </param>
        public void PlaceMines(int[,] grid, bool[,] revealedSquare, int numberOfMines)
        {
            Random random = new Random();

            int gridSize = grid.GetLength(0);

            int minesPlaced = 0;

            // loop continues until all the mines are placed randomly.
            while (minesPlaced < numberOfMines)
            {
                int row = random.Next(gridSize);
                int col = random.Next(gridSize);

                if (grid[row, col] != -1)
                {
                    // places -1 as the mines in the random grid[row, col].
                    grid[row, col] = -1;

                    // after placing mine increases the values of adjacent squares.
                    this.IncrementAdjacentSquares(grid, gridSize, row, col);
                    minesPlaced++;
                }
            }
        }

        /// <summary>
        /// Increments the count of adjacent squares containing mines in the game grid.
        /// </summary>
        /// <param name="grid">The game grid.</param>
        /// <param name="gridSize">The size grid.</param>
        /// <param name="row"> Row index of the square to check for adjacent mines.</param>
        /// <param name="col"> Column index of the square to check for adjacent mines.</param>
        private void IncrementAdjacentSquares(int[,] grid, int gridSize, int row, int col)
        {
            // Bounds for adjacent squares
            int lowerBoundRow = Math.Max(0, row - 1);
            int lowerBoundCol = Math.Max(0, col - 1);
            int upperBoundRow = Math.Min(gridSize - 1, row + 1);
            int upperBoundCol = Math.Min(gridSize - 1, col + 1);

            // Iterate through adjacent squares and increment mine counts if not a mine (-1)
            for (int i = lowerBoundRow; i <= upperBoundRow; i++)
            {
                for (int j = lowerBoundCol; j <= upperBoundCol; j++)
                {
                    // Checks if the square is a mine or not, if not then increases its value
                    if (grid[i, j] != -1)
                    {
                        grid[i, j]++;
                    }
                }
            }
        }
    }
}
