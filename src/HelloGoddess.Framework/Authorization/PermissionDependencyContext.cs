using HelloGoddess.Infrastructure.Dependency;
using HelloGoddess.Infrastructure.Authorization;

namespace HelloGoddess.Infrastructure.Authorization
{
    internal class PermissionDependencyContext : IPermissionDependencyContext, ITransientDependency
    {
        public UserIdentifier User { get; set; }

        public IIocResolver IocResolver { get; private set; }
        
        public IPermissionChecker PermissionChecker { get; set; }

        public PermissionDependencyContext(IIocResolver iocResolver)
        {
            IocResolver = iocResolver;
            PermissionChecker = NullPermissionChecker.Instance;
        }
    }
}