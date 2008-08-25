using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using System.IO;

namespace Com.Hertkorn.Framework.SourceCodeManagement.Test.MockDaten
{
    public class MockProject : IProjectInfo
    {
        /// <summary>
        /// Initializes a new instance of the MockProject class.
        /// </summary>
        public MockProject(FileInfo projectFile, string rawContent, string productVersion, string rootNameSpace, IList<IProjectReferenceInfo> projectReferenceListe, IList<IAssemblyReference> assemblyReferenceListe, Guid projectGuid, string projectName, string rawProjectPath)
        {
            ProjectFile = projectFile;
            RawContent = rawContent;
            ProductVersion = productVersion;
            RootNameSpace = rootNameSpace;
            ProjectReferenceListe = projectReferenceListe;
            AssemblyReferenceListe = assemblyReferenceListe;
            ProjectGuid = projectGuid;
            ProjectName = projectName;
            RawProjectPath = rawProjectPath;
        }
        #region IProjectInfo Members

        public FileInfo ProjectFile { get; set; }

        public string RawContent { get; set; }

        public string ProductVersion { get; set; }

        public string RootNameSpace { get; set; }

        public IList<IProjectReferenceInfo> ProjectReferenceListe { get; set; }

        public IList<IAssemblyReference> AssemblyReferenceListe { get; set; }

        #endregion

        #region IBaseProjectInfo Members

        public Guid ProjectGuid { get; set; }

        public string ProjectName { get; set; }

        public string RawProjectPath { get; set; }

        #endregion

        public class ProjectReference : IProjectReferenceInfo
        {
            /// <summary>
            /// Initializes a new instance of the ProjectReference class.
            /// </summary>
            public ProjectReference(Guid projectGuid, string projectName, string rawProjectPath)
            {
                ProjectGuid = projectGuid;
                ProjectName = projectName;
                RawProjectPath = rawProjectPath;
            }

            #region IBaseProjectInfo Members

            public Guid ProjectGuid { get; set; }

            public string ProjectName { get; set; }

            public string RawProjectPath { get; set; }

            #endregion

            #region IReference Members

            public string RawReferencePath { get { return RawProjectPath; } }

            #endregion
        }

        public class AssemblyReference : IAssemblyReference
        {
            /// <summary>
            /// Initializes a new instance of the AssemblyReference class.
            /// </summary>
            public AssemblyReference(string rawHintPath, string rawInclude, bool specificVersion)
            {
                RawHintPath = rawHintPath;
                RawInclude = rawInclude;
                SpecificVersion = specificVersion;
            }

            #region IAssemblyReference Members

            public string RawHintPath { get; set; }

            public string RawInclude { get; set; }

            public bool SpecificVersion { get; set; }

            #endregion

            #region IReference Members

            string IReference.RawReferencePath
            {
                get { return RawHintPath; }
            }

            #endregion
        }
    }
}