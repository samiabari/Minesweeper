namespace Minsweeper.IRepository
{
    public interface IBoardRepository
    {
        void CreateGrid(int size, int[,] grid, bool[,] revealedSquare);
        void PlaceMines(int[,] grid, bool[,] revealedSquare, int numberOfMines);
    }
}
