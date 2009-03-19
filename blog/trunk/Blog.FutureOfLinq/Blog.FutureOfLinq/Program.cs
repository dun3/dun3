using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.IO;

namespace Blog.FutureOfLinq
{
    class Program
    {
        //private const int COUNT = 20000000;
        private static readonly int[,] COUNTS = 
        {
        { 10, 5000 }, 
        { 15, 5000 }, 
        { 30, 5000 }, 
        { 60, 5000 }, 
        { 100, 5000 },
        { 150, 5000 },
        { 300, 5000 },
        { 600, 5000 },
        { 1000, 5000 },
        { 1500, 5000 },
        { 3000, 5000 },
        { 6000, 5000 },
        { 10000, 5000 },
        { 15000, 5000 },
        { 30000, 5000 },
        { 60000, 5000 },
        { 100000, 500 },
        { 150000, 500 },
        { 300000, 500 },
        { 600000, 500 },
        { 1000000, 100 },
        { 3000000, 100 },
        { 10000000, 10 },
        { 50000000, 5 }
        };

        static void Main(string[] args)
        {
            using (StreamWriter s = new StreamWriter("result_" + DateTime.Now.Ticks + ".csv"))
            {

                for (int c = 0; c < COUNTS.GetLength(0); c++)
                {
                    int COUNT = COUNTS[c, 0];
                    int INNER_COUNT = COUNTS[c, 1];

                    Random random = new Random();
                    List<int> list = new List<int>(COUNT);
                    for (int i = 0; i < COUNT; i++)
                    {
                        list.Add(random.Next(int.MinValue, int.MaxValue));
                    }

                    Stopwatch sw = new Stopwatch();
                    int count = 0;

                    for (int i = 0; i < INNER_COUNT; i++)
                    {
                        var thisone = list.ToArray();
                        Thread.MemoryBarrier();

                        sw.Start();
                        Thread.MemoryBarrier();
                        var asQueryable = thisone.AsQueryable();
                        var filter1 = FilterPositive(asQueryable);
                        var filter2 = FilterBigger100(filter1);
                        count += filter2.Count();
                        Thread.MemoryBarrier();
                        sw.Stop();
                    }

                    GC.Collect();

                    Stopwatch sw2 = new Stopwatch();
                    int count2 = 0;
                    for (int i = 0; i < INNER_COUNT; i++)
                    {
                        var thisone = list.ToArray();
                        Thread.MemoryBarrier();

                        sw2.Start();
                        Thread.MemoryBarrier();
                        var asQueryable = thisone.AsQueryable();
                        var filter2 = FilterBigger100(asQueryable);
                        count2 += filter2.Count();
                        Thread.MemoryBarrier();
                        sw2.Stop();
                    }

                    GC.Collect();

                    Stopwatch sw3 = new Stopwatch();
                    int count3 = 0;
                    for (int i = 0; i < INNER_COUNT; i++)
                    {
                        var thisone = list.ToArray();
                        Thread.MemoryBarrier();

                        sw3.Start();
                        Thread.MemoryBarrier();
                        var asEnumerable = FilterPositive(thisone);
                        var filter2 = FilterBigger100(asEnumerable);
                        count3 += filter2.Count();
                        Thread.MemoryBarrier();
                        sw3.Stop();
                    }

                    GC.Collect();

                    Stopwatch sw3a = new Stopwatch();
                    int count3a = 0;
                    for (int i = 0; i < INNER_COUNT; i++)
                    {
                        var thisone = list.ToArray();
                        Thread.MemoryBarrier();

                        sw3a.Start();
                        Thread.MemoryBarrier();
                        var filter2 = FilterBigger100(thisone);
                        count3a += filter2.Count();
                        Thread.MemoryBarrier();
                        sw3a.Stop();
                    }

                    GC.Collect();

                    Stopwatch sw4 = new Stopwatch();
                    int count4 = 0;
                    for (int i = 0; i < INNER_COUNT; i++)
                    {
                        var thisone = list.ToArray();
                        Thread.MemoryBarrier();

                        sw4.Start();
                        Thread.MemoryBarrier();
                        var asQueryable = thisone.AsEnumerable();
                        var filter2 = FilterBigger100Yield(asQueryable);
                        Thread.MemoryBarrier();
                        count4 += filter2.Count();
                        sw4.Stop();
                    }

                    GC.Collect();

                    Stopwatch sw5 = new Stopwatch();
                    int count5 = 0;
                    for (int i = 0; i < INNER_COUNT; i++)
                    {
                        var thisone = list.ToArray();
                        Thread.MemoryBarrier();

                        sw5.Start();
                        Thread.MemoryBarrier();
                        var asQueryable = thisone.AsEnumerable();
                        var filter2 = FilterBigger100List(asQueryable);
                        count5 += filter2.Count();
                        Thread.MemoryBarrier();
                        sw5.Stop();
                    }

                    GC.Collect();

                    Stopwatch sw6 = new Stopwatch();
                    int count6 = 0;
                    for (int i = 0; i < INNER_COUNT; i++)
                    {
                        var thisone = list.ToArray();
                        Thread.MemoryBarrier();

                        sw6.Start();
                        Thread.MemoryBarrier();
                        var asQueryable = thisone.AsQueryable();
                        var filter1 = FilterPositive(asQueryable);
                        var filter2 = FilterBigger100(filter1);
                        var filter3 = ConvertQueryable(filter2);
                        count6 += filter3.Count();
                        Thread.MemoryBarrier();
                        sw6.Stop();
                    }

                    GC.Collect();

                    Stopwatch sw6a = new Stopwatch();
                    int count6a = 0;
                    for (int i = 0; i < INNER_COUNT; i++)
                    {
                        var thisone = list.ToArray();
                        Thread.MemoryBarrier();

                        sw6a.Start();
                        Thread.MemoryBarrier();
                        var asQueryable = thisone.AsQueryable();
                        var filter1 = FilterBigger100(asQueryable);
                        var filter2 = FilterPositive(filter1);
                        var filter3 = ConvertQueryable(filter2);
                        count6a += filter3.Count();
                        Thread.MemoryBarrier();
                        sw6a.Stop();
                    }

                    Console.WriteLine();
                    Console.WriteLine("Count: " + COUNT);
                    Console.WriteLine("-------------");
                    Console.WriteLine("1 ChainedWhere: " + sw.ElapsedMilliseconds + " " + count);
                    Console.WriteLine("2 OneWhere:     " + sw2.ElapsedMilliseconds + " " + count2);
                    Console.WriteLine("3 ChainedEnum:  " + sw3.ElapsedMilliseconds + " " + count3);
                    Console.WriteLine("4 OneEnum:      " + sw3a.ElapsedMilliseconds + " " + count3a);
                    Console.WriteLine("5 Yield:        " + sw4.ElapsedMilliseconds + " " + count4);
                    Console.WriteLine("6 OwnList:      " + sw5.ElapsedMilliseconds + " " + count5);
                    Console.WriteLine("7 Reduced:      " + sw6.ElapsedMilliseconds + " " + count6);
                    Console.WriteLine("8 ReducedRev:   " + sw6a.ElapsedMilliseconds + " " + count6a);
                    Console.WriteLine("-------------");
                    Console.WriteLine("1 to 8: " + Result(sw.ElapsedMilliseconds, sw6a.ElapsedMilliseconds));
                    Console.WriteLine("2 to 8: " + Result(sw2.ElapsedMilliseconds, sw6a.ElapsedMilliseconds));
                    Console.WriteLine("3 to 8: " + Result(sw3.ElapsedMilliseconds, sw6a.ElapsedMilliseconds));
                    Console.WriteLine("4 to 8: " + Result(sw3a.ElapsedMilliseconds, sw6a.ElapsedMilliseconds));
                    Console.WriteLine("5 to 8: " + Result(sw4.ElapsedMilliseconds, sw6a.ElapsedMilliseconds));
                    Console.WriteLine("6 to 8: " + Result(sw5.ElapsedMilliseconds, sw6a.ElapsedMilliseconds));
                    Console.WriteLine("7 to 8: " + Result(sw6.ElapsedMilliseconds, sw6a.ElapsedMilliseconds));

                    Func<long, double> conv = (k) =>
                    {
                        return (1.0 * k) / (COUNT * INNER_COUNT);
                    };

                    s.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7}",
                        conv(sw.ElapsedMilliseconds),
                        conv(sw2.ElapsedMilliseconds),
                        conv(sw3.ElapsedMilliseconds),
                        conv(sw3a.ElapsedMilliseconds),
                        conv(sw4.ElapsedMilliseconds),
                        conv(sw5.ElapsedMilliseconds),
                        conv(sw6.ElapsedMilliseconds),
                        conv(sw6a.ElapsedMilliseconds));

                    GC.Collect();
                    Thread.Sleep(500);
                    //Console.Read();
                }
            }
        }

        private static string Result(long time, long time8)
        {
            if (time > time8)
            {
                return string.Format("{0:f3} : 1", (1.0 * time) / time8);
            }
            else
            {
                return string.Format("1 : {0:f3}", (1.0 * time8) / time);
            }
        }

        private static IQueryable<int> ConvertQueryable(IQueryable<int> asQueryable)
        {
            WhereSimplifier simp = new WhereSimplifier();

            var e = simp.Visit(asQueryable.Expression);
            //EnumerableQueryable<int>
            //asQueryable.Expression = e;
            Assembly a = typeof(Queryable).Assembly;
            var t = a.GetType("System.Linq.EnumerableQuery`1", true);
            var t2 = t.MakeGenericType(typeof(int));
            var f = t2.GetField("expression", BindingFlags.Instance | BindingFlags.NonPublic);
            f.SetValue(asQueryable, e);
            return asQueryable;
        }

        private static IQueryable<int> FilterBigger100(IQueryable<int> asQueryable)
        {
            return from s2 in asQueryable
                   where s2 > 100
                   select s2;
        }

        private static IEnumerable<int> FilterBigger100(IEnumerable<int> asEnumerable)
        {
            return from s2 in asEnumerable
                   where s2 > 100
                   select s2;
        }

        private static IEnumerable<int> FilterBigger100Yield(IEnumerable<int> asEnumerable)
        {
            foreach (var item in asEnumerable)
            {
                if (item > 100)
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<int> FilterBigger100List(IEnumerable<int> asEnumerable)
        {
            List<int> list = new List<int>();
            foreach (var item in asEnumerable)
            {
                if (item > 100)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        private static IQueryable<int> FilterPositive(IQueryable<int> asQueryable)
        {
            return from s in asQueryable
                   where s > 0
                   select s;
        }

        private static IEnumerable<int> FilterPositive(IEnumerable<int> asEnumerable)
        {
            return from s in asEnumerable
                   where s > 0
                   select s;
        }
    }
}
