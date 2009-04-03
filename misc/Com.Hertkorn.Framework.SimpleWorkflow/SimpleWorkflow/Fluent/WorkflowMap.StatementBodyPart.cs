using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SimpleWorkflow.Workflow;
using System.Linq.Expressions;

namespace Com.Hertkorn.Framework.SimpleWorkflow.Fluent
{
    public partial class WorkflowMap<TWorkflow> where TWorkflow : BaseWorkflow
    {
        public class StatementBodyPart
        {
            public virtual IParameterBinding<T1> Step<TClass, T1>(Expression<Func<TClass, Action<T1>>> method) where TClass : class, new()
            {
                throw new NotImplementedException();
            }
        }
    }
}
