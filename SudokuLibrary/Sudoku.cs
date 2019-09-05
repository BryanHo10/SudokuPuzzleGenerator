using System;
using System.Collections.Generic;
using System.IO;

namespace SudokuLibrary
{
    public class Sudoku
    {
        private const int ROWSIZE = 9;
        private const int COLSIZE = 9;
        public List<List<int>> Grid;
        
        public Sudoku()
        {
            int[] defaultVal = new int[9];
            Grid = new List<List<int>>(9);
            for(int i = 0;i<ROWSIZE;i++)
            {
                Grid.Add(new List<int>(defaultVal));
            }
        }
        public void PrintPuzzle()
        {
            for(int colPosition = 0; colPosition < COLSIZE; colPosition++)
            {
                foreach(List<int> cellVal in Grid)
                {
                    Console.Write($" {cellVal[colPosition]} ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Attempts to assign a custom Sudoku grid (9 row and 9 column)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool TryImportPuzzle(List<List<int>> input)
        {
            if(input.Count == 9 && input[0].Count == 9)
            {
                Grid = input;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Imports CSV values to the Sudoku grid from left to right, top to bottom.
        /// </summary>
        /// <param name="filePath"></param>
        public void ImportPuzzleFromFile(string filePath)
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
                Grid[rowPosition].Add(Int32.Parse(inputString[i]));
            }
        }
        /// <summary>
        /// Exports Sudoku grid from left to right, top to bottom to CSV format
        /// </summary>
        /// <param name="filePath"></param>
        public void ExportPuzzle(string filePath)
        {
            string output = "";
            for (int colPosition = 0; colPosition < COLSIZE; colPosition++)
            {
                foreach (List<int> cellVal in Grid)
                {
                    output+=($"{cellVal[colPosition]}, ");
                }
            }
            File.WriteAllText(filePath,output);
        }

    }
}
