using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HelloGoddess.Common.Util
{
    public static class ObjectCreator
    {
        private static readonly Dictionary<Type, Type[]> ImplTypesDic = new Dictionary<Type, Type[]>();

        public static IEnumerable<T> Create<T>()
        {
            Type type = typeof(T);
            Type[] implTypes = GeImplTypes(type);

            return implTypes.Select(innerType => (T)Activator.CreateInstance(innerType));
        }

        private static Type[] GeImplTypes(Type type)
        {
            if (ImplTypesDic.ContainsKey(type))
            {
                return ImplTypesDic[type];
            }
            var types = type.GetTypeInfo().IsInterface ? TypeHelper.GetTypesByInterfaceType(type) : TypeHelper.GetTypesByBaseType(type);

            return types.Where(a => !a.GetTypeInfo().GetCustomAttributes(typeof(DisableAttribute), false).Any()).ToArray();
        }
    }
}