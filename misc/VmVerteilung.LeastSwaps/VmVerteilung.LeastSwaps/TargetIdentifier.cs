using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VmVerteilung.LeastSwaps
{
    public class TargetIdentifier
    {
        public TargetIdentifier(string id)
        {
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

        private List<Vm> m_Definition = new List<Vm>();
        public List<Vm> Definition
        {
            get
            {
                return m_Definition;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            TargetIdentifier other = obj as TargetIdentifier;
            return Id == other.Id;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
