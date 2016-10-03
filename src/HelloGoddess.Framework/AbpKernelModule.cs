//using System;
//using System.IO;
//using System.Linq.Expressions;
//using System.Reflection;
//using HelloGoddess.Infrastructure.Application.Services;
//using HelloGoddess.Infrastructure.Auditing;
//using HelloGoddess.Infrastructure.Authorization;
//using HelloGoddess.Infrastructure.BackgroundJobs;
//using HelloGoddess.Infrastructure.Collections.Extensions;
//using HelloGoddess.Infrastructure.Configuration;
//using HelloGoddess.Infrastructure.Configuration.Startup;
//using HelloGoddess.Infrastructure.Dependency;
//using HelloGoddess.Infrastructure.Domain.Uow;
//using HelloGoddess.Infrastructure.Events.Bus;
//using HelloGoddess.Infrastructure.Localization;
//using HelloGoddess.Infrastructure.Localization.Dictionaries;
//using HelloGoddess.Infrastructure.Localization.Dictionaries.Xml;
//using HelloGoddess.Infrastructure.Modules;
//using HelloGoddess.Infrastructure.Net.Mail;
//using HelloGoddess.Infrastructure.Notifications;
//using HelloGoddess.Infrastructure.Runtime.Caching;
//using HelloGoddess.Infrastructure.Runtime.Validation.Interception;
//using HelloGoddess.Infrastructure.Threading;
//using HelloGoddess.Infrastructure.Threading.BackgroundWorkers;
//using HelloGoddess.Infrastructure.Timing;
//using Castle.MicroKernel.Registration;
//using HelloGoddess.Infrastructure;
//using HelloGoddess.Infrastructure.Application.Features;
//using HelloGoddess.Infrastructure.Application.Navigation;
//using HelloGoddess.Infrastructure.Authorization;

//namespace Abp
//{
//    /// <summary>
//    /// Kernel (core) module of the ABP system.
//    /// No need to depend on this, it's automatically the first module always.
//    /// </summary>
//    public sealed class AbpKernelModule : AbpModule
//    {
//        public override void PreInitialize()
//        {
//            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

//            ValidationInterceptorRegistrar.Initialize(IocManager);
//            AuditingInterceptorRegistrar.Initialize(IocManager);
//            UnitOfWorkRegistrar.Initialize(IocManager);
//            AuthorizationInterceptorRegistrar.Initialize(IocManager);

//            NamedTypeSelector namedTypeSelector = new NamedTypeSelector(
//                "HelloGoddess.Infrastructure.ApplicationServices",
//                type => typeof(IApplicationService).IsAssignableFrom(type));

//            Configuration.Auditing.Selectors.Add(namedTypeSelector);

//            Configuration.Localization.Sources.Add(
//                new DictionaryBasedLocalizationSource(
//                    AbpConsts.LocalizationSourceName,
//                    new XmlEmbeddedFileLocalizationDictionaryProvider(
//                        Assembly.GetEntryAssembly(), "HelloGoddess.Infrastructure.Localization.Sources.AbpXmlSource"
//                        )));

//            Configuration.Settings.Providers.Add<LocalizationSettingProvider>();
//            Configuration.Settings.Providers.Add<EmailSettingProvider>();
//            Configuration.Settings.Providers.Add<NotificationSettingProvider>();
//            Configuration.Settings.Providers.Add<TimingSettingProvider>();

//            Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.SoftDelete, true);
//            Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.MustHaveTenant, true);
//            Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.MayHaveTenant, true);

//            ConfigureCaches();
//            AddIgnoredTypes();
//        }

//        public override void Initialize()
//        {
//            foreach (var replaceAction in ((AbpStartupConfiguration)Configuration).ServiceReplaceActions.Values)
//            {
//                replaceAction();
//            }

//            IocManager.IocContainer.Install(new EventBusInstaller(IocManager));

//            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(),
//                new ConventionalRegistrationConfig
//                {
//                    InstallInstallers = false
//                });
//        }

//        public override void PostInitialize()
//        {
//            RegisterMissingComponents();

//            IocManager.Resolve<SettingDefinitionManager>().Initialize();
//            IocManager.Resolve<FeatureManager>().Initialize();
//            IocManager.Resolve<PermissionManager>().Initialize();
//            IocManager.Resolve<LocalizationManager>().Initialize();
//            IocManager.Resolve<NotificationDefinitionManager>().Initialize();
//            IocManager.Resolve<NavigationManager>().Initialize();

//            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
//            {
//                var workerManager = IocManager.Resolve<IBackgroundWorkerManager>();
//                workerManager.Start();
//                workerManager.Add(IocManager.Resolve<IBackgroundJobManager>());
//            }
//        }

//        public override void Shutdown()
//        {
//            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
//            {
//                IocManager.Resolve<IBackgroundWorkerManager>().StopAndWaitToStop();
//            }
//        }

//        private void ConfigureCaches()
//        {
//            Configuration.Caching.Configure(AbpCacheNames.ApplicationSettings, cache =>
//            {
//                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(8);
//            });

//            Configuration.Caching.Configure(AbpCacheNames.TenantSettings, cache =>
//            {
//                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(60);
//            });

//            Configuration.Caching.Configure(AbpCacheNames.UserSettings, cache =>
//            {
//                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(20);
//            });
//        }

//        private void AddIgnoredTypes()
//        {
//            var commonIgnoredTypes = new[] { typeof(Stream), typeof(Expression) };

//            foreach (var ignoredType in commonIgnoredTypes)
//            {
//                Configuration.Auditing.IgnoredTypes.AddIfNotContains(ignoredType);
//                Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
//            }

//            var validationIgnoredTypes = new[] { typeof(Type) };
//            foreach (var ignoredType in validationIgnoredTypes)
//            {
//                Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
//            }
//        }

//        private void RegisterMissingComponents()
//        {
//            if (!IocManager.IsRegistered<IGuidGenerator>())
//            {
//                IocManager.IocContainer.Register(
//                    Component
//                        .For<IGuidGenerator, SequentialGuidGenerator>()
//                        .Instance(SequentialGuidGenerator.Instance)
//                );
//            }

//            IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);
//            IocManager.RegisterIfNot<IAuditingStore, SimpleLogAuditingStore>(DependencyLifeStyle.Singleton);
//            IocManager.RegisterIfNot<IPermissionChecker, NullPermissionChecker>(DependencyLifeStyle.Singleton);
//            IocManager.RegisterIfNot<IRealTimeNotifier, NullRealTimeNotifier>(DependencyLifeStyle.Singleton);
//            IocManager.RegisterIfNot<INotificationStore, NullNotificationStore>(DependencyLifeStyle.Singleton);
//            IocManager.RegisterIfNot<IUnitOfWorkFilterExecuter, NullUnitOfWorkFilterExecuter>(DependencyLifeStyle.Singleton);

//            if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
//            {
//                IocManager.RegisterIfNot<IBackgroundJobStore, InMemoryBackgroundJobStore>(DependencyLifeStyle.Singleton);
//            }
//            else
//            {
//                IocManager.RegisterIfNot<IBackgroundJobStore, NullBackgroundJobStore>(DependencyLifeStyle.Singleton);
//            }
//        }
//    }
//}