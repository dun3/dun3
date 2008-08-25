using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;

namespace Com.Hertkorn.Framework.SourceCodeManagement.Vcs
{
    public interface IVcsSolutionInfo : ISolutionInfo
    {
        ILocalSolutionStructure MinimalCheckoutStructure { get; }
    }
}
