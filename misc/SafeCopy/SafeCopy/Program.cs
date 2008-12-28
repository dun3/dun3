using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace SafeCopy
{
    class Program
    {
        private const int MAX_QUEUE_LENGTH = 10000;

        static Program()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private static Queue<QueueItem> m_queue = new Queue<QueueItem>();
        private static bool m_running = true;

        private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(Program).Assembly, typeof(Program));

        public static long FileCount = 0;
        public static long DirCount = 0;
        public static long ErrorCount = 0;

        public static FileInfo m_logFile = new FileInfo(@"c:\safecopylog.txt");
        public static Dictionary<string, object> m_logFileAlreadyCopied = new Dictionary<string, object>();

        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0) { Console.WriteLine("no args found"); return; }

                DirectoryInfo sourceDir = new DirectoryInfo(args[0]);

                if (!sourceDir.Exists) { Console.WriteLine("di does not exist"); return; }

                LoadAlreadyCopied();

                string relativePath = ".";

                DirectoryInfo targetDir = new DirectoryInfo(relativePath);

                Thread thread = new Thread((o) =>
                {
                    try
                    {
                        while (m_running || (m_queue.Count > 0))
                        {
                            long lengthSum = 0;

                            List<QueueItem> items = new List<QueueItem>(2000);
                            lock (m_queue)
                            {
                                // get max 50MB worth of next files
                                while ((m_queue.Count > 0) && (lengthSum < 50 * 1024 * 1024))
                                {
                                    QueueItem next = m_queue.Dequeue();
                                    items.Add(next);
                                    lengthSum += next.Length;
                                }
                            }

                            foreach (var item in items)
                            {
                                try
                                {
                                    Console.Write(".");
                                    //if (!File.Exists(item.TargetPath))
                                    //{

                                    File.Copy(item.SourcePath, item.TargetPath);

                                    //}
                                }
                                catch (Exception ex)
                                {
                                    Console.Write("!");
                                    ErrorCount++;
                                    LOG.ErrorFormat("Error copying file '{0}' -> '{1}': {2}", item.SourcePath, item.TargetPath, ex.ToString());
                                }
                            }

                            using (var stream = new StreamWriter(m_logFile.FullName, true, Encoding.Unicode))
                            {
                                foreach (var item in items)
                                {
                                    stream.WriteLine(item.SourcePath);
                                }

                                stream.Flush();
                                stream.Close();
                            }

                            if (m_queue.Count == 0)
                            {
                                Thread.Sleep(100);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LOG.FatalFormat("Got fatal exception {0}", ex.ToString());
                    }
                });

                thread.Start();

                Recurse(sourceDir, targetDir, relativePath);

                PushFoundFilesToQueue();

                m_running = false;

                thread.Join();
            }
            finally
            {
                m_running = false;
            }

            Console.WriteLine("done");
            Console.WriteLine(string.Format("DirCount: {0} FileCount: {1} ErrorCount: {2}", DirCount, FileCount, ErrorCount));
        }

        private static void LoadAlreadyCopied()
        {
            if (m_logFile.Exists)
            {
                using (var stream = new StreamReader(m_logFile.OpenRead(), Encoding.Unicode))
                {
                    try
                    {
                        while (stream.Peek() >= 0)
                        {
                            var line = stream.ReadLine();
                            m_logFileAlreadyCopied[line] = new object();
                        }
                    }
                    catch (Exception ex)
                    {
                        LOG.Error(ex.ToString());
                    }
                }
            }
            else
            {
                m_logFile.Create();
            }
        }

        private static LinkedList<QueueItem> m_foundFiles = new LinkedList<QueueItem>();

        private static void Recurse(DirectoryInfo sourceDir, DirectoryInfo targetDir, string relativePath)
        {
            LOG.DebugFormat("Entering Recurse with '{0}' '{1}' '{2}'", sourceDir.FullName, targetDir.FullName, relativePath);

            foreach (FileInfo file in sourceDir.GetFiles())
            {
                if (!m_logFileAlreadyCopied.ContainsKey(file.FullName))
                {
                    string targetPath = Path.Combine(targetDir.FullName, file.Name);
                    try
                    {
                        LOG.InfoFormat("Copying '{0}' -> '{1}'", file.FullName, targetPath);
                        //Console.Write(".");
                        FileCount++;

                        m_foundFiles.AddLast(new QueueItem(file.FullName, targetPath, file.Length));
                    }
                    catch (Exception ex)
                    {
                        ErrorCount++;
                        LOG.ErrorFormat("Error evaluating file '{0}' -> '{1}': {2}", file.FullName, targetPath, ex.ToString());
                        Console.Write("!");
                    }
                }
                else
                {
                    Console.Write("s");
                }
            }

            int limit = MAX_QUEUE_LENGTH;
            while (m_queue.Count > limit)
            {
                limit = MAX_QUEUE_LENGTH / 2;
                Thread.Sleep(100);
            }

            if (m_foundFiles.Count > 200)
            {
                PushFoundFilesToQueue();
            }

            foreach (DirectoryInfo nextSourceDir in sourceDir.GetDirectories())
            {
                try
                {
                    Console.Write("+");
                    string nextSourceDirPath = Path.Combine(targetDir.FullName, relativePath + "\\" + nextSourceDir.Name);
                    DirectoryInfo nextTargetDir = new DirectoryInfo(nextSourceDirPath);
                    if (!nextTargetDir.Exists)
                    {
                        LOG.DebugFormat("Creating '{0}'", nextTargetDir.FullName);
                        nextTargetDir.Create();
                    }
                    DirCount++;
                    Recurse(nextSourceDir, nextTargetDir, nextSourceDirPath);
                }
                catch (Exception ex)
                {
                    ErrorCount++;
                    LOG.ErrorFormat("Error in source directory '{0}': {1}", nextSourceDir.FullName, ex.ToString());
                    Console.Write("^");
                }
            }

            Console.Write("-");
            LOG.DebugFormat("Exiting Recurse with '{0}' '{1}' '{2}'", sourceDir.FullName, targetDir.FullName, relativePath);
        }

        private static void PushFoundFilesToQueue()
        {
            lock (m_queue)
            {
                foreach (var item in m_foundFiles)
                {
                    m_queue.Enqueue(item);
                }
                m_foundFiles.Clear();
            }
        }

        public class QueueItem
        {
            public QueueItem(string sourcePath, string targetPath, long length)
            {
                SourcePath = sourcePath;
                TargetPath = targetPath;
                Length = length;
            }

            public string SourcePath { get; set; }
            public string TargetPath { get; set; }
            public long Length { get; set; }
        }
    }
}
