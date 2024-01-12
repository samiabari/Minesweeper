using Minsweeper.IRepository;
using Minsweeper.IService;
using Minsweeper.Service;
using Minsweeper.TestHelpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinsweeperTest
{
    [TestFixture]
    public class MinesweeperServiceTests
    {
        [Test]
        public void StartTest()
        {
            var minesweeperServiceMock = new Mock<IMinesweeperService>();
            minesweeperServiceMock.Setup(repo => repo.Start());
            minesweeperServiceMock.Object.Start();
            minesweeperServiceMock.Verify(repo => repo.Start(), Times.Once);
        }


        [Test]
        public void InitialInputTest()
        {
            var minesweeperServiceMock = new Mock<MinesweeperService> { CallBase = true };

            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupSequence(x => x.ReadLine())
                .Returns("4")  // Grid size
                .Returns("2"); // Number of mines

            minesweeperServiceMock.Object.Console = consoleMock.Object;
            var result = minesweeperServiceMock.Object.InitialInput();
            Assert.AreEqual(4, result.GridSize);
            Assert.AreEqual(2, result.NumberOfMines);
        }
    }
}
