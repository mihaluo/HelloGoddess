using System.Collections.Generic;
using System.Collections.Immutable;
using HelloGoddess.Infrastructure.Configuration.Startup;
using HelloGoddess.Infrastructure.Dependency;

namespace HelloGoddess.Infrastructure.Localization
{
    public class DefaultLanguageProvider : ILanguageProvider, ITransientDependency
    {
        private readonly ILocalizationConfiguration _configuration;

        public DefaultLanguageProvider(ILocalizationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IReadOnlyList<LanguageInfo> GetLanguages()
        {
            return _configuration.Languages.ToImmutableList();
        }
    }
}