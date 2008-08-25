using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Com.Hertkorn.Framework.SourceCodeManagement.Product;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using System.IO;

namespace Com.Hertkorn.Framework.SourceCodeManagement.Test
{
    /// <summary>
    /// Summary description for ProductSourceStructureTest
    /// </summary>
    [TestClass]
    public class ProductSourceStructureTest
    {
        public ProductSourceStructureTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            List<ISolutionInfo> solutions = new List<ISolutionInfo>();
            solutions.Add(new Solution(new FileInfo(@"C:\dev\Projects\Faces\Faces.Wpf.sln")));
            solutions.Add(new Solution(new FileInfo(@"C:\dev\Projects\Faces\Faces.Www.sln")));
            solutions.Add(new Solution(new FileInfo(@"C:\dev\Projects\SAPLogonPadUpdater\SAPLogonPadUpdater.sln")));
            ProductSourceStructure structure = new ProductSourceStructure(solutions);
            IList<string> liste = structure.UniqueDirectoryListe;
            List<string> sorted = new List<string>(structure.RootDirectoryListe);
            sorted.Sort();
        }
        //[TestMethod]
        //public void TestMethod2()
        //{
        //    FileInfo[] fis = (new DirectoryInfo("c:\\dev\\Projects")).GetFiles("*.sln", SearchOption.AllDirectories);

        //    List<ISolutionInfo> solutions = new List<ISolutionInfo>();
        //    foreach (var item in fis)
        //    {
        //        solutions.Add(new Solution(item));
        //    }
        //    ProductSourceStructure structure = new ProductSourceStructure(solutions);

        //    IList<string> liste = structure.UniqueDirectoryListe;
        //    List<string> sorted = new List<string>(structure.RootDirectoryListe);
        //    sorted.Sort();
        //}

        //[TestMethod]
        //public void TestMethod3()
        //{
        //    FileInfo[] fis = (new DirectoryInfo("c:\\dev\\Projects")).GetFiles("*.sln", SearchOption.AllDirectories);
        //    foreach (var item in fis)
        //    {

        //        List<ISolutionInfo> solutions = new List<ISolutionInfo>();
        //        solutions.Add(new Solution(item));
        //        ProductSourceStructure structure = new ProductSourceStructure(solutions);

        //        IList<string> liste = structure.UniqueDirectoryListe;
        //        List<string> sorted = new List<string>(structure.RootDirectoryListe);
        //        sorted.Sort();
        //        var s = (from t in sorted
        //                 where !t.StartsWith(@"c:\\dev\\")
        //                 select t);
        //        Assert.IsTrue(s.Count() == 0, "there were items referenced that where outside of c:\\dev in " + item.FullName, s);
        //    }
        //}

    }
}
