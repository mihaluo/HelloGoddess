using HelloGoddess.Infrastructure.Localization;
using Newtonsoft.Json;

namespace HelloGoddess.Infrastructure.UI.Inputs
{
    public interface ILocalizableComboboxItem
    {
        string Value { get; set; }

        [JsonConverter(typeof(LocalizableStringToStringJsonConverter))]
        ILocalizableString DisplayText { get; set; }
    }
}