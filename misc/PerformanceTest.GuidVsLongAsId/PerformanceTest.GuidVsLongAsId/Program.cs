using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.Timing;

namespace PerformanceTest.GuidVsLongAsId
{
    class Program
    {
        static void Main(string[] args)
        {
            GC.Collect();
            var longSw = Stopwatch.Time("long", RUNS * SET_SIZE, TestLong);
            GC.Collect();
            var guidSw = Stopwatch.Time("Guid", RUNS * SET_SIZE, TestGuid);
            Console.WriteLine(longSw.ToString());
            Console.WriteLine("=======");
            Console.WriteLine(guidSw.ToString());
        }

        public static Guid DEFAULT_GUID = Guid.Empty;
        public static long DEFAULT_LONG = 0;

        public static void TestLong()
        {
            Test<long>();
        }

        public static void TestGuid()
        {
            Test<Guid>();
        }

        public static readonly int SET_SIZE = 100000;
        public static readonly int RUNS = 100;

        public static List<bool> SET;

        static Program()
        {
            SET = new List<bool>(SET_SIZE);
            for (int i = 0; i < SET_SIZE; i++)
            {
                SET.Add(false);
            }
        }

        public static void Test<T>() where T : struct
        {
            List<TestClass<T>> thisSet = new List<TestClass<T>>(SET_SIZE);

            for (int j = 0; j < RUNS; j++)
            {
                for (int i = 0; i < SET_SIZE; i++)
                {
                    thisSet.Add(new TestClass<T>());
                }

                for (int i = 0; i < SET_SIZE; i++)
                {
                    if (typeof(T) == typeof(Guid))
                    {
                        var o = thisSet[i] as TestClass<Guid>;
                        SET[i] = o.Id == DEFAULT_GUID;
                    }
                    else
                    {
                        var o = thisSet[i] as TestClass<long>;
                        SET[i] = o.Id == DEFAULT_LONG;
                    }
                }
            }
        }

        public class TestClass<T> where T : struct
        {
            public T Id { get; set; }
        }
    }
}
