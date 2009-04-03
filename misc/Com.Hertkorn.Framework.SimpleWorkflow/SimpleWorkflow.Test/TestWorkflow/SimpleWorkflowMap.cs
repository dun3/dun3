using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SimpleWorkflow.Fluent;
using Com.Hertkorn.Framework.SimpleWorkflow.TestService;

namespace Com.Hertkorn.Framework.SimpleWorkflow.TestWorkflow
{
    public class SimpleWorkflowMap : WorkflowMap<SimpleWorkflow>
    {
        public SimpleWorkflowMap()
        {
            Step<TestSystem, string, string, string, Transient>(x => x.CreateTransient)
                .BindResult(x => x.transient)
                .BindParameter(x => x.country)
                .BindParameter(x => x.location)
                .BindParameter(x => x.organization);

            Step<TestSystem, Transient>(x => x.DoSomethingBefore)
                .BindParameter(x => x.transient);
            Step<TestSystem, Transient>(x => x.DoSomethingMiddle)
                .BindParameter(x => x.transient);
            Step<TestSystem, Transient>(x => x.DoSomethingAfter)
                .BindParameter(x => x.transient);
        }
    }
}
