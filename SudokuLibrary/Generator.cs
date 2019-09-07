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
            Puzzle = SortGrid(Puzzle);
            return Puzzle;
        }
        private Sudoku PopulateCells()
        {
            Sudoku randomizedGrid = new Sudoku();
            List<int> allowedValues = new List<int> { 1,2,3,4,5,6,7,8,9 };
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
                            randomizedGrid.Grid[(3*j)+k][(3*i)+h].Value = allowedValues[count];
                            count++;
                        }
                    }
                }
            }
            return randomizedGrid;
        }
        /// <summary>
        /// Handles Row and Column sorting
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Sudoku SortGrid(Sudoku input)
        {
            Sudoku sortingGrid = input;
            int CurrentRow = 0;
            int CurrentCol = 0;
            while (CurrentCol < COLSIZE && CurrentRow < ROWSIZE)
            {
                sortingGrid = SortRow(CurrentRow,sortingGrid);
                CurrentRow++;
                sortingGrid = SortColumn(CurrentCol,sortingGrid);
                CurrentCol++;
            }
            return sortingGrid;
        }
        private Sudoku SortRow(int RowPosition,Sudoku inputPuzzle)
        {
            List<int> RecordedValues = new List<int>();
            foreach(List<Cell> ColumnSet in inputPuzzle.Grid)
            {
                Cell currentCell = ColumnSet[RowPosition];
                if(currentCell.isSorted)
                {
                    RecordedValues.Add(currentCell.Value);
                }
                else if(!RecordedValues.Contains(currentCell.Value))
                {
                    ColumnSet[RowPosition].isSorted = true;
                    RecordedValues.Add(currentCell.Value);
                }
                else
                {

                }
            }
            return inputPuzzle;
        }
        private Sudoku SortColumn(int ColumnPosition,Sudoku inputPuzzle)
        {
            List<int> RecordedValues = new List<int>();
            foreach(Cell RowValue in inputPuzzle.Grid[ColumnPosition])
            {
                if(RowValue.isSorted)
                {
                    RecordedValues.Add(RowValue.Value);
                }
                else if(!RecordedValues.Contains(RowValue.Value))
                {
                    RowValue.isSorted = true;
                    RecordedValues.Add(RowValue.Value);
                }
                else
                {

                }

            }
            return inputPuzzle;
        }
        /// <summary>
        /// Swaps Duplicate value in current Row/Column with a unsorted unique value
        /// </summary>
        /// <param name="duplicate"></param>
        /// <param name="inputPuzzle"></param>
        /// <returns></returns>
        private Sudoku BoxAdjacentCellSwap(Cell duplicate, Sudoku inputPuzzle,ViewOrientation focus)
        {
            return inputPuzzle;
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
                if(Puzzle.Grid[randColIndex][randRowIndex].Value != 0)
                {
                    Puzzle.Grid[randColIndex][randRowIndex].Value = 0;
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
