using System;
using System.Collections.Generic;
using System.Linq;
using HelloGoddess.Infrastructure.Modules;

namespace HelloGoddess.Infrastructure.PlugIns
{
    public interface IPlugInSource
    {
        List<Type> GetModules();
    }

    public static class PlugInSourceExtensions
    {
        public static List<Type> GetModulesWithAllDependencies(this IPlugInSource plugInSource)
        {
            return plugInSource
                .GetModules()
                .SelectMany(AbpModule.FindDependedModuleTypesRecursivelyIncludingGivenModule)
                .Distinct()
                .ToList();
        }
    }
}