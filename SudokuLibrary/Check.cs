using SudokuLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLibrary
{
    public class Check
    {
        private Sudoku CurrentSolution;

        public Check(Sudoku puzzle)
        {
            CurrentSolution = puzzle;
        }
        /// <summary>
        /// Checks validity of cell values in puzzle
        /// </summary>
        /// <returns></returns>
        public bool CheckAllCells()
        {
            foreach(List<Cell> ColumnSet in CurrentSolution.Grid)
            {
                foreach(Cell CellSpace in ColumnSet)
                {
                    if (!CheckColumn(CellSpace))
                        return false;
                    else if (!CheckRow(CellSpace))
                        return false;
                    else if (!CheckNeighbor(CellSpace))
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Returns all invalid cells
        /// </summary>
        /// <returns></returns>
        public List<Cell> GetInvalidCellsSync()
        {
            List<Cell> InvalidCells = new List<Cell>();
            foreach (List<Cell> ColumnSet in CurrentSolution.Grid)
            {
                foreach (Cell CellSpace in ColumnSet)
                {
                    if (!CheckColumn(CellSpace))
                        InvalidCells.Add(CellSpace);
                    else if (!CheckRow(CellSpace))
                        InvalidCells.Add(CellSpace);
                    else if (!CheckNeighbor(CellSpace))
                        InvalidCells.Add(CellSpace);
                }
            }
            return InvalidCells;
        }
        /// <summary>
        /// REturns invalid cells asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cell>> GetInvalidCellsAsync()
        {
            List<Cell> InvalidCells = new List<Cell>();
            List<Task<Cell>> tasks = new List<Task<Cell>>();
            foreach (List<Cell> ColumnSet in CurrentSolution.Grid)
            {
                foreach (Cell CellSpace in ColumnSet)
                {
                    tasks.Add(Task.Run(() => CheckCurrentCell(CellSpace)));
                }
            }
            var results = await Task.WhenAll(tasks);
            foreach (Cell item in results)
            {
                InvalidCells.Add(item);
            }
            return InvalidCells;
        }
        private Cell CheckCurrentCell(Cell CurrentCell)
        {
            if (!CheckColumn(CurrentCell))
                return CurrentCell;
            else if (!CheckRow(CurrentCell))
                return CurrentCell;
            else if (!CheckNeighbor(CurrentCell))
                return CurrentCell;
            return null;
        }
        public bool CheckRow(Cell CurrentCell)
        {
            List<int> AllowedNums = new List<int> {1,2,3,4,5,6,7,8,9 };
            foreach(List<Cell> ColumnSet in CurrentSolution.Grid)
            {
                int CellValue = ColumnSet[CurrentCell.RowPosition].Value;
                if (AllowedNums.Contains(CellValue))
                {
                    AllowedNums.Remove(CellValue);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckColumn(Cell CurrentCell)
        {
            List<int> AllowedNums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (Cell cell in CurrentSolution.Grid[CurrentCell.ColumnPosition])
            {
                if (AllowedNums.Contains(cell.Value))
                {
                    AllowedNums.Remove(cell.Value);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckNeighbor(Cell CurrentCell)
        {
            List<int> AllowedNums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int startRow = (CurrentCell.RowPosition / 3) * 3;
            int startCol = (CurrentCell.ColumnPosition / 3) * 3;

            for (int row = startRow; row < startRow + 3; row++)
            {

                for (int col = startCol; col < startCol + 3; col++)
                {
                    int cellValue = CurrentSolution.Grid[col][row].Value;
                    if (AllowedNums.Contains(cellValue))
                    {
                        AllowedNums.Remove(cellValue);
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            return true;
        }

    }
}
