using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using System.IO;

namespace Com.Hertkorn.Framework.SourceCodeManagement.Test.MockDaten
{
    public static class MockDatenGenerator
    {
        public static MockSolution FacesSolution
        {
            get
            {
                // TODO: Eigentlich nicht sauber!
                FileInfo solutionFile = new FileInfo(Properties.Settings.Default.BaseTestDataPath + "Faces.Www.sln");

                StreamReader sr = solutionFile.OpenText();
                string rawContent = sr.ReadToEnd();

                return new MockSolution(
                    solutionFile,
                    new List<ISolutionProjectInfo> {                       
                        new MockSolution.Project("Faces.Www", "{CB3BE0A3-3652-41DC-A29F-2769669EF47C}", "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", @"Faces.Www\Faces.Www.csproj"),
                        new MockSolution.Project("_Types", "{D92BC820-F69D-4AE0-9CE0-8563F226BBBF}", "{2150E333-8FDC-42A3-9474-1A3956D46DE8}", "_Types"),
                        new MockSolution.Project("_Framework", "{9BA3B999-323A-46B5-B43B-662407BA2DF4}", "{2150E333-8FDC-42A3-9474-1A3956D46DE8}", "_Framework"),
                        new MockSolution.Project("De.Hbv.Infrastructure.Faces.ServiceTypes", "{BB68C16C-6E65-4953-82DD-7D117F724C84}", "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", @"..\De.Hbv.Infrastructure\Faces\ServiceTypes\De.Hbv.Infrastructure.Faces.ServiceTypes.csproj"),
                        new MockSolution.Project("Services", "{171B7942-D0DA-44EB-9220-D09082707BCF}", "{2150E333-8FDC-42A3-9474-1A3956D46DE8}", "Services"),
                        new MockSolution.Project("De.Hbv.Infrastructure.Faces.Services.Gebaeude", "{F4C0CEE0-EF06-4871-A162-63E56FA31A13}", "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", @"..\De.Hbv.Infrastructure\Faces\Services\Gebaeude\De.Hbv.Infrastructure.Faces.Services.Gebaeude.csproj"),
                        new MockSolution.Project("De.Hbv.Infrastructure.Faces.Services.Mitarbeiter", "{31528CC8-3CF8-4C13-83DE-FB5572D274A7}", "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", @"..\De.Hbv.Infrastructure\Faces\Services\Mitarbeiter\De.Hbv.Infrastructure.Faces.Services.Mitarbeiter.csproj"),
                        new MockSolution.Project("De.Hbv.Infrastructure.Faces.Services.Mock", "{8666243E-3D76-4691-A50D-346CBC5058B3}", "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", @"..\De.Hbv.Infrastructure\Faces\Services\Mock\De.Hbv.Infrastructure.Faces.Services.Mock.csproj"),
                        new MockSolution.Project("De.Hbv.Framework.Wcf", "{CF02ADEE-C33E-47FA-B877-08F593763F60}", "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", @"..\De.Hbv.Framework\Wcf\De.Hbv.Framework.Wcf\De.Hbv.Framework.Wcf.csproj"),
                        new MockSolution.Project("_Mock", "{7BD3A39F-65CE-46BB-B664-AB95B38E7DEE}", "{2150E333-8FDC-42A3-9474-1A3956D46DE8}", "_Mock"),
                        new MockSolution.Project("De.Hbv.Framework.Helper", "{70EE87C0-4295-4681-97FB-1E9549549B8B}", "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}", @"..\De.Hbv.Framework\Helper\Helper\De.Hbv.Framework.Helper.csproj")
                    },
                    rawContent,
                    VisualStudioVersion.VS2008);
            }
        }

        public static MockProject SAPLogonPadUpdater
        {
            get
            {
                // TODO: Eigentlich nicht sauber!
                FileInfo projectFile = new FileInfo(Properties.Settings.Default.BaseTestDataPath + "SAPLogonPadUpdater.csproj");

                StreamReader sr = projectFile.OpenText();
                string rawContent = sr.ReadToEnd();

                return new MockProject(projectFile,
                    rawContent,
                    "9.0.21022",
                    "SAPLogonPadUpdater",
                    new List<IProjectReferenceInfo> { 
                        new MockProject.ProjectReference(new Guid("{70EE87C0-4295-4681-97FB-1E9549549B8B}"), "De.Hbv.Framework.Helper", @"..\..\De.Hbv.Framework\Helper\Helper\De.Hbv.Framework.Helper.csproj"),
                        new MockProject.ProjectReference(new Guid("{72ECFBD5-5807-4792-8FB0-0C46C998C7D9}"),"De.Hbv.Framework.InfoCollector.Windows.Forms",@"..\..\De.Hbv.Framework\InfoCollector\InfoCollector.Windows.Forms\De.Hbv.Framework.InfoCollector.Windows.Forms.csproj"),
                        new MockProject.ProjectReference(new Guid("{DE3DBBEC-D242-4175-B520-6A72A5E6D026}"),"De.Hbv.Framework.InfoCollector",@"..\..\De.Hbv.Framework\InfoCollector\InfoCollector\De.Hbv.Framework.InfoCollector.csproj")
                    },
                    new List<IAssemblyReference> { 
                        new MockProject.AssemblyReference(@"..\..\..\assembliesExtern\EntLib\3.1.0.0\Microsoft.Practices.EnterpriseLibrary.Common.dll",
                            "Microsoft.Practices.EnterpriseLibrary.Common, Version=3.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                            false),
                        new MockProject.AssemblyReference(@"..\..\..\assembliesExtern\EntLib\3.1.0.0\Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.dll",
                            "Microsoft.Practices.EnterpriseLibrary.ExceptionHandling, Version=3.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                            false),
                        new MockProject.AssemblyReference(@"..\..\..\assembliesExtern\EntLib\3.1.0.0\Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging.dll",
                            "Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging, Version=3.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                            false),
                        new MockProject.AssemblyReference(@"..\..\..\assembliesExtern\EntLib\3.1.0.0\Microsoft.Practices.EnterpriseLibrary.Logging.dll",
                            "Microsoft.Practices.EnterpriseLibrary.Logging, Version=3.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                            false),
                        new MockProject.AssemblyReference(@"..\..\..\assembliesExtern\EntLib\3.1.0.0\Microsoft.Practices.ObjectBuilder.dll",
                            "Microsoft.Practices.ObjectBuilder, Version=1.0.51206.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL",
                            false)
                    },
                    new Guid("{BCECDF5A-BC51-4978-B78C-EC67906AB8A4}"),
                    "SAPLogonPadUpdater",
                    projectFile.FullName
                    );
            }
        }
    }
}
