using System.Collections.Generic;
using HelloGoddess.Infrastructure.MultiTenancy;

namespace HelloGoddess.Infrastructure.Domain.Uow
{
    public class ConnectionStringResolveArgs : Dictionary<string, object>
    {
        public MultiTenancySides? MultiTenancySide { get; set; }

        public ConnectionStringResolveArgs(MultiTenancySides? multiTenancySide = null)
        {
            MultiTenancySide = multiTenancySide;
        }
    }
}