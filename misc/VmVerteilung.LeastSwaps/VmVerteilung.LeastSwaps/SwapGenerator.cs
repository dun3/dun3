using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VmVerteilung.LeastSwaps
{
    public class SwapGenerator
    {
        public List<Swap> GenerateSwaps(List<Store> before, List<List<Vm>> targetDefinition)
        {
            if (before.Count != targetDefinition.Count) { throw new ArgumentException("Count differs"); }
            int beforeVmCount = before.Aggregate(0, (count, next) => { return count + next.Vmz.Count; });
            int targetVmCount = targetDefinition.Aggregate(0, (count, next) => { return count + next.Count; });

            if (beforeVmCount != targetVmCount) { throw new ArgumentException("Vmcount differs"); }

            List<TargetIdentifier> targetz = new List<TargetIdentifier>();
            for (int i = 0; i < targetDefinition.Count; i++)
            {
                TargetIdentifier targetId = new TargetIdentifier(i.ToString());
                targetId.Definition.AddRange(targetDefinition[i]);
                targetz.Add(targetId);
            }

            return GenerateSwaps(before, targetz);
        }

        public List<Swap> GenerateSwaps(List<Store> before, List<TargetIdentifier> targetz)
        {
            List<TargetIdentifier> copyTargetz = new List<TargetIdentifier>(targetz);

            List<SwapPossibility> matches = new List<SwapPossibility>();

            // using some form of nearest neighbor

            int totalCount = 0;

            foreach (Store store in before)
            {
                TargetIdentifier optimalIdentifier = null;
                int swapCount = int.MaxValue;
                foreach (TargetIdentifier target in copyTargetz)
                {
                    List<Vm> storeVmz = new List<Vm>(store.Vmz);
                    List<Vm> targetVmz = new List<Vm>(target.Definition);

                    for (int i = storeVmz.Count - 1; i >= 0; i--)
                    {
                        for (int j = targetVmz.Count - 1; j >= 0; j--)
                        {
                            if (storeVmz[i].Equals(targetVmz[j]))
                            {
                                storeVmz.RemoveAt(i);
                                targetVmz.RemoveAt(j);
                                break;
                            }
                        }
                    }

                    int thisSwapCount = storeVmz.Count + targetVmz.Count;
                    if (thisSwapCount < swapCount)
                    {
                        // found new nearest swap
                        optimalIdentifier = target;
                        swapCount = thisSwapCount;

                        if (swapCount == 0)
                        {
                            // perfect match
                            break;
                        }
                    }
                }

                copyTargetz.Remove(optimalIdentifier);

                matches.Add(new SwapPossibility() { Store = store, Target = optimalIdentifier });

                totalCount += swapCount;
            }

            return GenerateSwaps(matches);
        }

        private List<Swap> GenerateSwaps(List<SwapPossibility> matches)
        {
            List<Swap> swapz = new List<Swap>();
            foreach (var item in matches)
            {
                swapz.AddRange(item.CalculateSwapz());
            }
            return swapz;
        }

        private class SwapPossibility
        {
            public Store Store { get; set; }
            public TargetIdentifier Target { get; set; }

            internal List<Swap> CalculateSwapz()
            {
                List<Vm> storeVmz = new List<Vm>(Store.Vmz);
                List<Vm> targetVmz = new List<Vm>(Target.Definition);

                for (int i = storeVmz.Count - 1; i >= 0; i--)
                {
                    for (int j = targetVmz.Count - 1; j >= 0; j--)
                    {
                        if (storeVmz[i].Equals(targetVmz[j]))
                        {
                            storeVmz.RemoveAt(i);
                            targetVmz.RemoveAt(j);
                            break;
                        }
                    }
                }

                List<Swap> swapz = new List<Swap>();

                foreach (var item in targetVmz)
                {
                    swapz.Add(new Swap() { Target = Store, Vm = item });
                }

                return swapz;
            }
        }
    }
}
