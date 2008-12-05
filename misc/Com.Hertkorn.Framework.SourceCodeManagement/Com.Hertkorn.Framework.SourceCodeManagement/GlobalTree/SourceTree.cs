using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;

namespace Com.Hertkorn.Framework.SourceCodeManagement.GlobalTree
{
    public class SourceTree : ISourceTreeRootDirectory
    {
        public SourceTree(DirectoryInfo root)
        {
            m_root = new Directory(root);
        }

        private SourceTree.Directory m_root;
        public ISourceTreeDirectory Root
        {
            get { return m_root; }
        }

        #region ISourceTreeDirectory Members

        public DirectoryInfo SystemDirectory
        {
            get { return m_root.SystemDirectory; }
        }

        public IList<ISourceTreeDirectory> ChildDirectoryListe
        {
            get { return m_root.ChildDirectoryListe; }
        }

        public IList<ISolutionInfo> FindSolutionAll()
        {
            return m_root.FindSolutionAll();
        }

        public ISourceTreeDirectory FindDirectoryBySolution(ISolutionInfo solution)
        {
            return m_root.FindDirectoryBySolution(solution);
        }

        #endregion

        private class Directory : ISourceTreeDirectory
        {
            public Directory(DirectoryInfo systemDirectory)
            {
                SystemDirectory = systemDirectory;
                m_childDirectoryListe = InitializeChildDirectoryListe(SystemDirectory);
            }

            private IList<SourceTree.Directory> InitializeChildDirectoryListe(DirectoryInfo systemDirectory)
            {
                List<SourceTree.Directory> childListe = new List<SourceTree.Directory>();
                foreach (var item in Filter(systemDirectory.GetDirectories()))
                {
                    childListe.Add(new SourceTree.Directory(item));
                }
                return childListe;
            }

            private IEnumerable<DirectoryInfo> Filter(DirectoryInfo[] directoryInfo)
            {
                var dirs = from dir in directoryInfo
                           where dir.Name != ".svn"
                           select dir;
                return dirs;
            }

            public DirectoryInfo SystemDirectory { get; private set; }

            private IList<SourceTree.Directory> m_childDirectoryListe;
            public IList<ISourceTreeDirectory> ChildDirectoryListe
            {
                get { return m_childDirectoryListe.Cast<ISourceTreeDirectory>().ToList(); }
            }
            //public IList<ISourceTreeDirectory> ChildDirectoryListe
            //{
            //    get
            //    {
            //        List<ISourceTreeDirectory> cast = new List<ISourceTreeDirectory>(m_childDirectoryListe.Count);
            //        foreach (var item in m_childDirectoryListe)
            //        {
            //            cast.Add(item);
            //        }
            //        return cast;
            //    }
            //}


            public IList<ISolutionInfo> FindSolutionAll()
            {
                List<ISolutionInfo> solutionListe = new List<ISolutionInfo>();
                foreach (var item in SystemDirectory.GetFiles("*.sln", SearchOption.AllDirectories))
                {
                    solutionListe.Add(new Solution(item));
                }
                return solutionListe;
            }

            public ISourceTreeDirectory FindDirectoryBySolution(ISolutionInfo solution)
            {
                return FindDirectoryByDirectoryInfo(this, solution.SolutionFile.Directory);
            }


            private ISourceTreeDirectory FindDirectoryByDirectoryInfo(ISourceTreeDirectory directory, DirectoryInfo directoryInfo)
            {
                throw new NotImplementedException();
            }
        }
    }
}
