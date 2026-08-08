using Coolify.Net.Clients.Coolify.Net;
using Coolify.Net.Provision.Brokers.Configurations;
using Coolify.Net.Provision.Brokers.Loggings;
using Coolify.Net.Provision.Models.Configurations;
using Coolify.Net.Provision.Services.Foundations.CoolifyProvisions;
using Coolify.Net.Provision.Services.Processings;

namespace Coolify.Net.Provision
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfigurationBroker configurationBroker = new ConfigurationBroker();
            CoolifyProvisionConfiguration configuration = configurationBroker.GetConfigurations();

            ICoolifyClient coolifyClient = new CoolifyClient(options =>
            {
                options.BaseUrl = configuration.Coolify.BaseUrl;
                options.ApiToken = configuration.Coolify.ApiToken;
            });

            ICoolifyProvisionService coolifyProvisionService =
                new CoolifyProvisionService(
                    coolifyClient,
                    loggingBroker: new LoggingBroker());

            ICoolifyProvisioningProcessingService coolifyProvisioningProcessingService =
                new CoolifyProvisioningProcessingService(
                    coolifyProvisionService,
                    configurationBroker);

            Console.WriteLine("Starting Coolify provisioning...");
            await coolifyProvisioningProcessingService.ProcessAsync();
            Console.WriteLine("Coolify provisioning completed successfully.");
        }
    }
}
