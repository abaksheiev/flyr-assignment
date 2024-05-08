using Autofac;
using FluentAssertions;
using Flyr.App.Contracts;
using Flyr.App.Enums;
using Flyr.App.Extensions;
using Flyr.App.Models;
using Flyr.App.Models.Configs;
using Flyr.App.Services;
using Flyr.App.Strategies;
using Microsoft.Extensions.Options;
using Moq;

namespace Flyr.UnitTests
{
    public class ProductCheckoutServiceTests
    {
        private IContainer _container;
        private ProductCheckoutService productCheckoutService;

        public ProductCheckoutServiceTests()
        {
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

            _container =builder.Build();

            productCheckoutService = _container.Resolve<ProductCheckoutService>();

        }

        private ProductPrice GetMockProductPrices()
        {
            return new ProductPrice
            {
                Data = new List<ProductPriceItem>
                {
                    new ProductPriceItem { Price = 3.0, Code = ProductCode.Coffee.GetStringValue() },
                    new ProductPriceItem  { Price = 5.0, Code = ProductCode.Strawberries.GetStringValue() },
                    new ProductPriceItem  { Price = 1.0, Code = ProductCode.GreenTea.GetStringValue() }
                }
            };
        }

        #region Green Teas
        [Fact]
        public void WhenAddGreenTea_ThenBonusGreenTea_ShouldBeAdded()
        {
            productCheckoutService.Reset();
            productCheckoutService.Scan(Product.CreateGreenTea);

            productCheckoutService.TotalPrice().Should().Be(1);
            productCheckoutService.TotalProduct().Should().Be(2);
        }

        #endregion

        #region Coffee

        [Fact]
        public void WhenBuyTwoCoffee_ThenPrice_ShouldBeWithoutDiscount()
        {
            productCheckoutService.Reset();

            productCheckoutService.Scan(Product.CreateCoffee);
            productCheckoutService.Scan(Product.CreateCoffee);

            productCheckoutService.TotalPrice().Should().Be(6);
            productCheckoutService.TotalProduct().Should().Be(2);
        }

        [Fact]
        public void WhenBuyMoreTreeCoffee_ThenAllCoffee_ShouldBeDiscounted()
        {
            productCheckoutService.Reset();

            productCheckoutService.Scan(Product.CreateCoffee);
            productCheckoutService.Scan(Product.CreateCoffee);
            productCheckoutService.Scan(Product.CreateCoffee);

            productCheckoutService.TotalPrice().Should().Be(6);
            productCheckoutService.TotalProduct().Should().Be(3);
        }
        #endregion

        #region Strawberries

        [Fact]
        public void WhenBuyTwoStrawberries_ThenPrice_ShouldBeOriginal()
        {
            productCheckoutService.Reset();

            productCheckoutService.Scan(Product.CreateStrawberries);
            productCheckoutService.Scan(Product.CreateStrawberries);
    

            productCheckoutService.TotalPrice().Should().Be(10);
            productCheckoutService.TotalProduct().Should().Be(2);
        }

        [Fact]
        public void WhenBuyMoreTreeStrawberries_ThenTheNextOnes_ShouldBeDiscounted() {

            productCheckoutService.Reset();

            productCheckoutService.Scan(Product.CreateStrawberries);
            productCheckoutService.Scan(Product.CreateStrawberries);
            productCheckoutService.Scan(Product.CreateStrawberries);
            productCheckoutService.Scan(Product.CreateStrawberries);

            productCheckoutService.TotalPrice().Should().Be(19);
            productCheckoutService.TotalProduct().Should().Be(4);

        }
        #endregion

    }
}