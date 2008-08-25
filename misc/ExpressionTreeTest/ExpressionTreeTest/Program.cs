using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionTreeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<TestClass, int>> exprTree = a => a.IntProp;

            ParameterExpression param = (ParameterExpression)exprTree.Parameters[0];
            MemberExpression operation = (MemberExpression)exprTree.Body;
            var aas = operation.Expression;
            Console.WriteLine("Decomposed expression: {0} => {1} ",
                              param.Name, operation.NodeType);


            TestClass test = new TestClass() { IntProp = 2 };

            ParameterExpression arg = Expression.Parameter(typeof(TestClass), "test");
            MemberExpression expr = Expression.Property(arg, "IntProp");
            Expression<Func<TestClass, int>> myLambda = Expression.Lambda<Func<TestClass, int>>(expr, new ParameterExpression[] { arg });

            var testLambda = Expression.Lambda(expr, new ParameterExpression[] { arg });

            Console.WriteLine(myLambda.Compile()(test));
            Console.WriteLine(testLambda.Compile().DynamicInvoke(test));

            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression five = Expression.Constant(5, typeof(int));
            BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
            Expression<Func<int, bool>> lambda1 =
                Expression.Lambda<Func<int, bool>>(
                    numLessThanFive,
                    new ParameterExpression[] { numParam });

            Console.WriteLine(lambda1.Compile()(19));

            //Expression.Bind

            Console.Read();

        }

        public class TestClass
        {
            public int IntProp { get; set; }
        }
    }
}
