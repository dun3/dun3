
namespace Com.Hertkorn.Framework.SourceCodeManagement.SourceTree
{
    public interface ISourceTreeRootDirectory : ISourceTreeDirectory
    {
        ISourceTreeDirectory Root { get; }
    }
}
