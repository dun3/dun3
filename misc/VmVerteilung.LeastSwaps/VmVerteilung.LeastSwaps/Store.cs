using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VmVerteilung.LeastSwaps
{
    public class Store
    {
        public Store(string id)
        {
            if (id == null) { throw new ArgumentNullException("id"); }
            m_Id = id;
        }

        private string m_Id;
        public string Id
        {
            get
            {
                return m_Id;
            }
        }

        private List<Vm> m_Vmz = new List<Vm>();
        public List<Vm> Vmz
        {
            get
            {
                return m_Vmz;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Store other = obj as Store;
            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
