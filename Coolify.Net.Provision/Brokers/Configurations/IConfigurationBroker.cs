// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Provision.Models.Configurations;

namespace Coolify.Net.Provision.Brokers.Configurations
{
    public interface IConfigurationBroker
    {
        CoolifyProvisionConfiguration GetConfigurations();
    }
}
