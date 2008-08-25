using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;

namespace Com.Hertkorn.Framework.SourceCodeManagement.Vcs
{
    public interface IVcsSolutionInfo : ISolutionInfo
    {
        ILocalSolutionStructure MinimalCheckoutStructure { get; }
    }
}
