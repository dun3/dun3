using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio
{
    [DebuggerDisplay("{this.GetType().Name}: Path={RawSolutionPath}, Version={Version.ToString()}, Subprojects: {ProjectListe.Count}")]
    public class Solution : ISolutionInfo
    {
        private string m_rawContent = string.Empty;

        public string RawContent
        {
            get { return m_rawContent; }
        }

        private FileInfo m_solutionFile;

        public FileInfo SolutionFile
        {
            get { return m_solutionFile; }
        }
        public string RawSolutionPath
        {
            get { return m_solutionFile.FullName; }
        }

        public Solution(FileInfo solutionFile)
        {
            m_solutionFile = solutionFile;
            StreamReader sr = solutionFile.OpenText();
            m_rawContent = sr.ReadToEnd();
            parseRawContent(m_rawContent);
        }

        protected virtual void parseRawContent(string rawContent)
        {
            parseVersion(rawContent);
            parseProjectListe(rawContent);
        }

        public static readonly string GROUPNAME_VERSION = "version";
        public static readonly Regex PARSE_VERSION_REGEX = new Regex(@"Microsoft Visual Studio Solution File, Format Version (?<" + GROUPNAME_VERSION + ">[0-9\\.]+)", RegexOptions.Compiled | RegexOptions.Multiline);
        protected virtual void parseVersion(string rawContent)
        {
            MatchCollection mc = PARSE_VERSION_REGEX.Matches(rawContent);
            if (mc.Count == 1)
            {
                if (mc[0].Groups[GROUPNAME_VERSION].Value == "10.00")
                {
                    m_version = VisualStudioVersion.VS2008;
                }
            }
        }

        public static readonly string GROUPNAME_PROJECT_TYPE = "type";
        public static readonly string GROUPNAME_PROJECT_GUID = "guid";
        public static readonly string GROUPNAME_PROJECT_NAME = "name";
        public static readonly string GROUPNAME_PROJECT_PATH = "path";
        public static readonly string REGEX_MATCH_GUID = "\\{[a-zA-Z0-9\\-]{36}\\}";
        //Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "De.Hbv.Framework.Helper", "..\De.Hbv.Framework\Helper\Helper\De.Hbv.Framework.Helper.csproj", "{70EE87C0-4295-4681-97FB-1E9549549B8B}"
        public static readonly Regex PARSE_SOLUTION_REGEX
            = new Regex(
                "Project\\(\"(?<" + GROUPNAME_PROJECT_TYPE + ">" + REGEX_MATCH_GUID + ")\"\\) = "
                + "\"(?<" + GROUPNAME_PROJECT_NAME + ">[^\\\"]+)\", "
                + "\"(?<" + GROUPNAME_PROJECT_PATH + ">[^\\\"]+)\", "
                + "\"(?<" + GROUPNAME_PROJECT_GUID + ">" + REGEX_MATCH_GUID + ")\"", 
                RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Multiline);
        protected virtual void parseProjectListe(string rawContent)
        {
            List<ISolutionProjectInfo> projectInfoListe = new List<ISolutionProjectInfo>();
            MatchCollection mc = PARSE_SOLUTION_REGEX.Matches(rawContent);
            foreach (Match match in mc)
            {
#warning TODO: Warum hier 5!?
                if (match.Groups.Count == 5)
                {
                    projectInfoListe.Add(new ProjectSubItem
                    {
                        ProjectTypeGuid = new Guid(match.Groups[GROUPNAME_PROJECT_TYPE].Value),
                        ProjectName = match.Groups[GROUPNAME_PROJECT_NAME].Value,
                        RawProjectPath = match.Groups[GROUPNAME_PROJECT_PATH].Value,
                        ProjectGuid = new Guid(match.Groups[GROUPNAME_PROJECT_GUID].Value)
                    });
                }
            }
            m_projectListe = projectInfoListe;
        }

        private VisualStudioVersion m_version = VisualStudioVersion.Unknown;
        public VisualStudioVersion Version
        {
            get { return m_version; }
        }

        private List<ISolutionProjectInfo> m_projectListe = new List<ISolutionProjectInfo>();
        public IList<ISolutionProjectInfo> ProjectListe
        {
            get { return m_projectListe; }
        }

        private class ProjectSubItem : ISolutionProjectInfo
        {
            #region ISolutionProjectInfo Members

            public Guid ProjectTypeGuid { get; set; }

            public Guid ProjectGuid { get; set; }

            public string ProjectName { get; set; }

            public string RawProjectPath { get; set; }

            #endregion
        }
    }
}
