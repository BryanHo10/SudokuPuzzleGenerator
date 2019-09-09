using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLibrary
{
    public class Check
    {
        Sudoku CurrentSolution;
        public Check()
        {

        }
        public Check(Sudoku puzzle)
        {
            CurrentSolution = puzzle;
        }
        /// <summary>
        /// Checks validity of cell values in puzzle
        /// </summary>
        /// <returns></returns>
        public bool CheckAllSync()
        {
            foreach(List<Cell> ColumnSet in CurrentSolution.Grid)
            {
                foreach(Cell CellSpace in ColumnSet)
                {
                    if (!CheckColumn(CellSpace))
                        return false;
                    if (!CheckRow(CellSpace))
                        return false;
                    if (!CheckNeighbor(CellSpace))
                        return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Checks Validity of Cell Values Asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CheckAllAsync()
        {
            return true;
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
