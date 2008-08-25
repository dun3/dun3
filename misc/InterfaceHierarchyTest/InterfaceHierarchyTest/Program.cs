using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace InterfaceHierarchyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PropertyInfo[] properties 
                = typeof(IHierarchy).GetProperties(
                    BindingFlags.Instance | BindingFlags.Public
                );
            int answer = properties.Length;

            Type baset = typeof(IHierarchy).BaseType;
            bool isbase = typeof(IHierarchy).IsSubclassOf(typeof(IDeap));
            bool isbase2 = typeof(IDeap).IsSubclassOf(typeof(IHierarchy));

            var i = typeof(IHierarchy).GetInterfaces();
        }
    }

    public interface IDeap
    {
        long Id { get; }
    }
    public interface IHierarchy : IDeap
    {
        string Name { get; }
    }
}
