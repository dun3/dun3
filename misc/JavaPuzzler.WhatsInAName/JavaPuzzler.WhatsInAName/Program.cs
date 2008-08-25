using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaPuzzler.WhatsInAName
{
    public class Name
    {
        public string First { get; set; }
        public string Last { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            Name name = obj as Name;

            return (name.Last == Last) && (name.First == First);
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Name, string> dict = new Dictionary<Name, string>(); 
            dict.Add(new Name() { First = "first", Last = "last" }, null);
            Console.WriteLine(dict.ContainsKey(new Name() { First = "first", Last = "last" }));

        }
    }
}
