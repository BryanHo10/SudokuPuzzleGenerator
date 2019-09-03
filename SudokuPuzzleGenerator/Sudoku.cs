using System;
using System.Collections.Generic;

namespace SudokuPuzzleGenerator
{
    public class Sudoku
    {
        private int RowSize,ColSize;
        public List<List<int>> PuzzleGrid;
        
        public Sudoku()
        {
            RowSize = ColSize = 9;
            PuzzleGrid = new List<List<int>>();
        }
        public void printPuzzle()
        {
            for(int Column_Position = 0; Column_Position < ColSize; Column_Position++)
            {
                foreach(List<int> cellVal in PuzzleGrid)
                {
                    Console.Write($" {cellVal[Column_Position]} ");
                }
                Console.WriteLine();
            }
        }
        public 

    }
}
