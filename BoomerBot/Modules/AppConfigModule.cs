using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoomerBot.Modulos
{
    public static class AppConfigModule
    {
        public static IConfigurationRoot Config => LazyConfig.Value;

        private static readonly Lazy<IConfigurationRoot> LazyConfig = new Lazy<IConfigurationRoot>(() => new ConfigurationBuilder()
            .SetBasePath(IISHelperModule.GetAppPath())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build());
    }
}
