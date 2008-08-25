using System;
using System.Diagnostics;
using System.Threading;
using System.Data.SqlClient;

namespace PerformanceTest.GuidCreation
{
    class Program
    {
        private const int CYCLES_PER_TEST = 10000000;
        static void Main(string[] args)
        {
            Guid[] testGuidArray = new Guid[CYCLES_PER_TEST];
            for (int i = 0; i < CYCLES_PER_TEST; i++)
            {
                testGuidArray[i] = Guid.NewGuid();
            }

            InMemory(testGuidArray);
            
            Console.Read();
        }

        private static void InMemory(Guid[] testGuidArray)
        {
            string testResult = "";
            Thread.Sleep(50);
            Stopwatch stopwatchEmptyLoop = new Stopwatch();
            stopwatchEmptyLoop.Start();
            for (int i = 0; i < CYCLES_PER_TEST; i++)
            {
                Thread.MemoryBarrier();
                Guid guid = testGuidArray[i];
                Thread.MemoryBarrier();
                testResult = guid.ToString();
                Thread.MemoryBarrier();
                testGuidArray[i] = guid;
            }
            stopwatchEmptyLoop.Stop();

            testResult = "";
            Thread.Sleep(50);
            Stopwatch stopwatchHotTestCycle = new Stopwatch();
            stopwatchHotTestCycle.Start();
            for (int i = 0; i < CYCLES_PER_TEST; i++)
            {
                Thread.MemoryBarrier();
                testGuidArray[i] = Guid.NewGuid();
                Thread.MemoryBarrier();
                Guid guid = testGuidArray[i];
                Thread.MemoryBarrier();
                testResult = guid.ToString();
            }
            stopwatchHotTestCycle.Stop();

            Auswertung(stopwatchEmptyLoop, stopwatchHotTestCycle);
            Diagnostics(testGuidArray, testResult);
        }

        

        private static void Auswertung(Stopwatch stopwatchEmptyLoop, Stopwatch stopwatchHotTestCycle)
        {
            Console.WriteLine("Using {0} cycles", CYCLES_PER_TEST);
            Console.WriteLine("Empty Loop: {0}", stopwatchEmptyLoop.ElapsedMilliseconds / 1000.0);
            Console.WriteLine("Hot Loop:   {0}", stopwatchHotTestCycle.ElapsedMilliseconds / 1000.0);
            long timeDifference = stopwatchHotTestCycle.ElapsedMilliseconds - stopwatchEmptyLoop.ElapsedMilliseconds;
            Console.WriteLine("Milliseconds per Guid.NewGuid(): {0}", ((double)timeDifference) / CYCLES_PER_TEST);
        }

        private static void Diagnostics(Guid[] testGuidArray, string testResult)
        {
            Console.WriteLine("-----Diagnostics");
            Console.WriteLine(testResult);
            Console.WriteLine(testGuidArray[0].ToString());
            Console.WriteLine(testGuidArray[CYCLES_PER_TEST - 2].ToString());
        }
    }
}
