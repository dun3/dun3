using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.SourceCodeManagement.SourceTree
{
    public interface ISourceTreeRootDirectory : ISourceTreeDirectory
    {
        ISourceTreeDirectory Root { get; }
    }
}
