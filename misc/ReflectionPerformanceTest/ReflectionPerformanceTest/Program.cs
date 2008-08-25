using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Linq.Expressions;
using FastDynamicPropertyAccessor;
using System.Reflection;

namespace ReflectionPerformanceTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AutoSource source = new AutoSource(1, "1", new object(), Guid.NewGuid(), 1);
            Target target = new Target();

            Stopwatch stopwatchReflection = new Stopwatch();
            {
                var member = typeof(AutoSource).GetProperty("Prop1");

                stopwatchReflection = TakeTime(IterateOverIntReadTests((i) =>
                {
                    source.Prop1 = i;

                    return (int)member.GetValue(source, null);
                }));
            }



            Stopwatch stopwatchPropertyAccessor = new Stopwatch();
            {
                PropertyAccessor accessor = new PropertyAccessor(source.GetType(), "Prop1");
                stopwatchPropertyAccessor = TakeTime(IterateOverIntReadTests((i) =>
                {
                    source.Prop1 = i;

                    return (int)accessor.Get(source);
                }));
            }

            Stopwatch stopwatchUntyped = new Stopwatch();
            {
                ParameterExpression arg = Expression.Parameter(typeof(AutoSource), "test");
                MemberExpression expr = Expression.Property(arg, "Prop1");

                var untypedLambda = Expression.Lambda(expr, new ParameterExpression[] { arg });
                Delegate untypedLambdaCompile = untypedLambda.Compile();

                stopwatchUntyped = TakeTime(IterateOverIntReadTests((i) =>
                {
                    source.Prop1 = i;

                    return (int)untypedLambdaCompile.DynamicInvoke(source);
                }));
            }
            Stopwatch stopwatchTypedUntyped = new Stopwatch();
            {
                ParameterExpression objectArg = Expression.Parameter(typeof(object), "test");
                var objectConvert = Expression.ConvertChecked(objectArg, typeof(AutoSource));
                MemberExpression objectExpr = Expression.Property(objectConvert, "Prop1");
                var convert = Expression.Convert(objectExpr, typeof(object));

                Expression<Func<object, object>> typedUntypedLambda = Expression.Lambda<Func<object, object>>(convert, new ParameterExpression[] { objectArg });
                Func<object, object> typedUntypedLambdaCompile = typedUntypedLambda.Compile();

                stopwatchTypedUntyped = TakeTime(IterateOverIntReadTests((i) =>
                {
                    source.Prop1 = i;

                    return (int)typedUntypedLambdaCompile(source);
                }));
            }
            Stopwatch stopwatchSemiTyped = new Stopwatch();
            {
                ParameterExpression arg = Expression.Parameter(typeof(AutoSource), "test");
                MemberExpression expr = Expression.Property(arg, "Prop1");

                var convertTyped = Expression.Convert(expr, typeof(object));
                Expression<Func<AutoSource, object>> semiTypedLambda = Expression.Lambda<Func<AutoSource, object>>(convertTyped, new ParameterExpression[] { arg });
                Func<AutoSource, object> semiTypedLambdaCompile = semiTypedLambda.Compile();

                stopwatchSemiTyped = TakeTime(IterateOverIntReadTests((i) =>
                 {
                     source.Prop1 = i;

                     return (int)semiTypedLambdaCompile(source);
                 }));
            }
            Stopwatch stopwatchTyped = new Stopwatch();
            {
                ParameterExpression arg = Expression.Parameter(typeof(AutoSource), "test");
                MemberExpression expr = Expression.Property(arg, "Prop1");

                Expression<Func<AutoSource, int>> typedLambda = Expression.Lambda<Func<AutoSource, int>>(expr, new ParameterExpression[] { arg });
                Func<AutoSource, int> typedLambdaCompile = typedLambda.Compile();

                stopwatchTyped = TakeTime(IterateOverIntReadTests((i) =>
                {
                    source.Prop1 = i;

                    return typedLambdaCompile(source);
                }));
            }

            Console.WriteLine("stopwatchReflection: " + stopwatchReflection.ElapsedMilliseconds);
            Console.WriteLine("stopwatchPropertyAccessor: " + stopwatchPropertyAccessor.ElapsedMilliseconds);
            Console.WriteLine("stopwatchUntyped: " + stopwatchUntyped.ElapsedMilliseconds);
            Console.WriteLine("stopwatchTypedUntyped: " + stopwatchTypedUntyped.ElapsedMilliseconds);
            Console.WriteLine("stopwatchSemiTyped: " + stopwatchSemiTyped.ElapsedMilliseconds);
            Console.WriteLine("stopwatchTyped: " + stopwatchTyped.ElapsedMilliseconds);


            ParameterExpression arg2 = Expression.Parameter(typeof(AutoSource), "test");
            ParameterExpression arg3 = Expression.Parameter(typeof(int), "intValue");
            MethodCallExpression propertyValue = Expression.Call(arg2, typeof(AutoSource).GetProperty("Prop1").GetSetMethod(), new ParameterExpression[] { arg3 });

            var myexp = Expression.Lambda<Action<AutoSource, int>>(propertyValue, new ParameterExpression[] { arg2, arg3 });

            Console.WriteLine(myexp);

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
                    for (int i = 0; i < 10000000; i++)
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
