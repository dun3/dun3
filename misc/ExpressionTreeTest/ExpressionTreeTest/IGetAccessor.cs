using System;
namespace ExpressionTreeTest
{
    interface IGetAccessor
    {
        object Get(object arg);
    }

    interface IGetAccessor<TYPE, RETURNTYPE>
    {
        RETURNTYPE Get(TYPE arg);
    }
}
