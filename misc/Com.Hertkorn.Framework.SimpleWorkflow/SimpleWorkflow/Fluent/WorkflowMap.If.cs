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
        public IfPart If(Expression<Func<TWorkflow, bool>> statement, Action<WorkflowMap<TWorkflow>.StatementBodyPart> body)
        {
            throw new NotImplementedException();
        }

        public class IfPart
        {
            public IfPart ElseIf(Expression<Func<TWorkflow, bool>> statement, Action<WorkflowMap<TWorkflow>.StatementBodyPart> body)
            {
                throw new NotImplementedException();
            }

            public void Else(Action<WorkflowMap<TWorkflow>.StatementBodyPart> body)
            {
                throw new NotImplementedException();
            }
        }
    }
}
