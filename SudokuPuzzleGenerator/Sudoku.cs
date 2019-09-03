using System;
using System.Collections.Generic;
using System.IO;

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
        public void importPuzzle()
        {

        }
        public void importPuzzleFromFile(string filePath)
        {

        }
        public void exportPuzzle(string filePath)
        {
            string output = "";
            for (int Column_Position = 0; Column_Position < ColSize; Column_Position++)
            {
                foreach (List<int> cellVal in PuzzleGrid)
                {
                    output+=($"{cellVal[Column_Position]}, ");
                }
            }
            File.WriteAllText(filePath,output);
        }

    }
}
