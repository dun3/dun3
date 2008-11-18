// Copyright: Tobias Hertkorn
// Blog: http://saftsack.fs.uni-bayreuth.de/~dun3/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.DuckTyping
{
    public interface IQuack
    {
        string Test { get; set; }
        string NotSet { get; set; }
    }
}
