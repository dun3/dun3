using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using System.IO;

namespace Com.Hertkorn.Framework.SourceCodeManagement.SourceTree
{
    public interface ISourceTreeDirectory
    {
        DirectoryInfo SystemDirectory { get; }
        IList<ISourceTreeDirectory> ChildDirectoryListe { get; }
        IList<ISolutionInfo> FindSolutionAll();

        ISourceTreeDirectory FindDirectoryBySolution(ISolutionInfo solution);
    }
}
