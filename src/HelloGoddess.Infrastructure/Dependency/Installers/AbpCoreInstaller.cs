﻿//using HelloGoddess.Infrastructure.Auditing;
//using HelloGoddess.Infrastructure.BackgroundJobs;
//using HelloGoddess.Infrastructure.Configuration.Startup;
//using HelloGoddess.Infrastructure.Domain.Uow;
//using HelloGoddess.Infrastructure.Localization;
//using HelloGoddess.Infrastructure.Modules;
//using HelloGoddess.Infrastructure.Notifications;
//using HelloGoddess.Infrastructure.PlugIns;
//using HelloGoddess.Infrastructure.Reflection;
//using HelloGoddess.Infrastructure.Runtime.Caching.Configuration;
//using Castle.MicroKernel.Registration;
//using Castle.MicroKernel.SubSystems.Configuration;
//using Castle.Windsor;
//using HelloGoddess.Infrastructure.Application.Features;

//namespace HelloGoddess.Infrastructure.Dependency.Installers
//{
//    internal class AbpCoreInstaller : IWindsorInstaller
//    {
//        public void Install(IWindsorContainer container, IConfigurationStore store)
//        {
//            //TODO: Register to IIocManager to not depend on Castle Windsor
//            container.Register(
//                Component.For<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>().ImplementedBy<UnitOfWorkDefaultOptions>().LifestyleSingleton(),
//                Component.For<INavigationConfiguration, NavigationConfiguration>().ImplementedBy<NavigationConfiguration>().LifestyleSingleton(),
//                Component.For<ILocalizationConfiguration, LocalizationConfiguration>().ImplementedBy<LocalizationConfiguration>().LifestyleSingleton(),
//                Component.For<IAuthorizationConfiguration, AuthorizationConfiguration>().ImplementedBy<AuthorizationConfiguration>().LifestyleSingleton(),
//                Component.For<IValidationConfiguration, ValidationConfiguration>().ImplementedBy<ValidationConfiguration>().LifestyleSingleton(),
//                Component.For<IFeatureConfiguration, FeatureConfiguration>().ImplementedBy<FeatureConfiguration>().LifestyleSingleton(),
//                Component.For<ISettingsConfiguration, SettingsConfiguration>().ImplementedBy<SettingsConfiguration>().LifestyleSingleton(),
//                Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
//                Component.For<IEventBusConfiguration, EventBusConfiguration>().ImplementedBy<EventBusConfiguration>().LifestyleSingleton(),
//                Component.For<IMultiTenancyConfig, MultiTenancyConfig>().ImplementedBy<MultiTenancyConfig>().LifestyleSingleton(),
//                Component.For<ICachingConfiguration, CachingConfiguration>().ImplementedBy<CachingConfiguration>().LifestyleSingleton(),
//                Component.For<IAuditingConfiguration, AuditingConfiguration>().ImplementedBy<AuditingConfiguration>().LifestyleSingleton(),
//                Component.For<IBackgroundJobConfiguration, BackgroundJobConfiguration>().ImplementedBy<BackgroundJobConfiguration>().LifestyleSingleton(),
//                Component.For<INotificationConfiguration, NotificationConfiguration>().ImplementedBy<NotificationConfiguration>().LifestyleSingleton(),
//                Component.For<IAbpStartupConfiguration, AbpStartupConfiguration>().ImplementedBy<AbpStartupConfiguration>().LifestyleSingleton(),
//                Component.For<ITypeFinder, TypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
//                Component.For<IAbpPlugInManager, AbpPlugInManager>().ImplementedBy<AbpPlugInManager>().LifestyleSingleton(),
//                Component.For<IAbpModuleManager, AbpModuleManager>().ImplementedBy<AbpModuleManager>().LifestyleSingleton(),
//                Component.For<IAssemblyFinder, AbpAssemblyFinder>().ImplementedBy<AbpAssemblyFinder>().LifestyleSingleton(),
//                Component.For<ILocalizationManager, LocalizationManager>().ImplementedBy<LocalizationManager>().LifestyleSingleton()
//                );
//        }
//    }
//}
