using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuPuzzleGenerator
{
    class Generator
    {
        private Sudoku CurrentSolution;

        Sudoku Puzzle;
        int EmptyCells;
        public Generator()
        {
            Puzzle = new Sudoku();
        }

        public void createNewSolution()
        {

        }
        /// <summary>
        /// Will attempt to remove values from the solution.
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        public bool tryRemoveValues(int cells)
        {
            if (EmptyCells + cells >= 81)
                return false;
            removeValues(cells);
            return true;
        }
        /// <summary>
        /// Sets random cells in the grid to 0.
        /// </summary>
        /// <param name="cells"></param>
        private void removeValues(int cells)
        {
            int counter = 0;
            Random random = new Random();
            while (counter < cells)
            {
                int randColIndex = random.Next(0, 9);
                int randRowIndex = random.Next(0, 9);
                if(Puzzle.Grid[randColIndex][randRowIndex] != 0)
                {
                    Puzzle.Grid[randColIndex][randRowIndex] = 0;
                    counter++;
                }                
            }
            EmptyCells += cells;
        }
    }
}
