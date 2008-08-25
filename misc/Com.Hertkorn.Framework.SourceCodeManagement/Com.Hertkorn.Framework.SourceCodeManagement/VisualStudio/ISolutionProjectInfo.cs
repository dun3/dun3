using System;

namespace Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio
{
    public interface ISolutionProjectInfo : IBaseProjectInfo
    {
        //Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "De.Hbv.Infrastructure.Faces.ServiceTypes", "..\De.Hbv.Infrastructure\Faces\ServiceTypes\De.Hbv.Infrastructure.Faces.ServiceTypes.csproj", "{BB68C16C-6E65-4953-82DD-7D117F724C84}"
        Guid ProjectTypeGuid { get; }
    }
}
