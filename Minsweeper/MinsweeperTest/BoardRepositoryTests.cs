using Minsweeper.Models;
using Minsweeper.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinsweeperTest
{
    public class BoardRepositoryTests
    {
        [TestCase(1, 0)]
        [TestCase(0, 0)]
        [TestCase(0, 1)]
        public void IncrementAdjacentSquaresCorrectIncrement(int mineRow, int mineCol)
        {
            
            var boardRepository = new BoardRepository();
            var board = new Board { GridSize = 2, NumberOfMines = 1 };
            board.Grid = new int[,]
            {
            { 0, 0 },
            { 0, -1}
            };

            boardRepository.IncrementAdjacentSquares(board, mineRow, mineCol);           
            Assert.AreEqual(1, board.Grid[0, 0]);
            Assert.AreEqual(1, board.Grid[1, 0]);
            Assert.AreEqual(1, board.Grid[0, 1]);
            

        }


        [Test]
        public void CreateGridTest()
        {
       
            var boardRepository = new BoardRepository();
            var board = new Board(2);

            var result = boardRepository.CreateGrid(board);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.GridSize);
        }
    }
}
