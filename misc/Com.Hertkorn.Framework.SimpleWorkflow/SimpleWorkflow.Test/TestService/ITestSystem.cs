using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.SimpleWorkflow.TestService
{
    public interface ITestSystem
    {
        bool IsHappy(Transient transient);
        Transient CreateTransient(string country, string location, string organization);
        void DoSomethingBefore(Transient transient);
        void DoSomethingMiddle(Transient transient);
        void DoSomethingAfter(Transient transient);
    }
}
