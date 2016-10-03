using HelloGoddess.Infrastructure.Collections.Extensions;
using HelloGoddess.Infrastructure.Localization;
using HelloGoddess.Infrastructure.MultiTenancy;
using HelloGoddess.Infrastructure.Application.Features;

namespace HelloGoddess.Infrastructure.Authorization
{
    internal abstract class PermissionDefinitionContextBase : IPermissionDefinitionContext
    {
        protected readonly PermissionDictionary Permissions;

        protected PermissionDefinitionContextBase()
        {
            Permissions = new PermissionDictionary();
        }

        public Permission CreatePermission(
            string name, 
            ILocalizableString displayName = null, 
            bool isGrantedByDefault = false, 
            ILocalizableString description = null, 
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant,
            IFeatureDependency featureDependency = null)
        {
            if (Permissions.ContainsKey(name))
            {
                throw new AbpException("There is already a permission with name: " + name);
            }

            var permission = new Permission(name, displayName, isGrantedByDefault, description, multiTenancySides, featureDependency);
            Permissions[permission.Name] = permission;
            return permission;
        }

        public Permission GetPermissionOrNull(string name)
        {
            return Permissions.GetOrDefault(name);
        }
    }
}
