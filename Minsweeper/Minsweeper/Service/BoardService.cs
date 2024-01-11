/// <author>Samia Bari</author>
/// <file>BoardService.cs</file>
/// <summary>
/// Description: This Service class implements IBoardService and provides method to start game play.
/// </summary>
namespace Minsweeper.Service
{
    using Minsweeper.IRepository;
    using Minsweeper.IService;
    using Minsweeper.Models;

    /// <summary>
    /// This BoardService implements IBoardService and provides method to start game play by letting users to take square input.
    /// </summary>
    public class BoardService : IBoardService
    {

        private readonly IBoardRepository boardRepository;
        private readonly InputValidatorService validator;
        public Board _Board;

        public BoardService(Board board, IBoardRepository repository)
        {
            _Board = board;
            _Board.UserInputCount = 0;
            _Board.Grid = new int[board.GridSize, board.GridSize];
            _Board.RevealedSquare = new bool[board.GridSize, board.GridSize];
            _Board.MineLessSquare = (board.GridSize * board.GridSize) - board.NumberOfMines;
            boardRepository = repository;
            validator = new InputValidatorService();

            _Board = boardRepository.CreateGrid(_Board);
            _Board = boardRepository.PlaceMines(_Board);
        }


        /// <summary>
        /// This method contains the functionality of letting users to take square input.
        /// </summary>
        public void StartTakingSquareInput()
        {
            string selectedSquare;

            // Loop continues until all mine-less squares are revealed.
            while (_Board.MineLessSquare > 0)
            {
                DisplayGrid();
                _Board.UserInputCount = 1;

                // Loop continues until user does valid square selection.
                do
                {
                    Console.Write("\nSelect a square to reveal (e.g. A1): ");
                    selectedSquare = Console.ReadLine().ToUpper();
                } while (!validator.CheckValidSquareInput(selectedSquare, _Board.GridSize, _Board.RevealedSquare));

                // Gets selected row and column from the given input. it support lowercase input.
                _Board.SelectedRow = selectedSquare[0] - (char.IsUpper(selectedSquare[0]) ? 'A' : 'a');
                _Board.SelectedCol = int.Parse(selectedSquare.Substring(1)) - 1;

                // Check if the selected square contains a mine.
                if (_Board.Grid[_Board.SelectedRow, _Board.SelectedCol] == -1)
                {
                    RevealSquare(_Board.SelectedRow, _Board.SelectedCol);  // If a mine found it reveals the whole board.
                    DisplayGrid();  // Displays the board.
                    _Board.UserInputCount = 0; //Resumes the game by setting in the initial stage.
                    Console.WriteLine("\nOh no, you detonated a mine! Game over.");
                    return;
                }

                // If not mines found and unrevealed then shows the square. and adjacent squares.

                if (_Board.Grid[_Board.SelectedRow, _Board.SelectedCol] != -1 && !_Board.RevealedSquare[_Board.SelectedRow, _Board.SelectedCol])
                {
                    Console.WriteLine("This square contains " + _Board.Grid[_Board.SelectedRow, _Board.SelectedCol] + " adjacent mines.");
                    RevealSquare(_Board.SelectedRow, _Board.SelectedCol);
                }
            }

            // If all the squares revealed except the mines then the game resumes.
            DisplayGrid();
            _Board.UserInputCount = 0;
            Console.WriteLine("\nCongratulations, you have won the game!");
        }

        /// <summary>
        /// This method contains the functionality to reveal selected square input.
        /// </summary>
        private void RevealSquare(int row, int col)
        {
            if (_Board.RevealedSquare[row, col])
            {
                return; // If the square is already revealed.
            }

            _Board.RevealedSquare[row, col] = true;  //sets the selected square as revealed.
            _Board.MineLessSquare--;

            // if 0 containing square is selected then reveals all the adjacent 0's until mine's adjucent squares found.
            if (_Board.Grid[row, col] == 0 || _Board.Grid[_Board.SelectedRow, _Board.SelectedCol] == -1)
            {
                // Setting the bounds to check mine adjacent squares.
                int lowerBoundRow = Math.Max(0, row - 1);
                int lowerBoundCol = Math.Max(0, col - 1);
                int upperBoundRow = Math.Min(_Board.GridSize - 1, row + 1);
                int upperBoundCol = Math.Min(_Board.GridSize - 1, col + 1);

                for (int i = lowerBoundRow; i <= upperBoundRow; i++)
                {
                    for (int j = lowerBoundCol; j <= upperBoundCol; j++)
                    {
                        RevealSquare(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// This method contains the functionality to Display the grid.
        /// </summary>
        private void DisplayGrid()
        {
            // Check if the user is in the starting point of the game.
            if (_Board.UserInputCount == 0)
            {
                Console.WriteLine("\nHere is your minefield:");
            }
            else
            {
                Console.WriteLine("\nHere is your updated minefield:");
            }
            Console.Write("  ");

            // Shows the column tag.
            for (int i = 1; i <= _Board.GridSize; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            for (int i = 0; i < _Board.GridSize; i++)
            {
                // Shows the row tag.
                Console.Write((char)('A' + i) + " ");

                for (int j = 0; j < _Board.GridSize; j++)
                {
                    // Checks if the selected matrix is revealed or not or if user selected the mine containing square.
                    if (_Board.RevealedSquare[i, j])
                    {
                        // If user selected mine containing square then shows all the mine with M.
                        if (_Board.Grid[i, j] == -1)
                        {
                            Console.Write("M ");
                        }
                        else
                        {
                            // shows the Value of the adjacent squares.
                            Console.Write(_Board.Grid[i, j] + " ");
                        }
                    }
                    else
                    {
                        // Unselected squares denoted with '_'.
                        Console.Write("_ ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
