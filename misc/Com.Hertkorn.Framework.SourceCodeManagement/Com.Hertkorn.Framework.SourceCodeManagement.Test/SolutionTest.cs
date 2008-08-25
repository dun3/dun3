using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using System.IO;
using Com.Hertkorn.Framework.SourceCodeManagement.Test.MockDaten;

namespace Com.Hertkorn.Framework.SourceCodeManagement.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class SolutionTest
    {
        public SolutionTest() { }

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
        public void TestLoadSolution()
        {
            MockSolution expectedSolution = MockDatenGenerator.FacesSolution;

            FileInfo solutionFile = new FileInfo(Properties.Settings.Default.BaseTestDataPath + "Faces.Www.sln");
            Solution solution = new Solution(solutionFile);
            Assert.AreEqual(expectedSolution.RawSolutionPath, solution.RawSolutionPath);
            Assert.AreEqual(expectedSolution.RawContent, solution.RawContent);
            Assert.AreEqual<VisualStudioVersion>(expectedSolution.Version, solution.Version);

            CollectionAssert.AllItemsAreNotNull(new List<ISolutionProjectInfo>(solution.ProjectListe));
            Assert.AreEqual<int>(expectedSolution.ProjectListe.Count, solution.ProjectListe.Count);

            ProjectListeEquivalent(expectedSolution.ProjectListe, solution.ProjectListe);
        }

        private static void ProjectListeEquivalent(IList<ISolutionProjectInfo> expected, IList<ISolutionProjectInfo> actual)
        {
            foreach (var expectedItem in expected)
            {
                ISolutionProjectInfo actualItem;
                try
                {
                    actualItem = (from t in actual
                                  where t.ProjectGuid.Equals(expectedItem.ProjectGuid)
                                  select t).Single();
                }
                catch (InvalidOperationException ex)
                {
                    Assert.Fail("Not a single item found " + ex.Message);
                    return;
                }
                Assert.AreEqual(expectedItem.ProjectGuid, actualItem.ProjectGuid);
                Assert.AreEqual(expectedItem.ProjectName, actualItem.ProjectName);
                Assert.AreEqual(expectedItem.RawProjectPath, actualItem.RawProjectPath);
                Assert.AreEqual(expectedItem.ProjectTypeGuid, actualItem.ProjectTypeGuid);
            }
        }
    }
}
