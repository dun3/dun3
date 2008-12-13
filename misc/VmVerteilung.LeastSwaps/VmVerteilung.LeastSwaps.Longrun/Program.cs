using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace VmVerteilung.LeastSwaps.Longrun
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int storeCount = 13;
            List<Store> list = GenerateBefore(storeCount, 84);

            var vmz = (from s in list
                       from t in s.Vmz
                       select t).ToList();

            List<TargetIdentifier> targetz = Distribute<TargetIdentifier>(storeCount, vmz, (id) => { return new TargetIdentifier(id); }, (target) => { return target.Definition; });

            int bruteForceCount = 0;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                List<Vm> t1 = new List<Vm>(list[i].Vmz);
                List<Vm> t2 = new List<Vm>(targetz[i].Definition);

                foreach (var item in t1)
                {
                    t2.Remove(item);
                }

                bruteForceCount += t2.Count;
            }


            SwapGenerator gen = new SwapGenerator();

            Stopwatch sw = new Stopwatch();

            Thread.Sleep(500);

            sw.Start();

            var swaps = gen.GenerateSwaps(list, targetz);

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(swaps.Count);
            Console.WriteLine(bruteForceCount);
            Console.Read();

        }

        static Random m_random = new Random();

        private static List<Store> GenerateBefore(int storeCount, int vmCount)
        {
            List<Vm> vmz = new List<Vm>(vmCount);
            for (int i = 0; i < vmCount; i++)
            {
                vmz.Add(new Vm(i.ToString()));
            }

            List<Store> storez = Distribute<Store>(storeCount, vmz, (id) => { return new Store(id); }, (store) => { return store.Vmz; });

            return storez;
        }

        private static List<T> Distribute<T>(int storeCount, List<Vm> vmz, Func<string, T> construct, Func<T, List<Vm>> inner)
        {
            List<Vm> copyVmz = new List<Vm>(vmz);

            List<T> storez = new List<T>(storeCount);
            for (int i = 0; i < storeCount - 1; i++)
            {
                T store = construct(i.ToString());

                int count = m_random.Next(0, vmz.Count * 2 / storeCount) + 1;

                for (int j = 0; j < count; j++)
                {
                    if (copyVmz.Count > 0)
                    {
                        Vm vm = copyVmz[m_random.Next(0, copyVmz.Count)];
                        inner(store).Add(vm);
                        copyVmz.Remove(vm);
                    }
                }

                storez.Add(store);
            }

            T last = construct((storeCount - 1).ToString());
            foreach (var item in copyVmz)
            {
                inner(last).Add(item);
            }
            storez.Add(last);

            return storez;
        }
    }
}
