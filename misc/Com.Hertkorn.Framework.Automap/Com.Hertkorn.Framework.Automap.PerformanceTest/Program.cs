using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Com.Hertkorn.Framework.Automap
{
    public class Program
    {
        public static void Main(string[] args)
        {

            AutoSource source = new AutoSource(1, "1", new object(), Guid.NewGuid(), 1);
            Target target = new Target();

            SimpleConverter simpleconverter = new SimpleConverter();
            Stopwatch stopwatchSimple = TakeTime(IterateOverTests(() => simpleconverter.Map(source, target)));


            var map = ManualMap<AutoSource, Target>.New
                .AddMap(a => a.Prop1, b => b.Prop1)
                .AddMap(a => a.Prop2, b => b.Prop2)
                .AddMap(a => a.Prop3, b => b.Prop3)
                .AddMap(a => a.Prop4, b => b.Prop4)
                .AddMap(a => a.Prop5, b => b.Prop5);

            Stopwatch stopwatchManual = TakeTime(IterateOverTests(() => map.Map(source, target)));

            Stopwatch stopwatchAuto = TakeTime(IterateOverTests(() => AutoConverter.AutoConvert(source, target)));

            LinqMapSource source2 = new LinqMapSource(1, 2, 3, 4, 5);
            LinqMapTarget target2 = new LinqMapTarget();

            var linqMap = LinqMap<LinqMapSource, LinqMapTarget>.New
                .AddMap(a => a.Source1, (a, b) => a.Target1 = b)
                .AddMap(a => a.Source2, (a, b) => a.Target2 = b)
                .AddMap(a => a.Source3, (a, b) => a.Target3 = b)
                .AddMap(a => a.Source4, (a, b) => a.Target4 = b)
                .AddMap(a => a.Source5, (a, b) => a.Target5 = b);

            Stopwatch stopwatchLinq = TakeTime(IterateOverTests(() => linqMap.Map(source2, target2)));

            Console.WriteLine(stopwatchSimple.ElapsedMilliseconds);
            Console.WriteLine(stopwatchManual.ElapsedMilliseconds);
            Console.WriteLine(stopwatchAuto.ElapsedMilliseconds);
            Console.WriteLine(stopwatchLinq.ElapsedMilliseconds);

            Console.Read();

        }

        public static Stopwatch TakeTime(Action<bool> action)
        {
            action(false);

            Thread.Sleep(50);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            action(true);

            stopwatch.Stop();

            return stopwatch;
        }

        public static Action<bool> IterateOverTests(Action action)
        {
            return (doFullRun) =>
            {
                if (doFullRun)
                {
                    for (int i = 0; i < 1000000; i++)
                    {
                        action();
                    }
                }
                else
                {
                    // prerun usually to get JIT out of the way
                    action();
                }
            };
        }

        public class AutoSource
        {
            /// <summary>
            /// Initializes a new instance of the AutoSource class.
            /// </summary>
            public AutoSource(int prop1, string prop2, object prop3, Guid prop4, int prop5)
            {
                Prop1 = prop1;
                Prop2 = prop2;
                Prop3 = prop3;
                Prop4 = prop4;
                Prop5 = prop5;
            }
            public int Prop1 { get; set; }
            public string Prop2 { get; set; }
            public object Prop3 { get; set; }
            public Guid Prop4 { get; set; }
            public int Prop5 { get; set; }
        }

        public class Target
        {
            public int Prop1 { get; set; }
            public string Prop2 { get; set; }
            public object Prop3 { get; set; }
            public Guid Prop4 { get; set; }
            public int Prop5 { get; set; }
        }


        public class LinqMapSource
        {
            /// <summary>
            /// Initializes a new instance of the Source class.
            /// </summary>
            public LinqMapSource(int source1, int source2, int source3, int source4, int source5)
            {
                Source1 = source1;
                Source2 = source2;
                Source3 = source3;
                Source4 = source4;
                Source5 = source5;
            }
            public int Source1 { get; set; }
            public int Source2 { get; set; }
            public int Source3 { get; set; }
            public int Source4 { get; set; }
            public int Source5 { get; set; }
        }


        public class LinqMapTarget
        {
            public int Target1 { get; set; }
            public int Target2 { get; set; }
            public int Target3 { get; set; }
            public int Target4 { get; set; }
            public int Target5 { get; set; }
        }
    }
}
