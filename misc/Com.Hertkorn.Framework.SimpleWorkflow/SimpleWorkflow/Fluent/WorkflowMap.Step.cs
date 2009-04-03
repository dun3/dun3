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

        //public virtual IFunctionResult Step<TClass, TResult>(Expression<Func<TClass, Func<TResult>>> method) where TClass : class, new()
        //{
        //    throw new NotImplementedException();
        //}

        public virtual IFunctionResult<T1, TResult> Step<TClass, T1, TResult>(Expression<Func<TClass, Func<T1, TResult>>> method) where TClass : class, new()
        {
            throw new NotImplementedException();
        }

        //public virtual IFunctionResult Step<TClass, T1, T2, TResult>(Expression<Func<TClass, Func<T1, T2, TResult>>> method) where TClass : class, new()
        //{
        //    throw new NotImplementedException();
        //}

        public virtual IFunctionResult<T1, T2, T3, TResult> Step<TClass, T1, T2, T3, TResult>(Expression<Func<TClass, Func<T1, T2, T3, TResult>>> method) where TClass : class, new()
        {
            throw new NotImplementedException();
        }

        //public virtual IFunctionResult Step<TClass, T1, T2, T3, T4, TResult>(Expression<Func<TClass, Func<T1, T2, T3, T4, TResult>>> method) where TClass : class, new()
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual IActionResult Step<TClass>(Expression<Func<TClass, Action>> method) where TClass : class, new()
        //{
        //    throw new NotImplementedException();
        //}

        public virtual IParameterBinding<T1> Step<TClass, T1>(Expression<Func<TClass, Action<T1>>> method) where TClass : class, new()
        {
            throw new NotImplementedException();
        }

        public virtual IParameterBinding<T1, T2> Step<TClass, T1, T2>(Expression<Func<TClass, Action<T1, T2>>> method) where TClass : class, new()
        {
            throw new NotImplementedException();
        }

        public virtual IParameterBinding<T1, T2, T3> Step<TClass, T1, T2, T3>(Expression<Func<TClass, Action<T1, T2, T3>>> method) where TClass : class, new()
        {
            throw new NotImplementedException();
        }

        //public virtual IActionResult Step<TClass, T1, T2, T3, T4>(Expression<Func<TClass, Action<T1, T2, T3, T4>>> method) where TClass : class, new()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
