using System.Collections.Generic;
using System.Threading.Tasks;
using HelloGoddess.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;

namespace HelloGoddess.Infrastructure.Configuration
{
    /// <summary>
    /// Implements default behavior for ISettingStore.
    /// Only <see cref="GetSettingOrNullAsync"/> method is implemented and it gets setting's value
    /// from application's configuration file if exists, or returns null if not.
    /// </summary>
    public class DefaultConfigSettingStore : ISettingStore
    {
        /// <summary>
        /// Gets singleton instance.
        /// </summary>
        public static DefaultConfigSettingStore Instance { get { return SingletonInstance; } }
        private static readonly DefaultConfigSettingStore SingletonInstance = new DefaultConfigSettingStore();

        private DefaultConfigSettingStore()
        {
        }

        public Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name)
        {
            string value = null;//ConfigurationManager.AppSettings[name];//todo yw 
            
            if (value == null)
            {
                return Task.FromResult<SettingInfo>(null);
            }

            return Task.FromResult(new SettingInfo(tenantId, userId, name, value));
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(SettingInfo setting)
        {
            LogHelper.Logger.LogWarning("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support DeleteAsync.");
        }

        /// <inheritdoc/>
        public async Task CreateAsync(SettingInfo setting)
        {
            LogHelper.Logger.LogWarning("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support CreateAsync.");
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(SettingInfo setting)
        {
            LogHelper.Logger.LogWarning("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support UpdateAsync.");
        }

        /// <inheritdoc/>
        public Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId)
        {
            LogHelper.Logger.LogWarning("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support GetAllListAsync.");
            return Task.FromResult(new List<SettingInfo>());
        }
    }
}