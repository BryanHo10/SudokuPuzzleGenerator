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
            Grid = new List<List<int>>();
        }
        public void printPuzzle()
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
        public void importPuzzle(List<List<int>> input)
        {
            Grid = input;
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
                Grid[rowPosition].Add(Int32.Parse(inputString[i]));
            }
        }
        public void exportPuzzle(string filePath)
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
