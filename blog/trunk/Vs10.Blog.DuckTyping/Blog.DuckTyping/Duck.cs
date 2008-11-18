// Copyright: Tobias Hertkorn
// Blog: http://saftsack.fs.uni-bayreuth.de/~dun3/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Scripting.Actions;

namespace Blog.DuckTyping
{
    public class Duck : IDynamicObject
    {
        string Name { get; set; }
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public virtual object GetMember(System.Scripting.Actions.GetMemberAction action)
        {
            return GetMember(action.Name);
        }

        public virtual object GetMember(string name)
        {
            try
            {
                return dictionary[name];
            }
            catch (Exception)
            {
                throw new MemberAccessException();
            }
        }

        public virtual void SetMember(System.Scripting.Actions.SetMemberAction action, object value)
        {
            SetMember(action.Name, value);
        }

        public virtual void SetMember(string name, object value)
        {
            dictionary[name] = value.ToString();
        }

        public virtual object Convert(ConvertAction action)
        {
            return Generator.GenerateProxy(action.ToType, this);
        }

        public virtual MetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
        {
            return new MetaDuck(parameter, this);
        }
    }
}
