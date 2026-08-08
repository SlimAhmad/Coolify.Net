// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Provision.Models.Configurations;
using Microsoft.Extensions.Configuration;

namespace Coolify.Net.Provision.Brokers.Configurations
{
    public class ConfigurationBroker : IConfigurationBroker
    {
        public CoolifyProvisionConfiguration GetConfigurations()
        {
            string basePath = AppContext.BaseDirectory;

            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: false)
                .Build();

            return configurationRoot.Get<CoolifyProvisionConfiguration>();
        }
    }
}
