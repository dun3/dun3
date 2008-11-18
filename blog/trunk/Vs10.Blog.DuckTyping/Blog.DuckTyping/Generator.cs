// Copyright: Tobias Hertkorn
// Blog: http://saftsack.fs.uni-bayreuth.de/~dun3/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.DuckTyping
{
    class Generator
    {
        public static Castle.DynamicProxy.ProxyGenerator g = new Castle.DynamicProxy.ProxyGenerator();

        public static object GenerateProxy(Type type, Duck element)
        {
            return    g.CreateInterfaceProxyWithoutTarget(type, new DuckInterceptor(element));
        }
    }
}
