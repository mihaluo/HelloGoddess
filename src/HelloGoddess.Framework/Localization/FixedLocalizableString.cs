using System;
using System.Globalization;

namespace HelloGoddess.Infrastructure.Localization
{
    /// <summary>
    /// A class that gets the same string on every localization.
    /// </summary>
    public class FixedLocalizableString : ILocalizableString
    {
        /// <summary>
        /// The fixed string.
        /// Whenever Localize methods called, this string is returned.
        /// </summary>
        public virtual string FixedString { get; private set; }

        /// <summary>
        /// Needed for serialization.
        /// </summary>
        private FixedLocalizableString()
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="FixedLocalizableString"/>.
        /// </summary>
        /// <param name="fixedString">
        /// The fixed string.
        /// Whenever Localize methods called, this string is returned.
        /// </param>
        public FixedLocalizableString(string fixedString)
        {
            FixedString = fixedString;
        }

        public string Localize(ILocalizationContext context)
        {
            return FixedString;
        }

        public string Localize(ILocalizationContext context, CultureInfo culture)
        {
            return FixedString;
        }

        public override string ToString()
        {
            return FixedString;
        }
    }
}