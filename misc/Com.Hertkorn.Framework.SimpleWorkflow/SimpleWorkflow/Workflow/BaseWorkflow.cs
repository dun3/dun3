using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Com.Hertkorn.Framework.SimpleWorkflow.Fluent;

namespace Com.Hertkorn.Framework.SimpleWorkflow.Workflow
{
    public abstract class BaseWorkflow
    {
        public BaseWorkflow(Guid id, Dictionary<string, object> parameterz)
        {
            m_id = id;
            m_parameterz = new Dictionary<string, object>(parameterz);
        }

        private readonly Guid m_id;
        public Guid Id
        {
            get { return m_id; }
        }

        private readonly Dictionary<string, object> m_parameterz;
    }
}
