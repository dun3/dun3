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
                    sw.WriteLine("  Total burden: " + item.Vmz.Aggregate(0.0, (before, next) => { return before + next.Io; }));
                }


                var sortByBurden = from s in storagez
                                   let t = s.Vmz.Aggregate(0.0, (before, next) => { return before + next.Io; })
                                   orderby t descending
                                   select new { Io = t, Store = s };

                sw.WriteLine("Sort by burden:");
                foreach (var item in sortByBurden)
                {
                    sw.WriteLine(item.Store.Id + ":" + item.Io);
                }

                var sortByUnassigned = from s in storagez
                                       orderby s.Unassigned descending
                                       select s;

                sw.WriteLine("Sort by Unassigned:");
                foreach (var item in sortByUnassigned)
                {
                    sw.WriteLine(item.Id + ":" + item.Unassigned);
                }

            }
        }

        private static void SortToBuckets(List<Vm> vmz, List<Storage> storagez)
        {
            SortInHotz(vmz, storagez);

            SortInMediumz(vmz, storagez);

            //SortInLow(vmz, storagez);
        }

        private static void SortInLow(List<Vm> vmz, List<Storage> storagez)
        {
            var lowz = new LinkedList<Vm>(
                from s in vmz
                where s.IoType == IoType.Low
                orderby s.Io descending
                select s);


            double totalUnassigned = storagez.Aggregate(
                0.0,
                (before, item) =>
                {
                    return before + item.Unassigned;
                });

            double totalToAssign = lowz.Aggregate(
                0.0,
                (before, item) =>
                {
                    return before + item.Size;
                });

            if (totalUnassigned < totalToAssign) { throw new Exception("More to assign (" + totalToAssign + ") than unassigned (" + totalUnassigned + ")"); }

            double targetUnassigned = (totalUnassigned - totalToAssign) / storagez.Count;

            List<Storage> applicableStoragez;
            do
            {
                applicableStoragez = (from s in storagez
                                      where s.Unassigned > targetUnassigned
                                      where s.Vmz.Count < 8
                                      orderby s.Unassigned descending
                                      select s).ToList();
                foreach (var item in applicableStoragez)
                {
                    item.Vmz.Add(lowz.First.Value);
                    lowz.RemoveFirst();
                }

            } while ((applicableStoragez.Count > 0) && (lowz.Count > 0));

            if (lowz.Count > 0)
            {
                foreach (var item in lowz)
                {
                    var lowest = (from s in storagez
                                  where s.Vmz.Count < 8
                                  orderby s.Unassigned descending
                                  select s).First();
                    lowest.Vmz.Add(item);
                }
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
                                             where s.Unassigned - medium.Size > 0.0
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
