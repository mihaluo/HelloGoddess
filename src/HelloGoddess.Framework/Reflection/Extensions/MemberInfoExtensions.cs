using System;
using System.Linq;
using System.Reflection;

namespace HelloGoddess.Infrastructure.Reflection.Extensions
{
    /// <summary>
    /// Extensions to <see cref="MemberInfo"/>.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Gets a single attribute for a member.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute</typeparam>
        /// <param name="memberInfo">The member that will be checked for the attribute</param>
        /// <param name="inherit">Include inherited attributes</param>
        /// <returns>Returns the attribute object if found. Returns null if not found.</returns>
        public static TAttribute GetSingleAttributeOrNull<TAttribute>(this MemberInfo memberInfo, bool inherit = true)
            where TAttribute : Attribute
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }

            var attrs = memberInfo.GetCustomAttributes(typeof(TAttribute), inherit);
            if (attrs.Any())
            {
                return (TAttribute)attrs.FirstOrDefault();
            }

            return default(TAttribute);
        }

        public static TAttribute GetSingleAttributeOrNull<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            return type.GetTypeInfo().GetSingleAttributeOrNull<TAttribute>();
        }

        public static TAttribute GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute>(this Type type, bool inherit = true)
            where TAttribute : Attribute
        {
            TypeInfo typeInfo = type.GetTypeInfo();
            var attr = typeInfo.GetSingleAttributeOrNull<TAttribute>();
            if (attr != null)
            {
                return attr;
            }

            if (typeInfo.BaseType == null)
            {
                return null;
            }

            return typeInfo.BaseType.GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute>(inherit);
        }
    }
}
