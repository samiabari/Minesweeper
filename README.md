# Minesweeper Game App
 The program creates a Console Minesweeper game with on a square grid based on user input for grid size and mine count. Users select squares to uncover, and if a mine is revealed, the game ends. Uncovered squares show numbers for adjacent mines. The game is won when all non-mine squares are uncovered. It also tracks user progress, displaying the minefield after each move.

# Language Used C# (.Net 7 Framework)

# Assumptions To solve the problem
1. Firstly Created the grid which the board the user will see. the grid is created as per user's given size. all the values of the grid is set to 0.
2. Another 2D boolean array taken to control the grid square is revealed or not. initially all of the squares are false. so user cannot see the values.
3. Mines are denoted as -1 to and it is places randomly using Random() C# class and all the adjacent squares of the mines are incremented by setting the upper and lower bound.
4. Squares are reveal if user selects a 0 containing square then then program will keep revealing the squares until it finds any squares that has adjacent mine. This is done using recursion to check adjacent unrevealed 0's.
5. If user choses a square with mine then the whole board should be shown with numbers and Mines denoted with 'M'. this is also done with recursion to reveal until all the squares are reveald.
6. If user selects all the squares without losing then the mines should be hidden but the numbers boxes should be revealed. This is done by checking in every step that the selected square has mine or not.  


# App Opening requirement
OS: Windows
IDE: Visual Studio

# Process to start the App
-- User need to open the Minsweeper.sln Solution file from the Visual Studio IDE by selecting Open project.
-- To Start the game In Visual Studio user need to click run.

