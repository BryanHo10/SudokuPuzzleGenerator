using SudokuLibrary.Models;
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
                SortRow(CurrentRow,sortingGrid);
                CurrentRow++;
                SortColumn(CurrentCol,sortingGrid);
                CurrentCol++;
            }
            return sortingGrid;
        }
        private void SortRow(int RowPosition,Sudoku inputPuzzle)
        {
            List<int> RecordedValues = new List<int>();
            foreach(List<Cell> ColumnSet in inputPuzzle.Grid)
            {
                Cell currentCell = ColumnSet[RowPosition];
                if (currentCell.Value == RecordedValues[RecordedValues.Count - 1])
                {
                    TryHotSwitch(currentCell, RecordedValues, ViewOrientation.ROW);
                    // Do the changes effect the inputPuzzle ... How does that effect the foreach loop?
                }
                if (currentCell.isSorted)
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
            
        }
        private void SortColumn(int ColumnPosition,Sudoku inputPuzzle)
        {
            List<int> RecordedValues = new List<int>();
            foreach(Cell RowValue in inputPuzzle.Grid[ColumnPosition])
            {
                if (RowValue.Value == RecordedValues[RecordedValues.Count-1])
                {
                    TryHotSwitch(RowValue, RecordedValues, ViewOrientation.COLUMN);
                    // Do the changes effect the inputPuzzle ... How does that effect the foreach loop?
                }
                else if(RowValue.isSorted)
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
        }
        /// <summary>
        /// Attempts to switch Cell values if same value has been previously sorted. 
        /// </summary>
        /// <param name="duplicate"></param>
        /// <param name="restrictions"></param>
        /// <param name="focus"></param>
        private bool TryHotSwitch(Cell duplicate, List<int> restrictions, ViewOrientation focus)
        {
            
            if (focus == ViewOrientation.COLUMN)
            {
                int startRow = (duplicate.RowPosition / 3) * 3;
                
                for (int rowIndex = duplicate.RowPosition;rowIndex < startRow + 3; rowIndex++)
                {
                    Cell CandidateCell = Puzzle.Grid[duplicate.ColumnPosition][rowIndex];
                    if (!restrictions.Contains(CandidateCell.Value) && CandidateCell.isSorted)
                    {
                        int tempVal = CandidateCell.Value;
                        CandidateCell.Value = duplicate.Value;
                        duplicate.Value = tempVal;

                        return true;
                    }
                }
            }
            else
            {
                int startCol = (duplicate.ColumnPosition / 3) * 3;
                for (int colIndex = duplicate.RowPosition; colIndex < startCol + 3; colIndex++)
                {
                    Cell CandidateCell = Puzzle.Grid[colIndex][duplicate.RowPosition];
                    if (!restrictions.Contains(CandidateCell.Value) && CandidateCell.isSorted)
                    {
                        int tempVal = CandidateCell.Value;
                        CandidateCell.Value = duplicate.Value;
                        duplicate.Value = tempVal;

                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Swaps Duplicate value in current Row/Column with a unsorted unique value
        /// </summary>
        /// <param name="duplicate"></param>
        /// <param name="inputPuzzle"></param>
        /// <returns></returns>
        private Sudoku BoxAdjacentCellSwap(Cell duplicate, Sudoku inputPuzzle,ViewOrientation focus)
        {
            if(focus == ViewOrientation.ROW)
            {

            }
            else
            {

            }

            return inputPuzzle;
        }
        private List<Cell> SearchBox(Cell duplicate, ViewOrientation focus, Sudoku inputPuzzle)
        {
            List<Cell> UnregisteredCells = new List<Cell>();
            int startRow = (duplicate.RowPosition / 3) * 3;
            int startCol = (duplicate.ColumnPosition / 3) * 3;

            for(int row = startRow;row < startRow + 3; row++)
            {
                if(focus == ViewOrientation.ROW && duplicate.RowPosition == row)
                {
                    continue;
                }

                for(int col = startCol; col < startCol + 3; col++)
                {
                    if (focus == ViewOrientation.COLUMN && duplicate.ColumnPosition == col)
                    {
                        continue;
                    }
                    else
                    {
                        UnregisteredCells.Add(inputPuzzle.Grid[col][row]);
                    }
                }
            }
            return UnregisteredCells;
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
        private List<T> Shuffle<T>(List<T> list)
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
