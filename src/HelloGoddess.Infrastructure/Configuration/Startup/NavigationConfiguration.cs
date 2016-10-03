using HelloGoddess.Infrastructure.Collections;
using HelloGoddess.Infrastructure.Application.Navigation;

namespace HelloGoddess.Infrastructure.Configuration.Startup
{
    internal class NavigationConfiguration : INavigationConfiguration
    {
        public ITypeList<NavigationProvider> Providers { get; private set; }

        public NavigationConfiguration()
        {
            Providers = new TypeList<NavigationProvider>();
        }
    }
}