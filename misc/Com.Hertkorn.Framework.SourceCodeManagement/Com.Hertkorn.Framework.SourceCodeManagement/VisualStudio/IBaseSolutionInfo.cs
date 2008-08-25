using System.Collections.Generic;

namespace Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio
{
    public interface IBaseSolutionInfo
    {
        string RawSolutionPath { get; }
        IList<ISolutionProjectInfo> ProjectListe { get; }
        string RawContent { get; }
        VisualStudioVersion Version { get; }
    }
}
