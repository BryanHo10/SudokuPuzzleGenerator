using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuLibrary
{
    public class Cell
    {
        public int Value;
        public int RowPosition, ColumnPosition;
        public bool isSorted;
        public Cell()
        {
            Value = 0;
        }
        public Cell(int num)
        {
            Value = num;
        }

    }
}
