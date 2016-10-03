using System.Collections.Generic;

namespace HelloGoddess.Infrastructure.Localization
{
    public interface ILanguageProvider
    {
        IReadOnlyList<LanguageInfo> GetLanguages();
    }
}