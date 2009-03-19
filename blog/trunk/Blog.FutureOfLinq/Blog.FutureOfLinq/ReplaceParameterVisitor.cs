using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Blog.FutureOfLinq
{
    public class ReplaceParameterVisitor : ExpressionVisitor
    {
        ParameterExpression m_toReplace;
        ParameterExpression m_newParameter;

        public ReplaceParameterVisitor(ParameterExpression toReplace, ParameterExpression newParameter)
        {
            m_toReplace = toReplace;
            m_newParameter = newParameter;
        }

        public new Expression Visit(Expression exp)
        {
            return base.Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (p == m_toReplace)
            {
                return m_newParameter;
            }
            return base.VisitParameter(p);
        }
    }
}
