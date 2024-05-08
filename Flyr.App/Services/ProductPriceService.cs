using Flyr.App.Contracts;
using Flyr.App.Enums;
using Flyr.App.Extensions;
using Flyr.App.Models.Configs;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Flyr.UnitTests")]
namespace Flyr.App.Services
{
    
    internal class ProductPriceService : IProductPriceService
    {
        private ProductPrice ProductPriceConfig { get; set; }

        public ProductPriceService(IOptions<ProductPrice> productPrices) {

            ProductPriceConfig = productPrices?.Value ?? throw new ArgumentNullException(nameof(productPrices));   
        }
        public double GetPriceForProduct(ProductCode code)
        {
            var productPrice = ProductPriceConfig.Data.SingleOrDefault(x => x.Code.Equals(code.GetStringValue(), StringComparison.CurrentCultureIgnoreCase));

            if (productPrice != null)
            {
                return productPrice.Price;
            }

            throw new Exception("Product does not have configuration");
        }
    }
}
