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
        //public interface IFunctionResult<TResult>
        //{
        //    void BindResult(Expression<Func<TWorkflow, TResult>> member);
        //}

        public interface IFunctionResult<T1, TResult>
        {
            IParameterBinding<T1> BindResult(Expression<Func<TWorkflow, TResult>> member);
        }

        public interface IFunctionResult<T1, T2, TResult>
        {
            IParameterBinding<T1, T2> BindResult(Expression<Func<TWorkflow, TResult>> member);
        }

        public interface IFunctionResult<T1, T2, T3, TResult>
        {
            IParameterBinding<T1, T2, T3> BindResult(Expression<Func<TWorkflow, TResult>> member);
        }

        public interface IFunctionResult<T1, T2, T3, T4, TResult>
        {
            IParameterBinding<T1, T2, T3, T4> BindResult(Expression<Func<TWorkflow, TResult>> member);
        }
    }
}
