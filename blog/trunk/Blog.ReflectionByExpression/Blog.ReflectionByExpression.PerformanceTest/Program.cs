﻿
using System;
using System.Diagnostics;
using System.Threading;
namespace Blog.ReflectionByExpression.PerformanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoSource source = new AutoSource(1, "1", new object(), Guid.NewGuid(), 1);
            Target target = new Target();

            {
                Stopwatch stopwatchBasicAccessor = new Stopwatch();
                {
                    IAccessor accessor = AccessorFactory.CreateBasicAccessor(typeof(AutoSource), "Prop1");

                    stopwatchBasicAccessor = TakeTime(IterateOverIntReadTests((i) =>
                    {
                        source.Prop1 = i;

                        return (int)accessor.GetValue(source);
                    }));
                }

                Stopwatch stopwatchBasicAccessorGeneric = new Stopwatch();
                {
                    IAccessor<int> accessor = AccessorFactory.CreateBasicAccessor<int>(typeof(AutoSource), "Prop1");

                    stopwatchBasicAccessorGeneric = TakeTime(IterateOverIntReadTests((i) =>
                    {
                        source.Prop1 = i;

                        return accessor.GetValue(source);
                    }));
                }

                Stopwatch stopwatchBasicAccessorTyped = new Stopwatch();
                {
                    IAccessor<AutoSource, int> accessor = AccessorFactory.CreateBasicAccessor<AutoSource, int>("Prop1");

                    stopwatchBasicAccessorTyped = TakeTime(IterateOverIntReadTests((i) =>
                    {
                        source.Prop1 = i;

                        return accessor.GetValue(source);
                    }));
                }

                Console.WriteLine("stopwatchBasicAccessor: " + stopwatchBasicAccessor.ElapsedMilliseconds);
                Console.WriteLine("stopwatchBasicAccessorGeneric: " + stopwatchBasicAccessorGeneric.ElapsedMilliseconds);
                Console.WriteLine("stopwatchBasicAccessorTyped: " + stopwatchBasicAccessorTyped.ElapsedMilliseconds);
            }

            {
                Stopwatch stopwatchEmitAccessor = new Stopwatch();
                {
                    IAccessor accessor = AccessorFactory.CreateEmitAccessor(typeof(AutoSource), "Prop1");

                    stopwatchEmitAccessor = TakeTime(IterateOverIntReadTests((i) =>
                    {
                        source.Prop1 = i;

                        return (int)accessor.GetValue(source);
                    }));
                }

                Stopwatch stopwatchEmitAccessorGeneric = new Stopwatch();
                {
                    IAccessor<int> accessor = AccessorFactory.CreateEmitAccessor<int>(typeof(AutoSource), "Prop1");

                    stopwatchEmitAccessorGeneric = TakeTime(IterateOverIntReadTests((i) =>
                    {
                        source.Prop1 = i;

                        return accessor.GetValue(source);
                    }));
                }

                Stopwatch stopwatchEmitAccessorTyped = new Stopwatch();
                {
                    IAccessor<AutoSource, int> accessor = AccessorFactory.CreateEmitAccessor<AutoSource, int>("Prop1");

                    stopwatchEmitAccessorTyped = TakeTime(IterateOverIntReadTests((i) =>
                    {
                        source.Prop1 = i;

                        return accessor.GetValue(source);
                    }));
                }

                Console.WriteLine("stopwatchEmitAccessor: " + stopwatchEmitAccessor.ElapsedMilliseconds);
                Console.WriteLine("stopwatchEmitAccessorGeneric: " + stopwatchEmitAccessorGeneric.ElapsedMilliseconds);
                Console.WriteLine("stopwatchEmitAccessorTyped: " + stopwatchEmitAccessorTyped.ElapsedMilliseconds);
            }


            {
                //Stopwatch stopwatchExpressionAccessor = new Stopwatch();
                //{
                //    IAccessor accessor = AccessorFactory.CreateExpressionAccessor(typeof(AutoSource), "Prop1");

                //    stopwatchExpressionAccessor = TakeTime(IterateOverIntReadTests((i) =>
                //    {
                //        source.Prop1 = i;

                //        return (int)accessor.GetValue(source);
                //    }));
                //}

                //Stopwatch stopwatchExpressionAccessorGeneric = new Stopwatch();
                //{
                //    IAccessor<int> accessor = AccessorFactory.CreateExpressionAccessor<int>(typeof(AutoSource), "Prop1");

                //    stopwatchExpressionAccessorGeneric = TakeTime(IterateOverIntReadTests((i) =>
                //    {
                //        source.Prop1 = i;

                //        return accessor.GetValue(source);
                //    }));
                //}

                Stopwatch stopwatchExpressionAccessorTyped = new Stopwatch();
                {
                    IAccessor<AutoSource, int> accessor = AccessorFactory.CreateExpressionAccessor<AutoSource, int>("Prop1");

                    stopwatchExpressionAccessorTyped = TakeTime(IterateOverIntReadTests((i) =>
                    {
                        source.Prop1 = i;

                        return accessor.GetValue(source);
                    }));
                }

                //Console.WriteLine("stopwatchExpressionAccessor: " + stopwatchExpressionAccessor.ElapsedMilliseconds);
                //Console.WriteLine("stopwatchExpressionAccessorGeneric: " + stopwatchExpressionAccessorGeneric.ElapsedMilliseconds);
                Console.WriteLine("stopwatchExpressionAccessorTyped: " + stopwatchExpressionAccessorTyped.ElapsedMilliseconds);
            }

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

        public static Action<bool> IterateOverIntReadTests(Func<int, int> func)
        {
            return (doFullRun) =>
            {
                int a = 0;
                if (doFullRun)
                {
                    for (int i = 0; i < 20000000; i++)
                    {
                        a = func(i);
                    }
                }
                else
                {
                    // prerun usually to get JIT out of the way
                    a = func(0);
                }
            };
        }
    }

    public class AutoSource
    {
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

        public void SetProp1(int prop1)
        {
            Prop1 = prop1;
        }
    }

    public class Target
    {
        public int Prop1 { get; set; }
        public string Prop2 { get; set; }
        public object Prop3 { get; set; }
        public Guid Prop4 { get; set; }
        public int Prop5 { get; set; }
    }
}
