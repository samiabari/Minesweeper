using Minsweeper.IRepository;

namespace Minsweeper.Repository
{
    public class BoardRepository : IBoardRepository
    {
        //board creation
        public void CreateGrid(int size, int[,] grid, bool[,] revealedSquare)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = 0;
                    revealedSquare[i, j] = false;
                }
            }
        }

        // mines setting based on number of mines given
        public void PlaceMines(int[,] grid, bool[,] revealedSquare, int numberOfMines)
        {
            Random random = new Random();

            int gridSize = grid.GetLength(0);

            int minesPlaced = 0;
            while (minesPlaced < numberOfMines)
            {
                int row = random.Next(gridSize);
                int col = random.Next(gridSize);

                if (grid[row, col] != -1)
                {
                    grid[row, col] = -1;
                    IncrementAdjacentSquares(grid, gridSize, row, col);
                    minesPlaced++;
                }
            }
        }

        // increasing value of adjacent squares so that we can reveal it later.
        private void IncrementAdjacentSquares(int[,] grid, int gridSize, int row, int col)
        {
            int lowerBoundRow = Math.Max(0, row - 1);
            int lowerBoundCol = Math.Max(0, col - 1);
            int upperBoundRow = Math.Min(gridSize - 1, row + 1);
            int upperBoundCol = Math.Min(gridSize - 1, col + 1);

            for (int i = lowerBoundRow; i <= upperBoundRow; i++)
            {
                for (int j = lowerBoundCol; j <= upperBoundCol; j++)
                {
                    if (grid[i, j] != -1)
                    {
                        grid[i, j]++;
                    }
                }
            }
        }
    }
}
