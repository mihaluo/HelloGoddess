using System;
using System.Collections.Generic;

namespace HelloGoddess.Infrastructure.Configuration.Startup
{
    public interface IValidationConfiguration
    {
        List<Type> IgnoredTypes { get; }
    }
}