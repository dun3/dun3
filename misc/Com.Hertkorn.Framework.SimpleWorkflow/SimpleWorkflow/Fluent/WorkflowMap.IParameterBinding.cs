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
        public interface IParameterBinding<T1>
        {
            void BindParameter(Expression<Func<TWorkflow, T1>> member);
        }

        public interface IParameterBinding<T1, T2>
        {
            IParameterBinding<T1> BindParameter(Expression<Func<TWorkflow, T2>> member);
        }

        public interface IParameterBinding<T1, T2, T3>
        {
            IParameterBinding<T1, T2> BindParameter(Expression<Func<TWorkflow, T3>> member);
        }

        public interface IParameterBinding<T1, T2, T3, T4>
        {
            IParameterBinding<T1, T2, T3> BindParameter(Expression<Func<TWorkflow, T4>> member);
        }
    }
}
