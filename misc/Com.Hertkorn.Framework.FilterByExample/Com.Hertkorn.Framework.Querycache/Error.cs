using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.Framework.Querycache
{
    class Error
    {
        internal static Exception ArgumentNull(string p)
        {
            throw new NotImplementedException();
        }

        internal static Exception ArgumentNotValid(string p)
        {
            throw new NotImplementedException();
        }

        internal static Exception UnhandledExpressionType(System.Linq.Expressions.ExpressionType expressionType)
        {
            throw new NotImplementedException();
        }

        internal static Exception UnhandledBindingType(System.Linq.Expressions.MemberBindingType memberBindingType)
        {
            throw new NotImplementedException();
        }
    }
}
