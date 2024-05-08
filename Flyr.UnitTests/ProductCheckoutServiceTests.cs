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
            _container = SetupContext.BuildContainer();
            productCheckoutService = _container.Resolve<ProductCheckoutService>();
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