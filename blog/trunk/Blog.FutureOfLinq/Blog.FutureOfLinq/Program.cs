using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace Blog.FutureOfLinq
{
    class Program
    {
        private const int COUNT = 20000000;
        private const int INNER_COUNT = 10;
        static void Main(string[] args)
        {
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
                Thread.Sleep(50);

                sw.Start();
                var asQueryable = thisone.AsQueryable();
                var filter1 = FilterPositive(asQueryable);
                var filter2 = FilterBigger100(filter1);
                count += filter2.Count();
                sw.Stop();
                GC.Collect();
                //Console.Write(i + " ");
            }
            
            Stopwatch sw2 = new Stopwatch();
            int count2 = 0;
            for (int i = 0; i < INNER_COUNT; i++)
            {
                var thisone = list.ToArray();
                Thread.Sleep(50);

                sw2.Start();
                var asQueryable = thisone.AsQueryable();
                var filter2 = FilterBigger100(asQueryable);
                count2 += filter2.Count();
                sw2.Stop();
                GC.Collect();
                //Console.Write(i + " ");
            }

            Stopwatch sw3 = new Stopwatch();
            int count3 = 0;
            for (int i = 0; i < INNER_COUNT; i++)
            {
                var thisone = list.ToArray();
                Thread.Sleep(50);

                sw3.Start();
                var asEnumerable = FilterPositive(thisone);
                var filter2 = FilterBigger100(asEnumerable);
                count3 += filter2.Count();
                sw3.Stop();
                GC.Collect();
                //Console.Write(i + " ");
            }

            Stopwatch sw3a = new Stopwatch();
            int count3a = 0;
            for (int i = 0; i < INNER_COUNT; i++)
            {
                var thisone = list.ToArray();
                Thread.Sleep(50);

                sw3a.Start();
                var filter2 = FilterBigger100(thisone);
                count3a += filter2.Count();
                sw3a.Stop();
                GC.Collect();
                //Console.Write(i + " ");
            }

            Stopwatch sw4 = new Stopwatch();
            int count4 = 0;
            for (int i = 0; i < INNER_COUNT; i++)
            {
                var thisone = list.ToArray();
                Thread.Sleep(50);

                sw4.Start();
                var asQueryable = thisone.AsEnumerable();
                var filter2 = FilterBigger100Yield(asQueryable);
                count4 += filter2.Count();
                sw4.Stop();
                GC.Collect();
                //Console.Write(i + " ");
            }

            Stopwatch sw5 = new Stopwatch();
            int count5 = 0;
            for (int i = 0; i < INNER_COUNT; i++)
            {
                var thisone = list.ToArray();
                Thread.Sleep(50);

                sw5.Start();
                var asQueryable = thisone.AsEnumerable();
                var filter2 = FilterBigger100List(asQueryable);
                count5 += filter2.Count();
                sw5.Stop();
                GC.Collect();
                //Console.Write(i + " ");
            }

            Stopwatch sw6 = new Stopwatch();
            int count6 = 0;
            for (int i = 0; i < INNER_COUNT; i++)
            {
                var thisone = list.ToArray();
                Thread.Sleep(50);

                sw6.Start();
                var asQueryable = thisone.AsQueryable();
                var filter1 = FilterPositive(asQueryable);
                var filter2 = FilterBigger100(filter1);
                var filter3 = ConvertQueryable(filter2);
                count6 += filter3.Count();
                sw6.Stop();
                GC.Collect();
                //Console.Write(i + " ");
            }

            Stopwatch sw6a = new Stopwatch();
            int count6a = 0;
            for (int i = 0; i < INNER_COUNT; i++)
            {
                var thisone = list.ToArray();
                Thread.Sleep(50);

                sw6a.Start();
                var asQueryable = thisone.AsQueryable();
                var filter1 = FilterBigger100(asQueryable);
                var filter2 = FilterPositive(filter1);
                var filter3 = ConvertQueryable(filter2);
                count6a += filter3.Count();
                sw6a.Stop();
                GC.Collect();
                //Console.Write(i + " ");
            }

            Console.WriteLine();
            Console.WriteLine("ChainedWhere: " + sw.ElapsedMilliseconds + " " + count);
            Console.WriteLine("OneWhere:     " + sw2.ElapsedMilliseconds + " " + count2);
            Console.WriteLine("ChainedEnum:  " + sw3.ElapsedMilliseconds + " " + count3);
            Console.WriteLine("OneEnum:      " + sw3a.ElapsedMilliseconds + " " + count3a);
            Console.WriteLine("Yield:        " + sw4.ElapsedMilliseconds + " " + count4);
            Console.WriteLine("OwnList:      " + sw5.ElapsedMilliseconds + " " + count5);
            Console.WriteLine("Reduced:      " + sw6.ElapsedMilliseconds + " " + count6);
            Console.WriteLine("ReducedRev:   " + sw6a.ElapsedMilliseconds + " " + count6a);

            //Console.Read();
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
            return from s in asQueryable
                   where s > 100
                   select s;
        }

        private static IEnumerable<int> FilterBigger100(IEnumerable<int> asQueryable)
        {
            return from s in asQueryable
                   where s > 100
                   select s;
        }

        private static IEnumerable<int> FilterBigger100Yield(IEnumerable<int> asQueryable)
        {
            foreach (var item in asQueryable)
            {
                if (item > 100)
                {
                    yield return item;
                }
            }
        }

        private static IEnumerable<int> FilterBigger100List(IEnumerable<int> asQueryable)
        {
            List<int> list = new List<int>();
            foreach (var item in asQueryable)
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

        private static IEnumerable<int> FilterPositive(IEnumerable<int> asQueryable)
        {
            return from s in asQueryable
                   where s > 0
                   select s;
        }
    }
}
