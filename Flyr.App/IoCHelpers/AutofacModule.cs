using Autofac;
using Flyr.App.Contracts;
using Flyr.App.Models;
using Flyr.App.Models.Configs;
using Flyr.App.Services;
using Flyr.App.Strategies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Flyr.App.SetupHelpers
{
    internal class AutofacModule : Module
    {
        private readonly IConfiguration _configuration;

        public AutofacModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Register IConfiguration
            builder.RegisterInstance(_configuration).As<IConfiguration>();

            // Register ProductPriceData using IConfiguration and bind it to the configuration data
            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var productPriceData = new ProductPrice();
                config.GetSection("ProductPrice").Bind(productPriceData); // Bind directly to ProductPrice
                return productPriceData;
            }).AsSelf().SingleInstance();

            // Register IOptions<ProductPrice>
            builder.Register(c => Options.Create(c.Resolve<ProductPrice>()))
                   .As<IOptions<ProductPrice>>()
                   .SingleInstance();

            // Register other services or dependencies
            // Automatically register types that implement IPricingStrategy from the current assembly
            builder.RegisterType<BulkDiscountStrategy>().As<IPricingStrategy>();
            builder.RegisterType<CoffeeDiscountStrategy>().As<IPricingStrategy>();
            builder.RegisterType<BuyOneGetOneFreeStrategy>().As<IPricingStrategy>();

            // Services
            builder.RegisterType<ProductCheckoutService>();
            builder.RegisterType<PurchaseBasket>().SingleInstance();
            builder.RegisterType<ProductPriceService>().As<IProductPriceService>();
        }
    }
}
