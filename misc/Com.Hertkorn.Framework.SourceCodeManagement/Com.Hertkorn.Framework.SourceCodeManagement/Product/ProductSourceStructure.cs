using System;
using System.Collections.Generic;
using System.Linq;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using NDepend.Helpers.FileDirectoryPath;

namespace Com.Hertkorn.Framework.SourceCodeManagement.Product
{
    public class ProductSourceStructure
    {
        public ProductSourceStructure(IList<ISolutionInfo> solutionListe)
        {
            SolutionListe = solutionListe;
            List<FilePathAbsolute> filePathListe = FindRelevantFiles(SolutionListe);
            InitializeUniqueDirectoryListe(filePathListe);
            InitializeRootDirectoryListe();
        }

        private List<FilePathAbsolute> FindRelevantFiles(IList<ISolutionInfo> SolutionListe)
        {
            List<FilePathAbsolute> filePathListe = new List<FilePathAbsolute>();
            foreach (var item in SolutionListe)
            {
                ParseSolutionItem(item, filePathListe);
            }
            return filePathListe;
        }

        private void InitializeUniqueDirectoryListe(List<FilePathAbsolute> filePathListe)
        {

            List<DirectoryPathAbsolute> listOfUniqueDirs;
            List<string> listOfUniqueFileNames;
            ListOfPathHelper.GetListOfUniqueDirsAndUniqueFileNames(filePathListe, out listOfUniqueDirs, out listOfUniqueFileNames);
            UniqueDirectoryListe = new List<string>();
            foreach (var item in listOfUniqueDirs)
            {
                UniqueDirectoryListe.Add(item.Path);
            }
        }

        private void InitializeRootDirectoryListe()
        {
            List<string> rootDirectoryListe = new List<string>(UniqueDirectoryListe);
            for (int i = 0; i < rootDirectoryListe.Count; i++)
            {
                var outerPath = rootDirectoryListe[i];
                // Make sure it ends in \ otherwise c:\testMe will falsely have the
                // root c:\test
                outerPath = EnsureEndsWithSlash(outerPath);
                
                for (int j = i + 1; j < rootDirectoryListe.Count; j++)
                {
                    var innerPath = rootDirectoryListe[j];
                    innerPath = EnsureEndsWithSlash(innerPath);

                    if (innerPath == null) continue; // kein pfad
                    if (outerPath == null) continue;
                    if (innerPath.StartsWith(outerPath))
                    {
                        // path outerPath is root of path innerPath
                        // --> mark path innerPath (j) as obsolete by setting to null
                        rootDirectoryListe[j] = null;
                    }
                    else if (outerPath.StartsWith(innerPath))
                    {
                        // path innerPath is root of path outerPath
                        // --> mark path outerPath (i) as obsolete by setting to null
                        rootDirectoryListe[i] = null;
                    }

                }
            }
            var nonNullListe = from s in rootDirectoryListe
                               where s != null
                               select s;
            RootDirectoryListe = new List<string>(nonNullListe);

        }

        private static string EnsureEndsWithSlash(string path)
        {
            if (!path.EndsWith("\\"))
            {
                path = path + "\\";
            }
            return path;
        }


        private static void ParseSolutionItem(ISolutionInfo item, List<FilePathAbsolute> filePathListe)
        {
            FilePathAbsolute solution = new FilePathAbsolute(item.SolutionFile.FullName);

            foreach (var projectInfo in item.ProjectListe)
            {
                ParseProjectInfo(projectInfo, solution, filePathListe);
            }

            filePathListe.Add(solution);
        }

        private static void ParseProjectInfo(ISolutionProjectInfo projectInfo, FilePathAbsolute baseDirectory, List<FilePathAbsolute> filePathListe)
        {
            string rawProjectPath = baseDirectory.ParentDirectoryPath.Path + @"\" + projectInfo.RawProjectPath;
            FilePathAbsolute absoluteProjectPath = new FilePathAbsolute(rawProjectPath);

            if (!absoluteProjectPath.Path.EndsWith(".csproj")) { return; }
            IProjectInfo project = new Project(absoluteProjectPath.FileInfo);

            foreach (var referenceInfo in project.ProjectReferenceListe)
            {
                try
                {
                    string rawReferencePath = absoluteProjectPath.ParentDirectoryPath.Path + @"\" + referenceInfo.RawReferencePath;
                    FilePathAbsolute absoluteReferencePath = new FilePathAbsolute(rawReferencePath);
                    filePathListe.Add(absoluteReferencePath);
                }
                catch (Exception) { }
            }
            foreach (var referenceInfo in project.AssemblyReferenceListe)
            {
                try
                {
                    string rawReferencePath = absoluteProjectPath.ParentDirectoryPath.Path + @"\" + referenceInfo.RawReferencePath;
                    FilePathAbsolute absoluteReferencePath = new FilePathAbsolute(rawReferencePath);
                    filePathListe.Add(absoluteReferencePath);
                }
                catch (Exception) { }
            }

            filePathListe.Add(absoluteProjectPath);
        }

        public IList<ISolutionInfo> SolutionListe { get; private set; }

        public IList<string> UniqueDirectoryListe { get; private set; }
        public IList<string> RootDirectoryListe { get; private set; }

    }
}
