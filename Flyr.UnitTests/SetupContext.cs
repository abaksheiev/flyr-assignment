using Autofac;
using Flyr.App.Contracts;
using Flyr.App.Enums;
using Flyr.App.Models;
using Flyr.App.Models.Configs;
using Flyr.App.Services;
using Flyr.App.Strategies;
using Microsoft.Extensions.Options;
using Moq;
using Flyr.App.Extensions;

namespace Flyr.UnitTests
{
    internal static class SetupContext
    {
        public static IContainer BuildContainer() {
            var builder = new ContainerBuilder();

            var mockOptions = new Mock<IOptions<ProductPrice>>();
            mockOptions.Setup(x => x.Value).Returns(GetMockProductPrices());

            builder.RegisterInstance(mockOptions.Object).As<IOptions<ProductPrice>>();

            builder.RegisterType<BulkDiscountStrategy>().As<IPricingStrategy>();
            builder.RegisterType<CoffeeDiscountStrategy>().As<IPricingStrategy>();
            builder.RegisterType<BuyOneGetOneFreeStrategy>().As<IPricingStrategy>();

            // Services
            builder.RegisterType<ProductCheckoutService>();
            builder.RegisterType<PurchaseBasket>().SingleInstance();
            builder.RegisterType<ProductPriceService>().As<IProductPriceService>();

            return builder.Build();
        }

        private static ProductPrice GetMockProductPrices()
        {
            return new ProductPrice
            {
                Data = new List<ProductPriceItem>
                {
                    new ProductPriceItem
                    {
                        Price = 3.0,
                        Code = ProductCode.Coffee.GetStringValue()
                    },
                    new ProductPriceItem
                    {
                        Price = 5.0,
                        Code = ProductCode.Strawberries.GetStringValue()
                    },
                    new ProductPriceItem
                    {
                        Price = 1.0,
                        Code = ProductCode.GreenTea.GetStringValue()
                    }
                }
            };
        }
    }
}
