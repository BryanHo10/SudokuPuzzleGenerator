using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuLibrary
{
    public class Generator
    {
        private Sudoku CurrentSolution;
        private const int ROWSIZE = 9;
        private const int COLSIZE = 9;

        Sudoku Puzzle;
        int EmptyCells;
        public Generator()
        {
            Puzzle = new Sudoku();
        }

        public Sudoku CreateNewSolution()
        {
            Puzzle = PopulateCells();

            return Puzzle;
        }
        private Sudoku PopulateCells()
        {
            Sudoku randomizedGrid = new Sudoku();
            List<int> allowedValues = new List<int> {1,2,3,4,5,6,7,8,9 };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    allowedValues = Shuffle(allowedValues);
                    int count = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        for (int h = 0; h < 3; h++)
                        {
                            randomizedGrid.Grid[(3*j)+k][(3*i)+h] = allowedValues[count];
                            count++;
                        }
                    }
                }
            }
            return randomizedGrid;
        }
        private void SortGrid()
        {

        }
        /// <summary>
        /// Will attempt to remove values from the solution.
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        public bool TryRemoveValues(int cells)
        {
            if (EmptyCells + cells >= 81)
                return false;
            RemoveValues(cells);
            return true;
        }
        /// <summary>
        /// Sets random cells in the grid to 0.
        /// </summary>
        /// <param name="cells"></param>
        private void RemoveValues(int cells)
        {
            Random rng = new Random();
            int counter = 0;
            while (counter < cells)
            {
                int randColIndex = rng.Next(0, 9);
                int randRowIndex = rng.Next(0, 9);
                if(Puzzle.Grid[randColIndex][randRowIndex] != 0)
                {
                    Puzzle.Grid[randColIndex][randRowIndex] = 0;
                    counter++;
                }                
            }
            EmptyCells += cells;
        }
        public List<T> Shuffle<T>(List<T> list)
        {
            Random rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
    }
}
