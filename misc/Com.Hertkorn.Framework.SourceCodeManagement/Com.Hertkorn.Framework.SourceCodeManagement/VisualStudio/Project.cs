using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio
{
    [DebuggerDisplay("{this.GetType().Name}: Name={ProjectName}, Guid={ProjectGuid}, ProjectRefs: {ProjectReferenceListe.Count}, AssemblyRefs: {AssemblyReferenceListe.Count}")]
    public class Project : IProjectInfo
    {
        private static XName ResolveXName(string name)
        {
            return XName.Get(name, "http://schemas.microsoft.com/developer/msbuild/2003");
        }

        public static readonly XName XNAME_PRODUCT_VERSION = ResolveXName("ProductVersion");
        public static readonly XName XNAME_PROJECT_GUID = ResolveXName("ProjectGuid");
        public static readonly XName XNAME_ROOT_NAMESPACE = ResolveXName("RootNamespace");
        public static readonly XName XNAME_ASSEMBLY_NAME = ResolveXName("AssemblyName");
        public static readonly XName XNAME_PROJECT_REFERENCE = ResolveXName("ProjectReference");
        public static readonly XName XNAME_ASSEMBLY_REFERENCE = ResolveXName("Reference");

        public Project(FileInfo projectFile)
        {
            m_projectFile = projectFile;

            using (StreamReader sr = ProjectFile.OpenText())
            {
                m_rawContent = sr.ReadToEnd();
            }

            XDocument doc = XDocument.Load(ProjectFile.FullName);

            parseProductVersion(doc);

            parseProjectGuid(doc);
            parseRootNameSpace(doc);
            parseAssemblyName(doc);
            parseProjectReference(doc);
            parseAssemblyReference(doc);
        }

        private void parseAssemblyReference(XDocument doc)
        {
            var assemblyReferenceListe = (from title in doc.Descendants(XNAME_ASSEMBLY_REFERENCE)
                                          select title);

            // <Reference Include="De.Hbv.Framework.LoggingAndExceptionHandling, Version=1.0.0.1, Culture=neutral, PublicKeyToken=76139e9454083dd5, processorArchitecture=MSIL">
            //   <SpecificVersion>False</SpecificVersion>
            //   <HintPath>..\..\..\..\..\assemblies\De.Hbv.Framework.LoggingAndExceptionHandling\1.0.0.1\De.Hbv.Framework.LoggingAndExceptionHandling.dll</HintPath>
            // </Reference>
            foreach (var item in assemblyReferenceListe)
            {
                XAttribute rawInclude = item.Attribute("Include");
                var hintPath = item.Descendants(ResolveXName("HintPath"));
                var specificVersion = item.Descendants(ResolveXName("SpecificVersion"));

                if ((hintPath.Count() == 1) && (specificVersion.Count() == 1))
                {
                    m_assemblyReferenceListe.Add(new Project.AssemblyReference(hintPath.Single().Value, rawInclude.Value, bool.Parse(specificVersion.Single().Value)));
                }
            }
        }

        private void parseProjectReference(XDocument doc)
        {
            IEnumerable<XElement> projectReferenceListe = (from title in doc.Descendants(XNAME_PROJECT_REFERENCE)
                                                           select title);

            // <ProjectReference Include="..\..\ServiceTypes\De.Hbv.Infrastructure.Faces.ServiceTypes.csproj">
            //   <Project>{BB68C16C-6E65-4953-82DD-7D117F724C84}</Project>
            //   <Name>De.Hbv.Infrastructure.Faces.ServiceTypes</Name>
            // </ProjectReference>
            foreach (XElement item in projectReferenceListe)
            {
                XAttribute projectPath = item.Attribute("Include");
                XElement name = item.Descendants(ResolveXName("Name")).Single();
                XElement projectGuid = item.Descendants(ResolveXName("Project")).Single();

                m_projectReferenceListe.Add(new Project.ProjectReference(new Guid(projectGuid.Value), name.Value, projectPath.Value));
            }
        }

        private void parseAssemblyName(XDocument doc)
        {
            m_assemblyName = (from title in doc.Descendants(XNAME_ASSEMBLY_NAME)
                              select title.Value).Single();
        }

        private void parseRootNameSpace(XDocument doc)
        {
            m_rootNameSpace = (from title in doc.Descendants(XNAME_ROOT_NAMESPACE)
                               select title.Value).Single();
        }

        private void parseProjectGuid(XDocument doc)
        {
            string guid = (from title in doc.Descendants(XNAME_PROJECT_GUID)
                           select title.Value).Single();
            m_projectGuid = new Guid(guid);
        }

        private void parseProductVersion(XDocument doc)
        {
            m_productVersion = (from title in doc.Descendants(XNAME_PRODUCT_VERSION)
                                select title.Value).Single();
        }


        #region IProjectInfo Members

        private string m_rawContent = string.Empty;
        public string RawContent
        {
            get { return m_rawContent; }
        }

        #endregion

        private FileInfo m_projectFile;
        public FileInfo ProjectFile
        {
            get { return m_projectFile; }
        }

        private List<IAssemblyReference> m_assemblyReferenceListe = new List<IAssemblyReference>();
        public IList<IAssemblyReference> AssemblyReferenceListe
        {
            get { return m_assemblyReferenceListe; }
        }

        private string m_productVersion = string.Empty;
        public string ProductVersion
        {
            get { return m_productVersion; }
        }

        private IList<IProjectReferenceInfo> m_projectReferenceListe = new List<IProjectReferenceInfo>();
        public IList<IProjectReferenceInfo> ProjectReferenceListe
        {
            get { return m_projectReferenceListe; }
            set { m_projectReferenceListe = value; }
        }

        private string m_rootNameSpace = string.Empty;
        public string RootNameSpace
        {
            get { return m_rootNameSpace; }
            set { m_rootNameSpace = value; }
        }

        #region IBaseProjectInfo Members

        private Guid m_projectGuid = Guid.Empty;
        public Guid ProjectGuid
        {
            get { return m_projectGuid; }
        }

        private string m_assemblyName = string.Empty;
        public string ProjectName
        {
            get { return m_assemblyName; }
        }

        public string RawProjectPath
        {
            get { return ProjectFile.FullName; }
        }

        #endregion

        // <Reference Include="De.Hbv.Framework.LoggingAndExceptionHandling, Version=1.0.0.1, Culture=neutral, PublicKeyToken=76139e9454083dd5, processorArchitecture=MSIL">
        //   <SpecificVersion>False</SpecificVersion>
        //   <HintPath>..\..\..\..\..\assemblies\De.Hbv.Framework.LoggingAndExceptionHandling\1.0.0.1\De.Hbv.Framework.LoggingAndExceptionHandling.dll</HintPath>
        // </Reference>
        private class AssemblyReference : IAssemblyReference
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

        // <ProjectReference Include="..\..\ServiceTypes\De.Hbv.Infrastructure.Faces.ServiceTypes.csproj">
        //   <Project>{BB68C16C-6E65-4953-82DD-7D117F724C84}</Project>
        //   <Name>De.Hbv.Infrastructure.Faces.ServiceTypes</Name>
        // </ProjectReference>
        private class ProjectReference : IProjectReferenceInfo
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

            string IReference.RawReferencePath { get { return RawProjectPath; } }

            #region IBaseProjectInfo Members

            public Guid ProjectGuid { get; set; }

            public string ProjectName { get; set; }

            public string RawProjectPath { get; set; }

            #endregion
        }
    }
}
