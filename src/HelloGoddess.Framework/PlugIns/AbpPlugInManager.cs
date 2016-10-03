using System.Collections.Generic;

namespace HelloGoddess.Infrastructure.PlugIns
{
    public class AbpPlugInManager : IAbpPlugInManager
    {
        public PlugInSourceList PlugInSources { get; }

        public AbpPlugInManager()
        {
            PlugInSources = new PlugInSourceList();
        }
    }
}