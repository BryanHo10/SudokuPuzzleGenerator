using System;
using System.Collections.Generic;
using System.IO;

namespace SudokuLibrary
{
    public class Sudoku
    {
        private const int ROWSIZE = 9;
        private const int COLSIZE = 9;
        public List<List<Cell>> Grid;
        
        public Sudoku()
        {
            Cell[] defaultVal = new Cell[9];
            Grid = new List<List<Cell>>(9);
            for(int i = 0;i<ROWSIZE;i++)
            {
                Grid.Add(new List<Cell>(defaultVal));
            }
        }
        /// <summary>
        /// Outputs Grid contents to console
        /// </summary>
        public void PrintPuzzle()
        {
            for(int colPosition = 0; colPosition < COLSIZE; colPosition++)
            {
                foreach(List<Cell> cellVal in Grid)
                {
                    Console.Write($" {cellVal[colPosition].Value} ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Attempts to assign a custom Sudoku grid (9 row and 9 column)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool TryImportPuzzle(List<List<Cell>> input)
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
                Grid[rowPosition].Add(new Cell(Int32.Parse(inputString[i])));
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
                foreach (List<Cell> cellVal in Grid)
                {
                    output+=($"{cellVal[colPosition].Value}, ");
                }
            }
            File.WriteAllText(filePath,output);
        }

    }
}
