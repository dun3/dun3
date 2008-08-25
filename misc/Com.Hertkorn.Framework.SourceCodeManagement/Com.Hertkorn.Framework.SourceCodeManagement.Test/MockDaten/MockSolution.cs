using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using System.IO;

namespace Com.Hertkorn.Framework.SourceCodeManagement.Test.MockDaten
{
    public class MockSolution : ISolutionInfo
    {
        /// <summary>
        /// Initializes a new instance of the MockSolution class.
        /// </summary>
        public MockSolution(FileInfo solutionFile, IList<ISolutionProjectInfo> projectListe, string rawContent, VisualStudioVersion version)
        {
            SolutionFile = solutionFile;
            ProjectListe = projectListe;
            RawContent = rawContent;
            Version = version;
        }
        #region IBaseSolutionInfo Members

        public IList<ISolutionProjectInfo> ProjectListe { get; set; }

        public string RawContent { get; set; }

        public VisualStudioVersion Version { get; set; }

        public string RawSolutionPath { get { return SolutionFile.FullName; } }

        #endregion

        public FileInfo SolutionFile { get; set; }

        public class Project : ISolutionProjectInfo
        {
            /// <summary>
            /// Initializes a new instance of the MockProject class.
            /// </summary>
            public Project(string projectName, string projectGuid, string projectTypeGuid, string rawProjectPath)
            {
                ProjectTypeGuid = new Guid(projectTypeGuid);
                ProjectGuid = new Guid(projectGuid);
                ProjectName = projectName;
                RawProjectPath = rawProjectPath;
            }
            #region IBaseProjectInfo Members

            public Guid ProjectTypeGuid { get; set; }

            public Guid ProjectGuid { get; set; }

            public string ProjectName { get; set; }

            public string RawProjectPath { get; set; }

            #endregion
        }
    }
}
