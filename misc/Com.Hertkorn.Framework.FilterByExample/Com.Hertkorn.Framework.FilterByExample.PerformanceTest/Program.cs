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
        public static void Main(string[] args)
        {
            EnumerableTest eTest = new EnumerableTest();

            eTest.SetUp();

            Stopwatch sw = new Stopwatch();

            Thread.Sleep(500);
            sw.Start();

            for (int i = 0; i < 1000; i++)
            {
                eTest.NoIgnoredPropertiesNoHitTest();
                eTest.NoIgnoredProperties1HitTest();
                eTest.NoIgnoredPropertiesMultipleHitsTest();
                eTest.NoIgnoredPropertiesMultiTest();
                eTest.OneIgnoredPropertiesTest();
                eTest.TwoIgnoredPropertiesTest();
            }

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);

            EnumerableTest qTest = new EnumerableTest();

            qTest.SetUp();

            sw = new Stopwatch();

            Thread.Sleep(500);

            sw.Start();

            for (int i = 0; i < 1000; i++)
            {
                qTest.NoIgnoredPropertiesNoHitTest();
                qTest.NoIgnoredProperties1HitTest();
                qTest.NoIgnoredPropertiesMultipleHitsTest();
                qTest.NoIgnoredPropertiesMultiTest();
                qTest.OneIgnoredPropertiesTest();
                qTest.TwoIgnoredPropertiesTest();
            }

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
}
