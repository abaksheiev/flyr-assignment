using Microsoft.Extensions.Configuration;

namespace Flyr.App.SetupHelpers
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")   
                .Build();
        }
    }
}
