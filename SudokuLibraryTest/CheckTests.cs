using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuLibraryTest
{
    [TestClass]
    public class CheckTests
    {
        private Check checker;

        [TestInitialize]
        public void SetupTest()
        {
            Generator testSol = new Generator();
            checker = new Check(testSol.CreateNewSolution());
        }
        [TestMethod]
        public void TestCheckRow()
        {

        }
    }
}
