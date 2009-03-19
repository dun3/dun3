using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.ObjectModel;

namespace Blog.FutureOfLinq
{
    public class WhereSimplifier : ExpressionVisitor
    {
        public new Expression Visit(System.Linq.Expressions.Expression exp)
        {
            return base.Visit(exp);
        }


        protected override Expression VisitBinary(BinaryExpression b)
        {
            return base.VisitBinary(b);
        }
        protected override MemberBinding VisitBinding(MemberBinding binding)
        {
            return base.VisitBinding(binding);
        }
        protected override IEnumerable<MemberBinding> VisitBindingList(ReadOnlyCollection<MemberBinding> original)
        {
            return base.VisitBindingList(original);
        }
        protected override Expression VisitConditional(ConditionalExpression c)
        {
            return base.VisitConditional(c);
        }
        protected override Expression VisitConstant(ConstantExpression c)
        {
            return base.VisitConstant(c);
        }
        protected override ElementInit VisitElementInitializer(ElementInit initializer)
        {
            return base.VisitElementInitializer(initializer);
        }
        protected override IEnumerable<ElementInit> VisitElementInitializerList(ReadOnlyCollection<ElementInit> original)
        {
            return base.VisitElementInitializerList(original);
        }
        protected override ReadOnlyCollection<Expression> VisitExpressionList(ReadOnlyCollection<Expression> original)
        {
            return base.VisitExpressionList(original);
        }
        protected override Expression VisitInvocation(InvocationExpression iv)
        {
            return base.VisitInvocation(iv);
        }
        protected override Expression VisitLambda(LambdaExpression lambda)
        {
            return base.VisitLambda(lambda);
        }
        protected override Expression VisitListInit(ListInitExpression init)
        {
            return base.VisitListInit(init);
        }
        protected override Expression VisitMemberAccess(MemberExpression m)
        {
            return base.VisitMemberAccess(m);
        }
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
        {
            return base.VisitMemberAssignment(assignment);
        }
        protected override Expression VisitMemberInit(MemberInitExpression init)
        {
            return base.VisitMemberInit(init);
        }
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding binding)
        {
            return base.VisitMemberListBinding(binding);
        }
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
        {
            return base.VisitMemberMemberBinding(binding);
        }
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            var visitedMethodExpression = (MethodCallExpression)base.VisitMethodCall(m);
            if (IsReducableWhere(visitedMethodExpression))
            {
                MethodCallExpression innerCall;

                TryGetInnerCall(visitedMethodExpression, out innerCall);

                LambdaExpression outerLambda;
                TryGetLambda(visitedMethodExpression, out outerLambda);
                LambdaExpression innerLambda;
                TryGetLambda(innerCall, out innerLambda);

                if (outerLambda.Parameters[0].Name != innerLambda.Parameters[0].Name)
                {
                    throw new NotImplementedException("Name sanatizing not implemented - we assume that you use the same parameter name");
                }

                ReplaceParameterVisitor rpv = new ReplaceParameterVisitor(innerLambda.Parameters[0], outerLambda.Parameters[0]);

                BinaryExpression and = Expression.AndAlso(outerLambda.Body, rpv.Visit(innerLambda.Body));

                LambdaExpression newLambda = Expression.Lambda(and, outerLambda.Parameters[0]);

                UnaryExpression quote = Expression.Quote(newLambda);

                visitedMethodExpression = Expression.Call(m.Method, innerCall.Arguments[0], quote);
            }

            return visitedMethodExpression;
        }

        private bool IsReducableWhere(MethodCallExpression m)
        {
            if (!(IsQueryableWhere(m) && m.Arguments.Count == 2))
            {
                return false;
            }

            MethodCallExpression call;

            if (!TryGetInnerCall(m, out call))
            {
                return false;
            }

            if (!IsQueryableWhere(call))
            {
                return false;
            }

            LambdaExpression lambda;
            return TryGetLambda(m, out lambda) && TryGetLambda(call, out lambda);
        }

        private static bool TryGetInnerCall(MethodCallExpression m, out MethodCallExpression call)
        {
            if (m.Arguments[1].NodeType == ExpressionType.Call && m.Arguments[0].NodeType == ExpressionType.Quote)
            {
                call = (MethodCallExpression)m.Arguments[1];
                return true;
            }
            else if (m.Arguments[0].NodeType == ExpressionType.Call && m.Arguments[1].NodeType == ExpressionType.Quote)
            {
                call = (MethodCallExpression)m.Arguments[0];
                return true;
            }
            else
            {
                call = null;
                return false;
            }
        }

        private static bool TryGetLambda(MethodCallExpression m, out LambdaExpression lambda)
        {
            UnaryExpression quote;
            if (m.Arguments[0].NodeType == ExpressionType.Quote)
            {
                quote = (UnaryExpression)m.Arguments[0];
            }
            else if (m.Arguments[1].NodeType == ExpressionType.Quote)
            {
                quote = (UnaryExpression)m.Arguments[1];
            }
            else
            {
                lambda = null;
                return false;
            }

            lambda = quote.Operand as LambdaExpression;

            return lambda != null;
        }

        private static bool IsQueryableWhere(MethodCallExpression m)
        {
            return m.Method.DeclaringType == typeof(System.Linq.Queryable) && m.Method.Name == "Where";
        }

        protected override NewExpression VisitNew(NewExpression nex)
        {
            return base.VisitNew(nex);
        }
        protected override Expression VisitNewArray(NewArrayExpression na)
        {
            return base.VisitNewArray(na);
        }
        protected override Expression VisitParameter(ParameterExpression p)
        {
            return base.VisitParameter(p);
        }
        protected override Expression VisitTypeIs(TypeBinaryExpression b)
        {
            return base.VisitTypeIs(b);
        }
        protected override Expression VisitUnary(UnaryExpression u)
        {
            return base.VisitUnary(u);
        }
    }
}
