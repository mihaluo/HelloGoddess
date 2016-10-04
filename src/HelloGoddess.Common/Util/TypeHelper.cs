using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HelloGoddess.Common.Util
{
    public static class TypeHelper
    {
        public static Type[] GetTypesByBaseType(Type baseType)
        {
            if (baseType == null) throw new ArgumentNullException("baseType");

            IEnumerable<Type> types = baseType.GetTypeInfo().Assembly.GetTypes()
                                                       .Where(t => baseType.IsAssignableFrom(t) && t != baseType);

            return types.ToArray();
        }

        public static Type[] GetTypesByInterfaceType(Type interfacType)
        {
            if (interfacType == null) throw new ArgumentNullException("interfacType");
            Assembly assembly = interfacType.GetTypeInfo().Assembly;

            IEnumerable<Type> enumerable = assembly.GetTypes().Where(t => t.GetTypeInfo().IsClass && 
                                                                      !t.GetTypeInfo().IsAbstract );

            List<Type> list = new List<Type>();
            foreach (var type in enumerable)
            {
                Type foundInterfaceType = type.GetTypeInfo().GetInterface(interfacType.Name);
                if (foundInterfaceType != null) list.Add(type);
            }
            return list.ToArray();
        }
    }
}