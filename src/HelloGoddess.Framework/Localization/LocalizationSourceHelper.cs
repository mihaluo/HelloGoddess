using System.Globalization;
using System.Text.RegularExpressions;
using HelloGoddess.Infrastructure.Configuration.Startup;
using HelloGoddess.Infrastructure.Extensions;
using HelloGoddess.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace HelloGoddess.Infrastructure.Localization
{
    public static class LocalizationSourceHelper
    {
        public static string ReturnGivenNameOrThrowException(ILocalizationConfiguration configuration, string sourceName, string name, CultureInfo culture)
        {
            var exceptionMessage = string.Format(
                "Can not find '{0}' in localization source '{1}'!",
                name, sourceName
                );

            if (!configuration.ReturnGivenTextIfNotFound)
            {
                throw new AbpException(exceptionMessage);
            }

            LogHelper.Logger.LogWarning(exceptionMessage);

            var notFoundText = configuration.HumanizeTextIfNotFound
                ? name.ToSentenceCase(culture)
                : name;

            return configuration.WrapGivenTextIfNotFound
                ? string.Format("[{0}]", notFoundText)
                : notFoundText;
        }
    }
}
