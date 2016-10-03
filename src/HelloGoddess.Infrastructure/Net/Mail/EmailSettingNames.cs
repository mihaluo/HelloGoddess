namespace HelloGoddess.Infrastructure.Net.Mail
{
    /// <summary>
    /// Declares names of the settings defined by <see cref="EmailSettingProvider"/>.
    /// </summary>
    public static class EmailSettingNames
    {
        /// <summary>
        /// HelloGoddess.Infrastructure.Net.Mail.DefaultFromAddress
        /// </summary>
        public const string DefaultFromAddress = "HelloGoddess.Infrastructure.Net.Mail.DefaultFromAddress";

        /// <summary>
        /// HelloGoddess.Infrastructure.Net.Mail.DefaultFromDisplayName
        /// </summary>
        public const string DefaultFromDisplayName = "HelloGoddess.Infrastructure.Net.Mail.DefaultFromDisplayName";

        /// <summary>
        /// SMTP related email settings.
        /// </summary>
        public static class Smtp
        {
            /// <summary>
            /// HelloGoddess.Infrastructure.Net.Mail.Smtp.Host
            /// </summary>
            public const string Host = "HelloGoddess.Infrastructure.Net.Mail.Smtp.Host";

            /// <summary>
            /// HelloGoddess.Infrastructure.Net.Mail.Smtp.Port
            /// </summary>
            public const string Port = "HelloGoddess.Infrastructure.Net.Mail.Smtp.Port";

            /// <summary>
            /// HelloGoddess.Infrastructure.Net.Mail.Smtp.UserName
            /// </summary>
            public const string UserName = "HelloGoddess.Infrastructure.Net.Mail.Smtp.UserName";

            /// <summary>
            /// HelloGoddess.Infrastructure.Net.Mail.Smtp.Password
            /// </summary>
            public const string Password = "HelloGoddess.Infrastructure.Net.Mail.Smtp.Password";

            /// <summary>
            /// HelloGoddess.Infrastructure.Net.Mail.Smtp.Domain
            /// </summary>
            public const string Domain = "HelloGoddess.Infrastructure.Net.Mail.Smtp.Domain";

            /// <summary>
            /// HelloGoddess.Infrastructure.Net.Mail.Smtp.EnableSsl
            /// </summary>
            public const string EnableSsl = "HelloGoddess.Infrastructure.Net.Mail.Smtp.EnableSsl";

            /// <summary>
            /// HelloGoddess.Infrastructure.Net.Mail.Smtp.UseDefaultCredentials
            /// </summary>
            public const string UseDefaultCredentials = "HelloGoddess.Infrastructure.Net.Mail.Smtp.UseDefaultCredentials";
        }
    }
}