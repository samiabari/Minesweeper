namespace Minsweeper.IService
{
    public interface IInputValidatorService
    {
        bool CheckValidSquareInput(string input, int gridSize, bool[,] revealedSquare);
        bool CheckInputIsInt(string input);
        bool CheckString(string input);
        bool CheckValidGridSizeInput(string input);
        bool CheckValidGridSize(int size);
        bool CheckValidMineNumber(int number, int gridSize);
        bool CheckValidMineNumberInput(string? input);
    }
}
