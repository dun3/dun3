using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionTreeTest
{
    public class GetAccessor : IGetAccessor, IGetAccessor<object, object>
    {
        public GetAccessor(Type type, string parameterName)
        {
            Type = type;
            ParameterName = parameterName;


            ParameterExpression arg = Expression.Parameter(type, "test");
            MemberExpression expr = Expression.Property(arg, parameterName);

            var testLambda = Expression.Lambda(expr, new ParameterExpression[] { arg });

            Delegate = testLambda.Compile();
        }

        public object Get(object arg)
        {
            return Delegate.DynamicInvoke(arg);
        }

        Delegate Delegate { get; set; }

        Type Type { get; set; }
        string ParameterName { get; set; }
    }

    public class GetAccessor<TYPE> : IGetAccessor, IGetAccessor<TYPE, object>
    {
        public GetAccessor(string parameterName)
        {
            Type = typeof(TYPE);
            ParameterName = parameterName;

            ParameterExpression arg = Expression.Parameter(Type, "test");
            MemberExpression expr = Expression.Property(arg, parameterName);

            var testLambda = Expression.Lambda<Func<TYPE, object>>(expr, new ParameterExpression[] { arg });

            Delegate = testLambda.Compile();
        }

        public object Get(TYPE arg)
        {
            return Delegate(arg);
        }

        Func<TYPE, object> Delegate { get; set; }

        Type Type { get; set; }
        string ParameterName { get; set; }

        object IGetAccessor.Get(object arg)
        {
            if (!(arg is TYPE)) { throw new ArgumentException("Expected type " + typeof(TYPE).AssemblyQualifiedName + " but got " + arg.GetType().AssemblyQualifiedName, "arg"); }
            return Get((TYPE)arg);
        }
    }

    public class GetAccessor<TYPE, RETURNTYPE> : IGetAccessor, IGetAccessor<TYPE, RETURNTYPE>
    {
        public GetAccessor(string parameterName)
        {
            Type = typeof(TYPE);
            ParameterName = parameterName;

            ParameterExpression arg = Expression.Parameter(Type, "test");
            MemberExpression expr = Expression.Property(arg, parameterName);

            var testLambda = Expression.Lambda<Func<TYPE, RETURNTYPE>>(expr, new ParameterExpression[] { arg });

            Delegate = testLambda.Compile();
        }

        public RETURNTYPE Get(TYPE arg)
        {
            return Delegate(arg);
        }

        object IGetAccessor.Get(object arg)
        {
            if (!(arg is TYPE)) { throw new ArgumentException("Expected type " + typeof(TYPE).AssemblyQualifiedName + " but got " + arg.GetType().AssemblyQualifiedName, "arg"); }
            return Get((TYPE)arg);
        }

        Func<TYPE, RETURNTYPE> Delegate { get; set; }

        Type Type { get; set; }
        string ParameterName { get; set; }
    }
}
