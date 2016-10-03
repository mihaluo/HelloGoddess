using System.Security.Claims;
using System.Threading;
using HelloGoddess.Infrastructure.Dependency;

namespace HelloGoddess.Infrastructure.Runtime.Session
{
    public class DefaultPrincipalAccessor : IPrincipalAccessor, ISingletonDependency
    {
        public virtual ClaimsPrincipal Principal => null;//todo

        public static DefaultPrincipalAccessor Instance => new DefaultPrincipalAccessor();
    }
}