using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VmVerteilung
{
    public class Storage
    {
        public string Id { get; set; }
        public double Capacity { get; set; }

        public List<Vm> Vmz { get; set; }

        public double Unassigned
        {
            get
            {
                double size = 0.0;
                foreach (var item in Vmz)
                {
                    size += item.Size;
                }
                return Capacity - size;
            }
        }

        public bool IsOvercommitted
        {
            get
            {
                return Unassigned < 0.0;
            }
        }

        public bool IsFull
        {
            get
            {
                return Unassigned < Capacity * 0.1;
            }
        }
    }
}
