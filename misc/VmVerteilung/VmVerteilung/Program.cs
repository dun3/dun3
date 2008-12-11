using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace VmVerteilung
{
    public class Program
    {
        private static readonly double MINIMUM_FREE = 5.0;

        static bool m_forcenew = false;
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "--forcenew")
                {
                    m_forcenew = true;
                }
            }

            List<Storage> storagez = LoadStorage();
            List<Vm> vmz = LoadVm();

            CategorizeVmz(vmz, storagez.Count);


            SortToBuckets(vmz, storagez);

            CheckOvercommitted(storagez);

            PrintStoragez(storagez);

            Console.Read();
        }

        private static void CheckOvercommitted(List<Storage> storagez)
        {
            var overcommitted = (from s in storagez
                                 where s.IsOvercommitted
                                 select s).ToList();
            if (overcommitted.Count > 0)
            {
                Console.WriteLine("overcommitted: " + overcommitted.Count);
                foreach (var item in overcommitted)
                {
                    Console.WriteLine(item.Id);
                }
                throw new Exception("Found overcommitted");
            }

            var overEight = (from s in storagez
                             where s.Vmz.Count > 8
                             select s).ToList();
            if (overEight.Count > 0)
            {
                Console.WriteLine("Overeight: " + overEight.Count);
                foreach (var item in overEight)
                {
                    Console.WriteLine(item.Id);
                }
                throw new Exception("Found overEight");
            }
        }

        private static void PrintStoragez(List<Storage> storagez)
        {
            FileInfo fi = new FileInfo(@"out.txt");
            if (fi.Exists)
            {
                fi.Delete();
            }
            using (StreamWriter sw = new StreamWriter(fi.FullName))
            {
                foreach (var item in storagez)
                {
                    sw.WriteLine(item.Id);
                    sw.WriteLine("  Unassigned: " + item.Unassigned);
                    sw.WriteLine("  Vmz: " + item.Vmz.Count);
                    foreach (var vm in item.Vmz)
                    {
                        sw.WriteLine("    " + vm.Id);
                        sw.WriteLine("    " + vm.IoType + " (" + vm.Io + ")");
                        sw.WriteLine("    " + vm.Size);
                    }
                    sw.WriteLine("  Total burden: " + item.Burden);
                }


                var sortByBurden = from s in storagez
                                   let t = s.Burden
                                   orderby t descending
                                   select new { Io = t, Store = s };

                sw.WriteLine("Sort by burden:");
                foreach (var item in sortByBurden)
                {
                    sw.WriteLine(item.Store.Id + ": " + item.Io);
                }

                var sortByUnassigned = from s in storagez
                                       orderby s.Unassigned descending
                                       select s;

                sw.WriteLine("Sort by Unassigned:");
                foreach (var item in sortByUnassigned)
                {
                    double fromHot = item.Vmz.Where((vm) => { return vm.IoType == IoType.Hot; }).Aggregate(0.0, (before, next) => { return before + next.Size; });
                    double fromMedium = item.Vmz.Where((vm) => { return vm.IoType == IoType.Medium; }).Aggregate(0.0, (before, next) => { return before + next.Size; });
                    double fromLow = item.Vmz.Where((vm) => { return vm.IoType == IoType.Low; }).Aggregate(0.0, (before, next) => { return before + next.Size; });
                    sw.WriteLine(item.Id + ": " + item.Unassigned + " - h: " + fromHot + " m: " + fromMedium + " l: " + fromLow);
                }
            }
        }

        private static void SortToBuckets(List<Vm> vmz, List<Storage> storagez)
        {
            SortInHotz(vmz, storagez);

            SortInMediumz(vmz, storagez);

            SortInLow(vmz, storagez);
        }

        private static void SortInLow(List<Vm> vmz, List<Storage> storagez)
        {
            var largeToSmall = from s in vmz
                               where s.IoType == IoType.Low
                               orderby s.Size descending
                               select s;

            foreach (var item in largeToSmall)
            {

                var firstThatFits = (from s in storagez
                                     where s.Vmz.Count < 8
                                     where s.Unassigned - item.Size > MINIMUM_FREE
                                     orderby s.Unassigned ascending
                                     select s).FirstOrDefault();

                if (firstThatFits == null) { throw new Exception("could not find any space left for low " + item.Id); }

                firstThatFits.Vmz.Add(item);
            }
        }

        private static void SortInMediumz(List<Vm> vmz, List<Storage> storagez)
        {

            var mediumz = (from s in vmz
                           where s.IoType == IoType.Medium
                           orderby s.Io descending
                           select s).ToList();

            // Do an averaging by IO

            foreach (var medium in mediumz)
            {
                Storage applicableStorage = (from s in storagez
                                             where s.Unassigned - medium.Size > MINIMUM_FREE
                                             where s.Vmz.Count < 8
                                             orderby s.Burden ascending
                                             select s).FirstOrDefault();
                if (applicableStorage == null)
                {
                    throw new Exception("Could not find solution for IO averaging. Sorry");
                }

                applicableStorage.Vmz.Add(medium);
            }
        }

        private static void SortInHotz(List<Vm> vmz, List<Storage> storagez)
        {
            var hotz = from s in vmz
                       where s.IoType == IoType.Hot
                       select s;

            int i = 0;
            foreach (var item in hotz)
            {
                storagez[i].Vmz.Add(item);
                i++;
            }
        }

        private static void CategorizeVmz(List<Vm> vmz, int buckets)
        {
            var sorted = from s in vmz
                         orderby s.Io descending
                         select s;

            int i = 0;
            foreach (var item in sorted)
            {
                if (i < buckets)
                {
                    item.IoType = IoType.Hot;
                }
                else if (i < buckets * 3)
                {
                    item.IoType = IoType.Medium;
                }
                else
                {
                    item.IoType = IoType.Low;
                }
                i++;
                Console.WriteLine(item.Id);
                Console.WriteLine(item.IoType + " " + item.Io);
            }
        }

        private static List<Vm> LoadVm()
        {
            XDocument doc = XDocument.Load(@"vm.xml");

            var vmz =
                (
                from item in doc.Descendants("MS")
                let s = item.Elements().ToArray()
                select new Vm()
                {
                    Id = s[0].Value,
                    Io = double.Parse(s[3].Value, CultureInfo.InvariantCulture.NumberFormat),
                    Size = double.Parse(s[1].Value, CultureInfo.InvariantCulture.NumberFormat),
                    IoType = IoType.Low
                }
                ).ToList();


            return vmz;
        }

        private static List<Storage> LoadStorage()
        {
            XDocument doc = XDocument.Load(@"stores.xml");

            var storagez =
                (
                from item in doc.Descendants("MS")
                let s = item.Elements().ToArray()
                select new Storage()
                {
                    Id = s[0].Value,
                    Capacity = double.Parse(s[1].Value, CultureInfo.InvariantCulture.NumberFormat),
                    Vmz = new List<Vm>()
                }
                ).ToList();

            return storagez;
        }
    }
}
