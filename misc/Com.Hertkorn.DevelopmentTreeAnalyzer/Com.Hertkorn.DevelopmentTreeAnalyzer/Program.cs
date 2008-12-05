using System;
using System.Collections.Generic;
using System.IO;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using Com.Hertkorn.Framework.SourceCodeManagement.Product;

namespace Com.Hertkorn.DevelopmentTree
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo[] fis = (new DirectoryInfo("c:\\dev")).GetFiles("*.sln", SearchOption.AllDirectories);

            FileInfo logfile = new FileInfo("c:\\output2.txt");
            if (logfile.Exists)
            {
                logfile.Delete();
            }

            using (StreamWriter sw = new StreamWriter(logfile.FullName, false))
            {
                foreach (var item in fis)
                {
                    try
                    {
                        List<ISolutionInfo> solutions = new List<ISolutionInfo>();
                        solutions.Add(new Solution(item));
                        ProductSourceStructure structure = new ProductSourceStructure(solutions);

                        IList<string> liste = structure.UniqueDirectoryListe;
                        List<string> sorted = new List<string>(structure.RootDirectoryListe);
                        sorted.Sort();

                        string error = string.Empty;
                        foreach (var item2 in sorted)
                        {
                            if (!item2.StartsWith("c:\\dev\\"))
                            {
                                error += item2 + Environment.NewLine;
                            }
                        }
                        if (!string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine("there were items referenced that where outside of c:\\dev in " + item.FullName);
                            Console.WriteLine(error);
                        }
                        else
                        {
                            string solutionName = solutions[0].SolutionFile.FullName;
                            Console.WriteLine(solutionName);

                            sw.WriteLine(solutionName + ";;;");

                            foreach (var project in solutions[0].ProjectListe)
                            {
                                Project p = new Project(new FileInfo(Path.Combine(solutions[0].SolutionFile.DirectoryName, project.RawProjectPath)));
                                sw.WriteLine(solutionName + ";" + p.ProjectFile.FullName + ";;");

                                foreach (var assembly in p.AssemblyReferenceListe)
                                {
                                    FileInfo fi = new FileInfo(Path.Combine(p.ProjectFile.DirectoryName, assembly.RawHintPath));

                                    sw.WriteLine(solutionName + ";" + p.ProjectFile.FullName + ";" + fi.FullName + ";\"" + assembly.RawInclude + "\"");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error parsing " + item.FullName);
                        Console.WriteLine(ex.GetType().ToString() + ": " + ex.Message);
                        //Console.WriteLine(ex.StackTrace);
                    }
                }
                sw.Flush();
                sw.Close();
            }
            Console.WriteLine("Done");
            Console.Read();

        }


        private class SubversionRepresentation
        {
            public string subversionurl { get; set; }
            public string targetdirectory { get; set; }
        }
        //private static string WriteSolutionConfig(List<ISolutionInfo> solutions, List<string> sorted, string name)
        //{
        //    //List<SubversionRepresentation> svnListe = ConvertToSubversionRepresentation(sorted);
        //    Template projectTemplate = Engine.GetTemplate(@"config-projecttemplate.vm");
        //    VelocityContext projectContext = new VelocityContext();

        //    List<object> solutionListe = new List<object>();
        //    foreach (var item in solutions)
        //    {
        //        string exeVersion = @"C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\devenv.exe";
        //        if (item.Version == VisualStudioVersion.VS2005)
        //        {
        //            exeVersion = @"C:\Program Files\Microsoft Visual Studio 8\Common7\IDE\devenv.exe";
        //        }


        //        var a = new
        //        {
        //            exe = exeVersion,
        //            file = TargetDirectory + item.SolutionFile.FullName.Remove(0, 6)
        //        };
        //        solutionListe.Add(a);
        //    }

        //    string postfix = TargetDirectory.Remove(0, 5);
        //    string projectname = name;
        //    if (projectname.Length > 100)
        //    {
        //        projectname = projectname.Substring(0, 100);
        //    }
        //    projectname = projectname + "-" + postfix;

        //    projectContext.Put("projectname", projectname);
        //    projectContext.Put("projectpriority", 800000);
        //    projectContext.Put("ccserverip", "172.28.52.140");
        //    projectContext.Put("workingdirectory", TargetDirectory);
        //    projectContext.Put("checkoutRootListe", svnListe);
        //    projectContext.Put("solutionListe", solutionListe);

        //    string configname = "config-" + projectname;
        //    string configfilename = configname + ".xml";

        //    using (StreamWriter writer = (new FileInfo("c:\\test\\" + configfilename)).CreateText())
        //    {
        //        projectTemplate.Merge(projectContext, writer);
        //    }
        //    return configname;
        //}
    }
}
