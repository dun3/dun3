
namespace Com.Hertkorn.Framework.SourceCodeManagement.GlobalTree
{
    public interface ISourceTreeRootDirectory : ISourceTreeDirectory
    {
        ISourceTreeDirectory Root { get; }
    }
}
