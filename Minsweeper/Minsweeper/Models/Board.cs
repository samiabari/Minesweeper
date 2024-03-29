﻿namespace Minsweeper.Models
{
    public class Board
    {
        public int[,] Grid { get; set; }

        public bool[,] RevealedSquare { get; set; }

        public int GridSize { get; set; }

        public int NumberOfMines { get; set; }

        public int MineLessSquare { get; set; }

        public int UserInputCount { get; set; }

        public int SelectedRow { get; set; }

        public int SelectedCol { get; set; }

        public Board()
        {

        }

        public Board(int gridSize)
        {
            GridSize = gridSize;
            Grid = new int[gridSize, gridSize];
            RevealedSquare = new bool[gridSize, gridSize];
        }
    }
}
