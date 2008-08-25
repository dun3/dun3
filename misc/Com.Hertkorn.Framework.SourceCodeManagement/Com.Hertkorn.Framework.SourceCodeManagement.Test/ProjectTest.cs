using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using Com.Hertkorn.Framework.SourceCodeManagement.Test.MockDaten;

namespace Com.Hertkorn.Framework.SourceCodeManagement.Test
{


    /// <summary>
    ///This is a test class for ProjectTest and is intended
    ///to contain all ProjectTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProjectTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod]
        public void TestLoadProject()
        {
            MockProject expectedProject = MockDatenGenerator.SAPLogonPadUpdater;

            FileInfo projectFile = new FileInfo(Properties.Settings.Default.BaseTestDataPath + "SAPLogonPadUpdater.csproj");
            Project project = new Project(projectFile);

            Assert.AreEqual(expectedProject.RawProjectPath, project.RawProjectPath);
            Assert.AreEqual(expectedProject.ProductVersion, project.ProductVersion);
            Assert.AreEqual(expectedProject.ProjectFile.FullName, project.ProjectFile.FullName);
            Assert.AreEqual(expectedProject.ProjectGuid, project.ProjectGuid);
            Assert.AreEqual(expectedProject.ProjectName, project.ProjectName);
            Assert.AreEqual(expectedProject.RawContent, project.RawContent);
            Assert.AreEqual(expectedProject.RootNameSpace, project.RootNameSpace);

            Assert.AreEqual<int>(expectedProject.AssemblyReferenceListe.Count, project.AssemblyReferenceListe.Count);
            AssemblyReferenceListeEquivalent(expectedProject.AssemblyReferenceListe, project.AssemblyReferenceListe);
            Assert.AreEqual<int>(expectedProject.ProjectReferenceListe.Count, project.ProjectReferenceListe.Count);
            ProjectReferenceListeEquivalent(expectedProject.ProjectReferenceListe, project.ProjectReferenceListe);
        }

        private void AssemblyReferenceListeEquivalent(IList<IAssemblyReference> expected, IList<IAssemblyReference> actual)
        {
            foreach (var expectedItem in expected)
            {
                IAssemblyReference actualItem;
                try
                {
                    actualItem = (from t in actual
                                  where t.RawHintPath == expectedItem.RawHintPath
                                  select t).Single();
                }
                catch (InvalidOperationException ex)
                {
                    Assert.Fail("Not a single item found " + ex.Message);
                    return;
                }
                Assert.AreEqual(expectedItem.RawHintPath, actualItem.RawHintPath);
                Assert.AreEqual(expectedItem.RawInclude, actualItem.RawInclude);
                Assert.AreEqual(expectedItem.RawReferencePath, actualItem.RawReferencePath);
                Assert.AreEqual<bool>(expectedItem.SpecificVersion, actualItem.SpecificVersion);
            }
        }

        private void ProjectReferenceListeEquivalent(IList<IProjectReferenceInfo> expected, IList<IProjectReferenceInfo> actual)
        {
            foreach (var expectedItem in expected)
            {
                IBaseProjectInfo actualItem;
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
            }
        }
    }
}
