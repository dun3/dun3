using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Com.Hertkorn.TempCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) { Console.WriteLine("Please specify the directory to clean"); Environment.ExitCode = 1; return; }

            DateTime cutlineUTC = DateTime.UtcNow.AddHours(-1.0);

            Console.WriteLine("Cutting everything before " + cutlineUTC.ToLocalTime().ToShortDateString() + " " + cutlineUTC.ToLocalTime().ToShortTimeString());

            foreach (var directory in args)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(directory);
                    if (!di.Exists) { throw new ArgumentException("directory '" + directory + "' does not exist"); }

                    Console.WriteLine(); Console.WriteLine("STARTING: " + di.FullName);

                    DeleteFilesAndFoldersOlderThanCutline(cutlineUTC, di);

                    Console.WriteLine();
                    Console.WriteLine("DONE: " + di.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: Could not process '" + directory + "'");
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    Console.WriteLine("==================================");
                }
            }
        }

        private static void DeleteFilesAndFoldersOlderThanCutline(DateTime cutlineUTC, DirectoryInfo di)
        {
            DeleteFilesAndFoldersOlderThanCutline(cutlineUTC, di, false);
        }

        private static void DeleteFilesAndFoldersOlderThanCutline(DateTime cutlineUTC, DirectoryInfo di, bool deleteDi)
        {
            DeletingFilesOlderThanCutline(cutlineUTC, di);

            foreach (var subDir in di.GetDirectories())
            {
                try
                {
                    if (subDir.LastWriteTimeUtc <= cutlineUTC)
                    {
                        DeleteFilesAndFoldersOlderThanCutline(cutlineUTC, subDir, true);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("   WARNING: " + subDir.FullName + " " + ex.Message);
                }
            }

            if (deleteDi)
            {
                try
                {
                    di.Delete();
                    Console.Write("+");
                }
                catch (Exception)
                {
                    // ignore 
                }
                finally
                {
                    Console.Write(";");
                }
            }
        }

        private static void DeletingFilesOlderThanCutline(DateTime cutlineUTC, DirectoryInfo di)
        {
            var childFiles = di.GetFiles();

            try
            {
                foreach (var file in childFiles)
                {
                    try
                    {
                        if (file.CreationTimeUtc <= cutlineUTC)
                        {
                            file.IsReadOnly = false;
                            file.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine("   WARNING: " + file.FullName + " " + ex.Message);
                    }
                    finally
                    {
                        Console.Write(".");
                    }
                }
            }
            finally
            {
                Console.Write("-");
            }
        }
    }
}
