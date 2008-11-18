// Copyright: Tobias Hertkorn
// Blog: http://saftsack.fs.uni-bayreuth.de/~dun3/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Scripting.Actions;
using System.Linq.Expressions;

namespace Blog.DuckTyping
{
    public class MetaDuck : MetaObject
    {
        public MetaDuck(System.Linq.Expressions.Expression expression, Duck duck)
            : base(expression, Restrictions.Empty, duck)
        {
        }

        public override MetaObject SetMember(SetMemberAction action, MetaObject[] args)
        {            
            return new MetaObject(
                Expression.Call(
                    Expression.Convert(Expression, LimitType),
                    typeof(Duck).GetMethod("SetMember", new Type[] { typeof(SetMemberAction), typeof(object) }),
                    Expression.Convert(Expression.Constant(action), typeof(SetMemberAction)),
                    args[1].Expression
                ),
                Restrictions.TypeRestriction(Expression, LimitType)
            );
        }

        public override MetaObject GetMember(GetMemberAction action, MetaObject[] args)
        {
            return new MetaObject(
               Expression.Call(
                   Expression.Convert(Expression, LimitType),
                   typeof(Duck).GetMethod("GetMember", new Type[] { typeof(GetMemberAction) }),
                   Expression.Constant(action)
               ),
               Restrictions.TypeRestriction(Expression, LimitType)
           );
        }

        public override MetaObject Convert(ConvertAction action, MetaObject[] args)
        {
            return new MetaObject(
                Expression.Call(
                    Expression.Convert(Expression, LimitType),
                    typeof(Duck).GetMethod("Convert", new Type[] { typeof(ConvertAction) }),
                    Expression.Constant(action)
                ),
                Restrictions.TypeRestriction(Expression, LimitType)
            );
        }      
    }
}
