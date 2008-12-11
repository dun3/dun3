using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VmVerteilung
{
    public class Vm
    {
        public string Id { get; set; }
        public double Size { get; set; }
        public double Io { get; set; }

        public IoType IoType { get; set; }
    }
}
