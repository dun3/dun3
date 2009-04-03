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
        public void Wait(WaitStep waitGuid)
        {
            throw new NotImplementedException();
        }
    }
}
