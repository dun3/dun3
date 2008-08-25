using System.IO;

namespace Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio
{
    public interface ISolutionInfo : IBaseSolutionInfo
    {
        FileInfo SolutionFile { get; }
    }
}
