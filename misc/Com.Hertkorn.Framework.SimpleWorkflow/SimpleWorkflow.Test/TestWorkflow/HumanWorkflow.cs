using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Hertkorn.Framework.SimpleWorkflow.Workflow;
using Com.Hertkorn.Framework.SimpleWorkflow.TestService;

namespace Com.Hertkorn.Framework.SimpleWorkflow.TestWorkflow
{
    public class HumanWorkflow : BaseWorkflow
    {
        public HumanWorkflow(Guid id, Dictionary<string, object> parameterz)
            : base(id, parameterz)
        {
        }

        public string country;
        public string location;
        public string organization;

        public Transient transient;
    }
}
