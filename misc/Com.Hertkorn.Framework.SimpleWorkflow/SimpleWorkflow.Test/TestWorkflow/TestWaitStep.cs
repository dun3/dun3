using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SimpleWorkflow.Workflow;

namespace Com.Hertkorn.Framework.SimpleWorkflow.TestWorkflow
{
    public class TestWaitStep : WaitStep
    {
        public TestWaitStep()
            : base(new Guid("{6E310BDC-DBE7-497f-A836-869199019800}"))
        {
        }
    }
}
