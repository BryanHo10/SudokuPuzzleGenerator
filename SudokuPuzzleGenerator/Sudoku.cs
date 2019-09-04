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
        public void importPuzzle(List<List<int>> input)
        {
            PuzzleGrid = input;
        }
        public void importPuzzleFromFile(string filePath)
        {
            
            char[] separator = { ',', ' ' };
            string[] inputString = File.ReadAllText(filePath).Split(separator);
            int rowPosition = -1;

            for(int i = 0; i < inputString.Length; i++)
            {
                if(i % 9 == 0)
                {
                    rowPosition += 1;
                }
                PuzzleGrid[rowPosition].Add(Int32.Parse(inputString[i]));
            }
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
