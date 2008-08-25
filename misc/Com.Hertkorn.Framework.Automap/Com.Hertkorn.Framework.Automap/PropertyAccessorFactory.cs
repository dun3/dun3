using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastDynamicPropertyAccessor;

namespace Com.Hertkorn.Framework.Automap
{
    public static class PropertyAccessorFactory
    {
        public static Dictionary<string, PropertyAccessor> GetAccessorz(Type type, IEnumerable<string> propertyNamez)
        {
            Dictionary<string, PropertyAccessor> knownPropertyAccessorz;
            if (!m_propertyAccessorCache.TryGetValue(type, out knownPropertyAccessorz))
            {
                knownPropertyAccessorz = new Dictionary<string, PropertyAccessor>();
                m_propertyAccessorCache[type] = knownPropertyAccessorz;
            }

            foreach (var propertyName in propertyNamez)
            {
                if (!knownPropertyAccessorz.ContainsKey(propertyName))
                {
                    knownPropertyAccessorz[propertyName] = CreateAccessor(type, propertyName);
                }
            }

            return knownPropertyAccessorz;
        }

        private static Dictionary<Type, Dictionary<string, PropertyAccessor>> m_propertyAccessorCache = new Dictionary<Type, Dictionary<string, PropertyAccessor>>();

        private static PropertyAccessor CreateAccessor(Type sourceType, string propertyName)
        {
            return new PropertyAccessor(sourceType, propertyName);
        }


    }
}
