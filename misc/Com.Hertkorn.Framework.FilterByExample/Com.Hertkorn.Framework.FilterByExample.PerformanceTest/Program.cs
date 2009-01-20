using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Com.Hertkorn.Framework.FilterByExample.PerformanceTest
{
    public class Program
    {
        private const int ENUMERABLE_COUNT = 1000;
        private const int QUERYABLE_COUNT = 1000;
        private const int PRECOMPILED_ENUMERABLE_COUNT = 100000;

        public static void Main(string[] args)
        {
            EnumerableTest eTest = new EnumerableTest();

            eTest.SetUp();

            Stopwatch sw = new Stopwatch();

            //Thread.Sleep(500);
            //sw.Start();

            //for (int i = 0; i < ENUMERABLE_COUNT; i++)
            //{
            //    eTest.NoIgnoredPropertiesNoHitTest();
            //    eTest.NoIgnoredProperties1HitTest();
            //    eTest.NoIgnoredPropertiesMultipleHitsTest();
            //    eTest.NoIgnoredPropertiesMultiTest();
            //    eTest.OneIgnoredPropertiesTest();
            //    eTest.TwoIgnoredPropertiesTest();
            //}

            //sw.Stop();

            //Console.WriteLine(String.Format("{0} --> {1:F5} ms/op", sw.ElapsedMilliseconds, 1.0 * sw.ElapsedMilliseconds / GetOpCount(ENUMERABLE_COUNT)));

            //QueryableTest qTest = new QueryableTest();

            //qTest.SetUp();

            //sw = new Stopwatch();

            //Thread.Sleep(500);

            //sw.Start();

            //for (int i = 0; i < QUERYABLE_COUNT; i++)
            //{
            //    qTest.NoIgnoredPropertiesNoHitTest();
            //    qTest.NoIgnoredProperties1HitTest();
            //    qTest.NoIgnoredPropertiesMultipleHitsTest();
            //    qTest.NoIgnoredPropertiesMultiTest();
            //    qTest.OneIgnoredPropertiesTest();
            //    qTest.TwoIgnoredPropertiesTest();
            //}

            //sw.Stop();

            //Console.WriteLine(String.Format("{0} --> {1:F5} ms/op", sw.ElapsedMilliseconds, 1.0 * sw.ElapsedMilliseconds / GetOpCount(QUERYABLE_COUNT)));

            //PrecompiledEnumerableTest peTest = new PrecompiledEnumerableTest();

            //peTest.SetUp();

            //sw = new Stopwatch();

            //Thread.Sleep(500);

            //sw.Start();

            //for (int i = 0; i < PRECOMPILED_ENUMERABLE_COUNT; i++)
            //{
            //    peTest.NoIgnoredPropertiesNoHitTest();
            //    peTest.NoIgnoredProperties1HitTest();
            //    peTest.NoIgnoredPropertiesMultipleHitsTest();
            //    peTest.NoIgnoredPropertiesMultiTest();
            //    peTest.OneIgnoredPropertiesTest();
            //    peTest.TwoIgnoredPropertiesTest();
            //}

            //sw.Stop();

            //Console.WriteLine(String.Format("{0} --> {1:F5} ms/op", sw.ElapsedMilliseconds, 1.0 * sw.ElapsedMilliseconds / GetOpCount(PRECOMPILED_ENUMERABLE_COUNT)));

            //Console.WriteLine("---------------");

            QueryableTest q2Test = new QueryableTest();

            q2Test.SetUp();

            sw = new Stopwatch();

            Thread.Sleep(500);

            sw.Start();

            for (int i = 0; i < QUERYABLE_COUNT; i++)
            {
                q2Test.NoIgnoredPropertiesMultiTest();
            }

            sw.Stop();

            Console.WriteLine(String.Format("{0} --> {1:F5} ms/op", sw.ElapsedMilliseconds, 1.0 * sw.ElapsedMilliseconds / (3 * QUERYABLE_COUNT)));

            QuerycacheTest qcTest = new QuerycacheTest();

            qcTest.SetUp();

            sw = new Stopwatch();

            Thread.Sleep(500);

            sw.Start();

            for (int i = 0; i < QUERYABLE_COUNT; i++)
            {
                qcTest.NoIgnoredPropertiesMultiTest();
            }

            sw.Stop();

            Console.WriteLine(String.Format("{0} --> {1:F5} ms/op", sw.ElapsedMilliseconds, 1.0 * sw.ElapsedMilliseconds / (3 * QUERYABLE_COUNT)));


            Console.WriteLine("One Op = Filtering a list of 50 items length");
        }

        public static int GetOpCount(int count)
        {
            return count * 9;
        }
    }
}
