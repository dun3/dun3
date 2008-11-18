// Copyright: Tobias Hertkorn
// Blog: http://saftsack.fs.uni-bayreuth.de/~dun3/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Interceptor;

namespace Blog.DuckTyping
{
    public class DuckInterceptor : IInterceptor
    {
        private Duck dynamicElement;

        public DuckInterceptor(Duck dynamicElement)
        {
            // TODO: Complete member initialization
            this.dynamicElement = dynamicElement;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name.StartsWith("get_"))
            {
                invocation.ReturnValue = dynamicElement.GetMember(invocation.Method.Name.Substring(4));
            }
            else if (invocation.Method.Name.StartsWith("set_"))
            {
                dynamicElement.SetMember(invocation.Method.Name.Substring(4), invocation.Arguments[0]);
            }
            else
            {
                throw new MissingMethodException();
            }
        }

    }
}
