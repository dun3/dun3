using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using Com.Hertkorn.Framework.SourceCodeManagement.SourceTree;
using System.IO;

namespace Com.Hertkorn.Framework.SourceCodeManagement.Vcs
{
    public class VcsSolution : IVcsSolutionInfo
    {
        public VcsSolution(ISolutionInfo solution)
        {
            Solution = solution;
        }

        #region IVcsSolutionInfo Members

        public ILocalSolutionStructure MinimalCheckoutStructure
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        public ISolutionInfo Solution { get; private set; }

        #region ISolutionInfo Members

        FileInfo ISolutionInfo.SolutionFile
        {
            get { return Solution.SolutionFile; }
        }

        string IBaseSolutionInfo.RawSolutionPath
        {
            get { return Solution.RawSolutionPath; }
        }

        IList<ISolutionProjectInfo> IBaseSolutionInfo.ProjectListe
        {
            get { return Solution.ProjectListe; }
        }

        string IBaseSolutionInfo.RawContent
        {
            get { return Solution.RawContent; }
        }

        VisualStudioVersion IBaseSolutionInfo.Version
        {
            get { return Solution.Version; }
        }

        #endregion

        private class CheckoutStructure : ILocalSolutionStructure
        {
            public ISolutionInfo Solution { get; private set; }
            public ISourceTreeDirectory ParentSourceTree { get; private set; }
            public IList<ISourceTreeDirectory> CheckoutTreeListe { get; private set; }

            internal CheckoutStructure(ISourceTreeDirectory parentSourceTree, ISolutionInfo solution)
            {
                Solution = solution;
                ParentSourceTree = parentSourceTree;
                CheckoutTreeListe = BuildCheckoutTreeListe(ParentSourceTree, Solution);
            }

            private IList<ISourceTreeDirectory> BuildCheckoutTreeListe(ISourceTreeDirectory parentSourceTree, ISolutionInfo solution)
            {
                IList<ISourceTreeDirectory> childDirectoryListe = parentSourceTree.ChildDirectoryListe;
                ISourceTreeDirectory directory = parentSourceTree.FindDirectoryBySolution(solution);
                throw new NotImplementedException();
            }
        }
    }
}
