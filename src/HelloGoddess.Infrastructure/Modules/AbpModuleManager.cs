using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using HelloGoddess.Infrastructure.Collections.Extensions;
using HelloGoddess.Infrastructure.Configuration.Startup;
using HelloGoddess.Infrastructure.Dependency;
using HelloGoddess.Infrastructure.PlugIns;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions.Internal;

namespace HelloGoddess.Infrastructure.Modules
{
    /// <summary>
    /// This class is used to manage modules.
    /// </summary>
    public class AbpModuleManager : IAbpModuleManager
    {
        public AbpModuleInfo StartupModule { get; private set; }

        private Type _startupModuleType;

        public IReadOnlyList<AbpModuleInfo> Modules => _modules.ToImmutableList();

        public ILogger Logger { get; set; }

        private readonly IIocManager _iocManager;
        private readonly IAbpPlugInManager _abpPlugInManager;
        private readonly AbpModuleCollection _modules;

        public AbpModuleManager(IIocManager iocManager, IAbpPlugInManager abpPlugInManager)
        {
            _modules = new AbpModuleCollection();
            _iocManager = iocManager;
            _abpPlugInManager = abpPlugInManager;
            Logger = NullLogger.Instance;
        }

        public virtual void Initialize(Type startupModule)
        {
            _startupModuleType = startupModule;
            LoadAllModules();
        }

        public virtual void StartModules()
        {
            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownModules()
        {
            Logger.LogDebug("Shutting down has been started");

            var sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());

            Logger.LogDebug("Shutting down completed.");
        }

        private void LoadAllModules()
        {
            Logger.LogDebug("Loading Abp modules...");

            var moduleTypes = FindAllModules().Distinct().ToList();

            Logger.LogDebug("Found " + moduleTypes.Count + " ABP modules in total.");

            RegisterModules(moduleTypes);
            CreateModules(moduleTypes);

            AbpModuleCollection.EnsureKernelModuleToBeFirst(_modules);

            SetDependencies();

            Logger.LogDebug("{0} modules loaded.", _modules.Count);
        }

        private List<Type> FindAllModules()
        {
            var modules = AbpModule.FindDependedModuleTypesRecursivelyIncludingGivenModule(_startupModuleType);

            _abpPlugInManager
                .PlugInSources
                .GetAllModules()
                .ForEach(m => modules.AddIfNotContains(m));

            return modules;
        }

        private void CreateModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = _iocManager.Resolve(moduleType) as AbpModule;
                if (moduleObject == null)
                {
                    throw new AbpInitializationException("This type is not an ABP module: " + moduleType.AssemblyQualifiedName);
                }

                moduleObject.IocManager = _iocManager;
                moduleObject.Configuration = _iocManager.Resolve<IAbpStartupConfiguration>();

                var moduleInfo = new AbpModuleInfo(moduleType, moduleObject);

                _modules.Add(moduleInfo);

                if (moduleType == _startupModuleType)
                {
                    StartupModule = moduleInfo;
                }

                Logger.LogDebug("Loaded module: " + moduleType.AssemblyQualifiedName);
            }
        }

        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                _iocManager.RegisterIfNot(moduleType);
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                moduleInfo.Dependencies.Clear();

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in AbpModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new AbpInitializationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }
    }
}
