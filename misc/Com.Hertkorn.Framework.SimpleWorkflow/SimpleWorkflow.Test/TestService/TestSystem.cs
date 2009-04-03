using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.SimpleWorkflow.TestService
{
    public class TestSystem : ITestSystem
    {
        public Transient CreateTransient(string country, string location, string organization)
        {
            return new Transient { Country = country, Location = location, Organization = organization };
        }

        public bool IsHappy(Transient transient)
        {
            return true;
        }

        public void DoSomethingBefore(Transient transient)
        {
            // nothing
        }

        public void DoSomethingMiddle(Transient transient)
        {
            // nothing
        }

        public void DoSomethingAfter(Transient transient)
        {
            // nothing
        }
    }
}
