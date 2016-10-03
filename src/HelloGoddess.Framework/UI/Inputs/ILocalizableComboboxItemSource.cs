using System.Collections.Generic;

namespace HelloGoddess.Infrastructure.UI.Inputs
{
    public interface ILocalizableComboboxItemSource
    {
        ICollection<ILocalizableComboboxItem> Items { get; }
    }
}