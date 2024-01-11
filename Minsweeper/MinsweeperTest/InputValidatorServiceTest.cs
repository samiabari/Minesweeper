using Minsweeper.Service;

namespace MinsweeperTest
{
    public class InputValidatorServiceTest
    {
        private InputValidatorService validator;
        [SetUp]
        public void Setup()
        {
            validator = new InputValidatorService();
        }

        [Test]
        public void CheckValidSquareInput_Validations_True()
        {
            bool result = validator.CheckValidSquareInput("A1", 3, new bool[3, 3]);
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckValidSquareInput_Validations_False1()
        {
            bool result = validator.CheckValidSquareInput("1", 3, new bool[3, 3]);
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckValidSquareInput_Validations_False2()
        {
            bool result = validator.CheckValidSquareInput("A", 3, new bool[3, 3]);
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckValidSquareInput_Validations_False3()
        {
            bool[,] revealed = new bool[3, 3];
            revealed[0, 0] = true;
            bool result = validator.CheckValidSquareInput("A1", 3, revealed);
            Assert.IsFalse(result);
        }


        [TestCase("123", ExpectedResult = true)]
        [TestCase("0", ExpectedResult = true)]
        [TestCase("-5", ExpectedResult = false)] // Negative number
        [TestCase("", ExpectedResult = false)] // Empty string
        [TestCase("abc", ExpectedResult = false)] // Non-numeric
        [TestCase("A1", ExpectedResult = false)] // Non-numeric
        [TestCase("!a", ExpectedResult = false)] // Non-numeric
        public bool CheckInputIsInt_Validation(string input)
        {
            // Act
            bool result = validator.CheckInputIsInt(input);

            // Assert
            return result;
        }

        [TestCase("A1", ExpectedResult = true)]
        [TestCase("1", ExpectedResult = false)]  // Incorrect input
        [TestCase("1A", ExpectedResult = false)]  // Incorrect input
        [TestCase("A0", ExpectedResult = true)]  // Incorrect input
        [TestCase("4", ExpectedResult = false)]   // Incorrect input
        [TestCase("B10", ExpectedResult = true)] // Incorrect input
        [TestCase("C", ExpectedResult = false)] // Incorrect input
        [TestCase("", ExpectedResult = false)] // Incorrect input
        public bool CheckString_Validations(string input)
        {
            // Act
            bool result = validator.CheckString(input);

            // Assert
            return result;
        }


        [TestCase("3", ExpectedResult = true)]
        [TestCase("10", ExpectedResult = true)]
        [TestCase("", ExpectedResult = false)] // Incorrect input, non numeric
        [TestCase("A1", ExpectedResult = false)] // Incorrect input, non numeric 
        [TestCase("1A", ExpectedResult = false)] // Incorrect input, non numeric
        [TestCase("1", ExpectedResult = false)] // Below minimum size
        [TestCase("15", ExpectedResult = false)] // Above maximum size
        [TestCase("abc", ExpectedResult = false)] // Non-numeric
        public bool CheckValidGridSizeInput_Validations(string input)
        {
            bool result = validator.CheckValidGridSizeInput(input);
            return result;
        }

        [TestCase(-1, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(10, ExpectedResult = true)]
        [TestCase(11, ExpectedResult = false)]
        public bool CheckValidGridSize_Validations(int size)
        {
            bool result = validator.CheckValidGridSize(size);
            return result;
        }


        [TestCase(5, 5, ExpectedResult = true)]
        [TestCase(0, 3, ExpectedResult = false)] // Zero mines
        [TestCase(10, 8, ExpectedResult = true)] // Exceeds maximum percentage
        [TestCase(4, 3, ExpectedResult = false)] // Negative number
        [TestCase(-5, 6, ExpectedResult = false)] // Negative number
        [TestCase(5, -6, ExpectedResult = false)] // Negative number
        public bool CheckValidMineNumber_Validations(int mineNumber, int gridSize)
        {
            bool result = validator.CheckValidMineNumber(mineNumber, gridSize);
            return result;
        }


        [TestCase("abc", ExpectedResult = false)] // Non-numeric
        [TestCase("", ExpectedResult = false)] // Non-numeric
        [TestCase("A1", ExpectedResult = false)] // Non-numeric
        [TestCase("1A", ExpectedResult = false)] // Non-numeric
        public bool CheckValidMineNumberInput_Validations(string input)
        {
            bool result = validator.CheckValidMineNumberInput(input);
            return result;
        }
    }
}