using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.SimpleWorkflow.Workflow
{
    public abstract class WaitStep : IEquatable<WaitStep>
    {
        public WaitStep(Guid waitGuid)
        {
            if (waitGuid == Guid.Empty) { throw new ArgumentException("can not equal Guid.Empty", "waitGuid"); }

            m_waitGuid = waitGuid;
        }

        private readonly Guid m_waitGuid;

        public override bool Equals(object obj)
        {
            return Equals(obj as WaitStep);
        }

        public override int GetHashCode()
        {
            return m_waitGuid.GetHashCode();
        }

        public bool Equals(WaitStep other)
        {
            if (other == null || GetType() != other.GetType())
            {
                return false;
            }

            return m_waitGuid == other.m_waitGuid;
        }
    }
}
