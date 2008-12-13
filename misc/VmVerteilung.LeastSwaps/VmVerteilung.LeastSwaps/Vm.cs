using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VmVerteilung.LeastSwaps
{
    public class Vm
    {
        public Vm(string id)
        {
            if (id == null) { throw new ArgumentNullException("id"); }
            m_Id = id;
        }

        private readonly string m_Id;
        public string Id
        {
            get
            {
                return m_Id;
            }
        }

        public Store Store { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Vm other = obj as Vm;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
