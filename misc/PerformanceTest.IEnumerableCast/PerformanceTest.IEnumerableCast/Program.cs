using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

namespace PerformanceTest.IEnumerableCast
{
    class Program
    {
        private const int OBJECTS_TO_TRANSFORM = 50000000;
        private const int DEFAULT_STEPSIZE = 10;

        private static readonly Random RANDOM = new Random();
        private static int RANDOM_NR = 0;

        public static ITestInterface TEST_OBJECT = new TestClass("0", 0);

        static void Main(string[] args)
        {
            int stepsize = DEFAULT_STEPSIZE;
            if (args.Length == 1) int.TryParse(args[0], out stepsize);

            Console.WriteLine("Stepsize: " + stepsize.ToString());

            FileInfo output = new FileInfo(String.Format("output_{0}_{1}.csv", OBJECTS_TO_TRANSFORM, stepsize));
            if (output.Exists)
            {
                File.Move(output.FullName, String.Format("{0},backup{1}", output.FullName, DateTime.Now.Ticks));
            }

            Console.WriteLine(output.FullName);

            var stream = new StreamWriter(output.OpenWrite());

            stream.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", "listLength", "cycles", "emptyLoop", "ownExtensionMethod", "plainSimpleInForeachLoop", "inForeachLoopInCustomMethod", "inForeachLoopInCustomMethodNoInlining", "inForeachLoopInListExtender", "frameworkCastToList"));

            var testRuns = GenerateRuns(stepsize);

            Console.WriteLine(String.Format("Generated {0} testruns", testRuns.Count));

            Thread.Sleep(500);
            Stopwatch process = new Stopwatch();
            process.Start();

            for (int i = 0; i < testRuns.Count; i++)
            {
                int listLength = testRuns[i].Key;
                int cycles = testRuns[i].Value;

                Stopwatch emptyLoop;
                Stopwatch ownExtensionMethod;
                Stopwatch plainSimpleInForeachLoop;
                Stopwatch inForeachLoopInCustomMethod;
                Stopwatch inForeachLoopInCustomMethodNoInlining;
                Stopwatch inForeachLoopInListExtender;
                Stopwatch frameworkCastToList;
                TestRun(listLength, cycles, out emptyLoop, out ownExtensionMethod, out plainSimpleInForeachLoop, out inForeachLoopInCustomMethod, out inForeachLoopInCustomMethodNoInlining, out inForeachLoopInListExtender, out frameworkCastToList);

                stream.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}", listLength, cycles, emptyLoop.ElapsedMilliseconds, ownExtensionMethod.ElapsedMilliseconds, plainSimpleInForeachLoop.ElapsedMilliseconds, inForeachLoopInCustomMethod.ElapsedMilliseconds, inForeachLoopInCustomMethodNoInlining.ElapsedMilliseconds, inForeachLoopInListExtender.ElapsedMilliseconds, frameworkCastToList.ElapsedMilliseconds));
                stream.Flush();
                Console.WriteLine(listLength.ToString() + " " + cycles.ToString());

                double percent = (i + 1) / (double)testRuns.Count;

                Console.WriteLine(string.Format("Done {0:0.0}% - {1:0.0}s elapsed, {2:0.0}s left", percent * 100, process.ElapsedMilliseconds / 1000, (process.ElapsedMilliseconds * (1 - percent)) / (percent * 1000)));

                Thread.Sleep(100);
            }

            Console.WriteLine(TEST_OBJECT.SomeString);

            stream.Flush();
            stream.Close();

            //Console.WriteLine("emptyLoop: " + emptyLoop.ElapsedMilliseconds);
            //Console.WriteLine("plainSimpleInForeachLoop: " + plainSimpleInForeachLoop.ElapsedMilliseconds);
            //Console.WriteLine("inForeachLoopInCustomMethod: " + inForeachLoopInCustomMethod.ElapsedMilliseconds);
            //Console.WriteLine("inForeachLoopInCustomMethodNoInlining: " + inForeachLoopInCustomMethodNoInlining.ElapsedMilliseconds);
            //Console.WriteLine("inForeachLoopInListExtender: " + inForeachLoopInListExtender.ElapsedMilliseconds);
            //Console.WriteLine("ownExtensionMethod: " + ownExtensionMethod.ElapsedMilliseconds);
            //Console.WriteLine("frameworkCastToList: " + frameworkCastToList.ElapsedMilliseconds);
            //Console.Read();
        }

        private static List<KeyValuePair<int, int>> GenerateRuns(int stepsize)
        {
            //var testRuns = new List<KeyValuePair<int, int>>();
            //for (int listLength = stepsize; listLength <= OBJECTS_TO_TRANSFORM / stepsize; listLength *= stepsize)
            //{
            //    testRuns.Add(new KeyValuePair<int, int>(listLength, OBJECTS_TO_TRANSFORM / listLength));
            //}
            //return testRuns;
            return new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(OBJECTS_TO_TRANSFORM / 10, 10) };
        }

        private static void TestRun(int listLength, int cycles, out Stopwatch emptyLoop, out Stopwatch ownExtensionMethod, out Stopwatch plainSimpleInForeachLoop, out Stopwatch inForeachLoopInCustomMethod, out Stopwatch inForeachLoopInCustomMethodNoInlining, out Stopwatch inForeachLoopInListExtender, out Stopwatch frameworkCastToList)
        {
            List<TestClass> testList = new List<TestClass>(2 * listLength);

            for (int i = 0; i < listLength; i++)
            {
                testList.Add(new TestClass(string.Empty, i));
            }

            emptyLoop = StopMe(() => ForOverTests(cycles, (i) => EmptyLoop(testList)));

            ownExtensionMethod = StopMe(() => ForOverTests(cycles, (i) => OwnExtensionMethod(testList)));

            plainSimpleInForeachLoop = StopMe(() => ForOverTests(cycles, (i) => PlainSimpleForeachLoop(testList)));

            inForeachLoopInCustomMethod = StopMe(() => ForOverTests(cycles, (i) => InForeachLoopInCustomMethod(testList)));

            inForeachLoopInCustomMethodNoInlining = StopMe(() => ForOverTests(cycles, (i) => InForeachLoopInCustomMethodNoInlining(testList)));

            inForeachLoopInListExtender = StopMe(() => ForOverTests(cycles, (i) => InForeachLoopInListExtender(testList)));

            frameworkCastToList = StopMe(() => ForOverTests(cycles, (i) => FrameworkCastToList(testList)));
        }

        private static void EmptyLoop(List<TestClass> testList)
        {
            RANDOM_NR = RANDOM.Next(0, testList.Count);
            TEST_OBJECT.SomeInt = RANDOM_NR;
        }

        private static void FrameworkCastToList(List<TestClass> testList)
        {
            RANDOM_NR = RANDOM.Next(0, testList.Count);
            TEST_OBJECT.SomeInt = RANDOM_NR;

            var a = testList.Cast<ITestInterface>();
            var b = a.Count();
            TEST_OBJECT = a.ToList()[RANDOM_NR];
        }

        private static void InForeachLoopInListExtender(List<TestClass> testList)
        {
            RANDOM_NR = RANDOM.Next(0, testList.Count);
            TEST_OBJECT.SomeInt = RANDOM_NR;

            TEST_OBJECT = ListExtender.CastListMethod<TestClass, ITestInterface>(testList)[RANDOM_NR];
        }

        private static void InForeachLoopInCustomMethodNoInlining(List<TestClass> testList)
        {
            RANDOM_NR = RANDOM.Next(0, testList.Count);
            TEST_OBJECT.SomeInt = RANDOM_NR;

            TEST_OBJECT = CastMethodNoInlining(testList)[RANDOM_NR];
        }

        private static void InForeachLoopInCustomMethod(List<TestClass> testList)
        {
            RANDOM_NR = RANDOM.Next(0, testList.Count);
            TEST_OBJECT.SomeInt = RANDOM_NR;

            TEST_OBJECT = CastMethod(testList)[RANDOM_NR];
        }

        private static void PlainSimpleForeachLoop(List<TestClass> testList)
        {
            RANDOM_NR = RANDOM.Next(0, testList.Count);
            TEST_OBJECT.SomeInt = RANDOM_NR;

            List<ITestInterface> cast = new List<ITestInterface>(testList.Count);
            foreach (var item in testList)
            {
                cast.Add(item);
            }
            TEST_OBJECT = cast[RANDOM_NR];
        }

        private static void OwnExtensionMethod(List<TestClass> testList)
        {
            RANDOM_NR = RANDOM.Next(0, testList.Count);
            TEST_OBJECT.SomeInt = RANDOM_NR;

            TEST_OBJECT = testList.CastListExtension<TestClass, ITestInterface>()[RANDOM_NR];
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static List<ITestInterface> CastMethodNoInlining(List<TestClass> testList)
        {
            List<ITestInterface> result = new List<ITestInterface>(testList.Count);
            foreach (var item in testList)
            {
                result.Add(item);
            }
            return result;
        }

        private static List<ITestInterface> CastMethod(List<TestClass> testList)
        {
            List<ITestInterface> result = new List<ITestInterface>(testList.Count);
            foreach (var item in testList)
            {
                result.Add(item);
            }
            return result;
        }

        public static List<K> CastList<T, K>(List<T> list) where T : K
        {
            List<K> cast = new List<K>(list.Count);
            foreach (T item in list)
            {
                cast.Add(item);
            }
            return cast;
        }

        private static void ForOverTests(int cycles, Action<int> action)
        {
            for (int i = 0; i < cycles; i++)
            {
                action(i);
            }
        }

        private static Stopwatch StopMe(System.Action action)
        {
            Thread.Sleep(50);
            Stopwatch stopwatchEmptyLoop = new Stopwatch();
            stopwatchEmptyLoop.Start();

            action();

            stopwatchEmptyLoop.Stop();
            Thread.Sleep(50);
            return stopwatchEmptyLoop;
        }
    }
}
