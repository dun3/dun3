using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Com.Hertkorn.Framework.Automap.Reflection;

namespace Com.Hertkorn.Framework.Automap
{
    public static class PropertyInfoFactory
    {
        private static Dictionary<Type, string[]> m_propertyNamez = new Dictionary<Type, string[]>();

        public static string[] GetPropertyNamez(Type type)
        {
            string[] propertyNamez;
            if (!m_propertyNamez.TryGetValue(type, out propertyNamez))
            {

                PropertyInfo[] propertyInfoListe = CreatePropertyInfoz(type);

                propertyNamez = (from p in propertyInfoListe
                                 orderby p.Name
                                 select p.Name).ToArray();
                m_propertyNamez[type] = propertyNamez;
            }
            return propertyNamez;
        }

        public static PropertyInfo[] CreatePropertyInfoz(Type type)
        {
            return DeepInterfaceDiscoverage.Find(type);
            //return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic);
        }

    }
}
