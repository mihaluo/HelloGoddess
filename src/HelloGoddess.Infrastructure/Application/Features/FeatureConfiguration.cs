using HelloGoddess.Infrastructure.Collections;

namespace HelloGoddess.Infrastructure.Application.Features
{
    internal class FeatureConfiguration : IFeatureConfiguration
    {
        public ITypeList<FeatureProvider> Providers { get; private set; }

        public FeatureConfiguration()
        {
            Providers = new TypeList<FeatureProvider>();
        }
    }
}