using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SimpleWorkflow.Fluent;
using Com.Hertkorn.Framework.SimpleWorkflow.TestService;

namespace Com.Hertkorn.Framework.SimpleWorkflow.TestWorkflow
{
    public class IfWorkflowMap : WorkflowMap<IfWorkflow>
    {
        public IfWorkflowMap()
        {
            Step<TestSystem, string, string, string, Transient>(x => x.CreateTransient)
                .BindResult(x => x.transient)
                .BindParameter(x => x.country)
                .BindParameter(x => x.location)
                .BindParameter(x => x.organization);

            Step<TestSystem, Transient, bool>(x => x.IsHappy)
                .BindResult(x => x.isHappy)
                .BindParameter(x => x.transient);

            If(x => x.isHappy, t =>
                {
                    t.Step<TestSystem, Transient>(x => x.DoSomethingMiddle);
                    t.Step<TestSystem, Transient>(x => x.DoSomethingAfter);
                })
                .ElseIf(x => x.country == null, t =>
                    {
                        t.Step<TestSystem, Transient>(x => x.DoSomethingBefore);
                    })
                    .Else(t =>
                    {
                        t.Step<TestSystem, Transient>(x => x.DoSomethingBefore);
                        t.Step<TestSystem, Transient>(x => x.DoSomethingMiddle);
                    });
        }
    }
}
