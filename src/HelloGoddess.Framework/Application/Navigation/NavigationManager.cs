using System.Collections.Generic;
using HelloGoddess.Infrastructure.Configuration.Startup;
using HelloGoddess.Infrastructure.Dependency;
using HelloGoddess.Infrastructure.Localization;

namespace HelloGoddess.Infrastructure.Application.Navigation
{
    internal class NavigationManager : INavigationManager, ISingletonDependency
    {
        public IDictionary<string, MenuDefinition> Menus { get; private set; }

        public MenuDefinition MainMenu
        {
            get { return Menus["MainMenu"]; }
        }

        private readonly IIocResolver _iocResolver;
        private readonly INavigationConfiguration _configuration;

        public NavigationManager(IIocResolver iocResolver, INavigationConfiguration configuration)
        {
            _iocResolver = iocResolver;
            _configuration = configuration;

            Menus = new Dictionary<string, MenuDefinition>
                    {
                        {"MainMenu", new MenuDefinition("MainMenu", new FixedLocalizableString("Main menu"))} //TODO: Localization for "Main menu"
                    };
        }

        public void Initialize()
        {
            var context = new NavigationProviderContext(this);

            foreach (var providerType in _configuration.Providers)
            {
                using (var provider = _iocResolver.ResolveAsDisposable<NavigationProvider>(providerType))
                {
                    provider.Object.SetNavigation(context);
                }
            }
        }
    }
}