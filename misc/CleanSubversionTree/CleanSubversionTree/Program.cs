// <copyright file="Program.cs" company="test">
//   Tobias Hertkorn
// </copyright>
namespace CleanSubversionTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using CleanSubversionTree.Properties;
    using System.Text.RegularExpressions;

    public class Program
    {
        private static Regex m_excludeDirectories;

        static void Main(string[] args)
        {
            try
            {
                List<string> excludes = new List<string>();
                excludes.AddRange(args);
                excludes.AddRange(Settings.Default.DEFAULT_EXCLUDES.OfType<string>());

                BuildExcludeRegex(excludes);

                DirectoryInfo di = new DirectoryInfo(Settings.Default.STARTING_DIRECTORY);
                if (!di.Exists) { throw new ArgumentException("Directory '" + di.FullName + "' does not exist"); }

                DeleteFilesAndFolders(di);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }
        }

        private static void BuildExcludeRegex(List<string> excludes)
        {
            if (excludes.Count > 0)
            {
                StringBuilder regexExpression = new StringBuilder();
                regexExpression.Append("^(");

                foreach (string exclude in excludes)
                {
                    if (!Path.IsPathRooted(exclude))
                    {
                        // relative Path
                        regexExpression.Append(".*");
                    }

                    regexExpression.Append(Regex.Escape(exclude));
                    regexExpression.Append("|");
                }

                regexExpression.RemoveLastCharacter();
                regexExpression.Append(").*$");
                m_excludeDirectories = new Regex(regexExpression.ToString(), RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
            }
            else
            {
                m_excludeDirectories = new Regex("(^a^b)", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase); // an always invalid Regex
            }
        }

        private static void DeleteFilesAndFolders(DirectoryInfo di)
        {
            DeleteFilesAndFolders(di, false);
        }

        private static void DeleteFilesAndFolders(DirectoryInfo di, bool deleteDi)
        {
            if (!m_excludeDirectories.IsMatch(di.FullName))
            {
                DeletingFiles(di);

                foreach (var subDir in di.GetDirectories())
                {
                    try
                    {
                        DeleteFilesAndFolders(subDir, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("   WARNING: " + subDir.FullName + " " + ex.Message);
                    }
                }

                if (deleteDi)
                {
                    try
                    {
                        di.Delete();
                    }
                    catch (Exception)
                    {
                        // ignore 
                    }
                }
            }
        }

        private static void DeletingFiles(DirectoryInfo di)
        {
            var childFiles = di.GetFiles();

            foreach (var file in childFiles)
            {
                try
                {
                    file.IsReadOnly = false;
                    file.Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("   WARNING: " + file.FullName + " " + ex.Message);
                }

            }
        }
    }
}
