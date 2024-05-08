using Autofac;
using Flyr.App.Models;
using Flyr.App.Models.Configs;
using Flyr.App.Services;
using Flyr.App.SetupHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Flyr.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Build configuration
            var config = ConfigurationHelper.BuildConfiguration();
          
            // Setup Autofac container
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacModule(config));

            // Build the container
            var container = builder.Build();

            // Resolve services and use IOptions<T>
            using (var scope = container.BeginLifetimeScope())
            {
                var service = scope.Resolve<ProductCheckoutService>();

                service.Scan(Product.CreateGreenTea);
                service.Scan(Product.CreateGreenTea);
                service.Scan(Product.CreateGreenTea);

                service.Scan(Product.CreateCoffee);
                service.Scan(Product.CreateCoffee);
                service.Scan(Product.CreateCoffee);

                service.Scan(Product.CreateStrawberries);
                service.Scan(Product.CreateStrawberries);
                service.Scan(Product.CreateStrawberries);

                // Print result
                service.PrintProductReports();
              
            }
        }
    }
}
