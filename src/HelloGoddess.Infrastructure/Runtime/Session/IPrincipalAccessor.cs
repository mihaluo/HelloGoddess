using System.Security.Claims;

namespace HelloGoddess.Infrastructure.Runtime.Session
{
    public interface IPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }
}
