// Copyright: Tobias Hertkorn
// Blog: http://saftsack.fs.uni-bayreuth.de/~dun3/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Indexed
{
    public class IndexedGetProperty<Index, Return>
    {
        private Func<Index, Return> m_getter;

        public IndexedGetProperty(Func<Index, Return> getter)
        {
            if (getter == null) { throw new ArgumentNullException("getter", "getter is null."); }

            m_getter = getter;
        }

        public Return this[Index index]
        {
            get { return m_getter(index); }
        }
    }
}
