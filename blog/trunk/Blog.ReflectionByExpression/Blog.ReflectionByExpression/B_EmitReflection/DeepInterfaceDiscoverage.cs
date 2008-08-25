using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Com.Hertkorn.Framework.Automap.Reflection
{
    public class DeepInterfaceDiscoverage
    {
        public static PropertyInfo[] Find(Type typeToReflect)
        {
            if (typeToReflect.IsInterface)
            {
                List<PropertyInfo> infoz = new List<PropertyInfo>();

                GetPropertyInfoz(infoz, typeToReflect);

                return infoz.ToArray();
            }
            else
            {
                return typeToReflect.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
        }

        private static void GetPropertyInfoz(List<PropertyInfo> infoz, Type typeToReflect)
        {
            foreach (var item in typeToReflect.GetInterfaces())
            {
                GetPropertyInfoz(infoz, item);
            }
            infoz.AddRange(typeToReflect.GetProperties(BindingFlags.Public | BindingFlags.Instance));
        }

        public static PropertyDescriptorCollection GetPropertiesDeep(Type typeToReflect, bool includeExplicity)
        {
            ArrayList properties = new ArrayList();
            Action<Type> reflector = default(Action<Type>);
            (reflector = delegate(Type type)
            {
                Predicate<string> alreadyAdded = new Predicate<string>(delegate(string propName)
                {
                    for (int i = 0; i < properties.Count; i++)
                        if (((PropertyDescriptor)properties[i]).Name == propName)
                            return true;
                    return false;
                });

                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(type))
                    if (!alreadyAdded(prop.Name) && (!type.IsInterface && !alreadyAdded(type.FullName.Replace('+', '.') + "." + prop.Name)))
                        properties.Add(prop);

                foreach (Type interfaceType in type.GetInterfaces())
                {
                    foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(interfaceType))
                    {
                        if (!typeToReflect.IsInterface && includeExplicity)
                        {
                            string explTypeName = interfaceType.FullName.Replace('+', '.');
                            if (
                               type.GetMember(explTypeName + ".get_" + prop.Name, BindingFlags.NonPublic | BindingFlags.Instance).Length > 0 ||
                               type.GetMember(explTypeName + ".set_" + prop.Name, BindingFlags.NonPublic | BindingFlags.Instance).Length > 0
                               )
                            {
                                ArrayList attributes = new ArrayList();
                                attributes.AddRange(interfaceType.GetProperty(prop.Name).GetCustomAttributes(false));
                                PropertyDescriptor explProperty = TypeDescriptor.CreateProperty(type, explTypeName + "." + prop.Name, prop.PropertyType, (Attribute[])attributes.ToArray(typeof(Attribute)));
                                properties.Add(explProperty);
                            }
                        }

                        if (!alreadyAdded(prop.Name) && (typeToReflect.GetProperty(prop.Name) != null))
                            properties.Add(prop);

                    }
                    reflector(interfaceType);
                }
            })(typeToReflect);
            return new PropertyDescriptorCollection((PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor)), false);
        }


        internal static PropertyInfo Find(Type targetType, string property)
        {
            var all = Find(targetType);
            return (from s in all
                    where s.Name == property
                    select s).FirstOrDefault();
        }

        internal static MethodInfo GetMethod(Type typeToReflect, string methodName)
        {

            if (typeToReflect.IsInterface)
            {
                return GetMethodInfoz(typeToReflect, methodName);
            }
            else
            {
                return typeToReflect.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            }
        }

        private static MethodInfo GetMethodInfoz(Type typeToReflect, string methodName)
        {
            foreach (var item in typeToReflect.GetInterfaces())
            {
                MethodInfo info = GetMethodInfoz(item, methodName);
                if (info != null)
                {
                    return info;
                }
            }

            return typeToReflect.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        }
    }

}
