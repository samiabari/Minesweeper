/// <author>Samia Bari</author>
/// <file>BoardRepository.cs</file>
/// <summary>
/// Description: This Repository implements IBoardRepository and provides methods to create the game grid and place mines within it.
/// </summary>
namespace Minsweeper.Repository
{
    using Minsweeper.IRepository;
    using Minsweeper.Models;

    /// <summary>
    /// This Repository implements IBoardRepository.
    /// Create the board/grid by adding mines and adjacent square values for the game.
    /// </summary>
    public class BoardRepository : IBoardRepository
    {
        public Board _Board { get; set; }

        public BoardRepository()
        {
            _Board = new Board();
        }

        // Constructor to initialize the Board instance
        /// <summary>
        /// Creates the grid as per user's given grid size. and initialize it by 0 and setting unrevealed.
        /// </summary>
        /// <param name="size"> size of the grid. </param>
        /// <param name="grid"> two-dimensional array to represent the game board.</param>
        /// <param name="revealedSquare"> two-dimensional array to represent the revealed square, same size as the grid size. </param>
        public Board CreateGrid(Board board)
        {
            for (int i = 0; i < board.GridSize; i++)
            {
                for (int j = 0; j < board.GridSize; j++)
                {
                    // places 0 initially for all the squares and makes non revealed.
                    board.Grid[i, j] = 0;
                    board.RevealedSquare[i, j] = false;
                }
            }
            _Board = board;
            return _Board;
        }

        /// <summary>
        /// initially places the mines randomely and increase the values of adjacent squares.
        /// </summary>
        /// <param name="grid"> two-dimensional array to represent the game board. </param>
        /// <param name="revealedSquare"> two-dimensional array to represent the revealed square, same size as the grid size. </param>
        /// <param name="numberOfMines"> Number of mines given by the user. </param>
        public Board PlaceMines(Board board)
        {
            Random random = new Random();

            int gridSize = board.Grid.GetLength(0);

            int minesPlaced = 0;

            // loop continues until all the mines are placed randomly.
            while (minesPlaced < board.NumberOfMines)
            {
                int row = random.Next(gridSize);
                int col = random.Next(gridSize);

                if (board.Grid[row, col] != -1)
                {
                    // places -1 as the mines in the random grid[row, col].
                    board.Grid[row, col] = -1;

                    // after placing mine increases the values of adjacent squares.
                    this.IncrementAdjacentSquares(board, row, col);
                    minesPlaced++;
                }

            }
            _Board = board;
            return _Board;
        }

        /// <summary>
        /// Increments the count of adjacent squares containing mines in the game grid.
        /// </summary>
        /// <param name="grid">The game grid.</param>
        /// <param name="gridSize">The size grid.</param>
        /// <param name="row"> Row index of the square to check for adjacent mines.</param>
        /// <param name="col"> Column index of the square to check for adjacent mines.</param>
        private void IncrementAdjacentSquares(Board board, int row, int col)
        {
            // Bounds for adjacent squares
            int lowerBoundRow = Math.Max(0, row - 1);
            int lowerBoundCol = Math.Max(0, col - 1);
            int upperBoundRow = Math.Min(board.GridSize - 1, row + 1);
            int upperBoundCol = Math.Min(board.GridSize - 1, col + 1);

            // Iterate through adjacent squares and increment mine counts if not a mine (-1)
            for (int i = lowerBoundRow; i <= upperBoundRow; i++)
            {
                for (int j = lowerBoundCol; j <= upperBoundCol; j++)
                {
                    // Checks if the square is a mine or not, if not then increases its value
                    if (board.Grid[i, j] != -1)
                    {
                        board.Grid[i, j]++;
                    }
                }
            }
        }
    }
}
