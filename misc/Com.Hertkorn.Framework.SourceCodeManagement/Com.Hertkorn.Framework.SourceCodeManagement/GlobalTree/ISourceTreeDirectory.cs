using System.Collections.Generic;
using System.IO;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;

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
