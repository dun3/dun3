using System;
using System.Collections.Generic;
using System.IO;
using Com.Hertkorn.Framework.SourceCodeManagement.Product;
using Com.Hertkorn.Framework.SourceCodeManagement.VisualStudio;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace Com.Hertkorn.DevelopmentTree
{
    class Program
    {

        private static VelocityEngine Engine;
        static Program()
        {
            VelocityEngine velocity = new VelocityEngine();

            velocity.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            velocity.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, @"C:\test\Com.Hertkorn.DevelopmentTree\Com.Hertkorn.DevelopmentTree\template");
            velocity.Init();
            Engine = velocity;
        }

        public static string TargetDirectory = "c:\\a\\00";
        public static int TargetDirectoryCount = 0;
        public static readonly string[] TARGET_CHARS = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        static void GenerateNextTargetDirectory()
        {
            TargetDirectoryCount++;
            TargetDirectory = "c:\\a\\";
            int first = TargetDirectoryCount / TARGET_CHARS.Length;
            int second = TargetDirectoryCount % TARGET_CHARS.Length;
            TargetDirectory += TARGET_CHARS[first];
            TargetDirectory += TARGET_CHARS[second];
        }

        static void Main(string[] args)
        {
            FileInfo[] fis = (new DirectoryInfo("c:\\dev")).GetFiles("*.sln", SearchOption.AllDirectories);

            List<string> configFileNames = new List<string>();
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
                        string name = solutions[0].SolutionFile.FullName.Replace('\\', '_').Replace(' ', '_').Remove(0, 3);
                        name = name.Remove(name.Length - 4, 4);

                        string configname = WriteSolutionConfig(solutions, sorted, name);
                        configFileNames.Add(configname);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error parsing " + item.FullName);
                    Console.WriteLine(ex.GetType().ToString() + ": " + ex.Message);
                    //Console.WriteLine(ex.StackTrace);
                }
            }
            WriteGlobalConfig(configFileNames);
            Console.WriteLine("Done");
            Console.Read();

        }

        private static void WriteGlobalConfig(List<string> configFileNames)
        {
            Template projectTemplate = Engine.GetTemplate(@"config.vm");
            VelocityContext projectContext = new VelocityContext();

            projectContext.Put("configFileNames", configFileNames);

            using (StreamWriter writer = (new FileInfo("c:\\test\\config-all.xml")).CreateText())
            {
                projectTemplate.Merge(projectContext, writer);
            }
        }

        private class SubversionRepresentation
        {
            public string subversionurl { get; set; }
            public string targetdirectory { get; set; }
        }
        private static string WriteSolutionConfig(List<ISolutionInfo> solutions, List<string> sorted, string name)
        {
            GenerateNextTargetDirectory();
            //List<SubversionRepresentation> svnListe = ConvertToSubversionRepresentation(sorted);
            List<object> svnListe = ConvertToSubversionRepresentation(sorted);
            Template projectTemplate = Engine.GetTemplate(@"config-projecttemplate.vm");
            VelocityContext projectContext = new VelocityContext();

            List<object> solutionListe = new List<object>();
            foreach (var item in solutions)
            {
                string exeVersion = @"C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\devenv.exe";
                if (item.Version == VisualStudioVersion.VS2005)
                {
                    exeVersion = @"C:\Program Files\Microsoft Visual Studio 8\Common7\IDE\devenv.exe";
                }


                var a = new
                {
                    exe = exeVersion,
                    file = TargetDirectory + item.SolutionFile.FullName.Remove(0, 6)
                };
                solutionListe.Add(a);
            }

            string postfix = TargetDirectory.Remove(0, 5);
            string projectname = name;
            if (projectname.Length > 100)
            {
                projectname = projectname.Substring(0, 100);
            }
            projectname = projectname + "-" + postfix;

            projectContext.Put("projectname", projectname);
            projectContext.Put("projectpriority", 800000);
            projectContext.Put("ccserverip", "172.28.52.140");
            projectContext.Put("workingdirectory", TargetDirectory);
            projectContext.Put("checkoutRootListe", svnListe);
            projectContext.Put("solutionListe", solutionListe);

            string configname = "config-" + projectname;
            string configfilename = configname + ".xml";

            using (StreamWriter writer = (new FileInfo("c:\\test\\" + configfilename)).CreateText())
            {
                projectTemplate.Merge(projectContext, writer);
            }
            return configname;
        }

        private static List<object> ConvertToSubversionRepresentation(List<string> sorted)
        {
            List<object> svnRep = new List<object>();
            foreach (var item in sorted)
            {
                var a = new
                {
                    subversionurl = "https://svn01.bauerverlag.de/svn/dev_net/" + item.Replace('\\', '/').Remove(0, 3),
                    targetdirectory = TargetDirectory + item.Remove(0, 6)
                };
                svnRep.Add(a);
            }

            return svnRep;
        }
        //private static List<SubversionRepresentation> ConvertToSubversionRepresentation(List<string> sorted)
        //{
        //    List<SubversionRepresentation> svnRep = new List<SubversionRepresentation>();
        //    foreach (var item in sorted)
        //    {
        //        SubversionRepresentation sr = new SubversionRepresentation();
        //        sr.subversionurl = "svn://172.23.1.95/" + item.Replace('\\', '/').Remove(0, 3);
        //        sr.targetdirectory = TargetDirectory + item.Remove(0, 6);
        //        svnRep.Add(sr);
        //    }

        //    return svnRep;
        //}
    }
}
