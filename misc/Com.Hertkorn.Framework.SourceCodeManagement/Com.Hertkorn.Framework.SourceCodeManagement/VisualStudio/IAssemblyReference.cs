
namespace Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio
{
    public interface IAssemblyReference : IReference
    {        
        string RawHintPath { get; }
        string RawInclude { get; }
        bool SpecificVersion { get; }
    }
}
