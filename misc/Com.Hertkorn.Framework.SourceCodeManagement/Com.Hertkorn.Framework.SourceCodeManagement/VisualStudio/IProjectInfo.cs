using System.Collections.Generic;
using System.IO;

namespace Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio
{
    public interface IProjectInfo : IBaseProjectInfo
    {
        FileInfo ProjectFile { get; }
        string RawContent { get; }
        string ProductVersion { get; }
        string RootNameSpace { get; }
        IList<IProjectReferenceInfo> ProjectReferenceListe { get; }
        IList<IAssemblyReference> AssemblyReferenceListe { get; }
    }
}