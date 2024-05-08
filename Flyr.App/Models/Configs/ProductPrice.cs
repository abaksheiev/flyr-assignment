using Flyr.App.Enums;

namespace Flyr.App.Models.Configs
{
    internal class AppConfiguration
    {
        public ProductPrice ProductPrice { get; set; } = new ProductPrice();
    }

    internal class ProductPrice
    {
        public IEnumerable<ProductPriceItem> Data { get; set; } = new List<ProductPriceItem>();
    }

    internal class ProductPriceItem
    {
        public required string Code { get; set; }
        public double Price { get; set; }
    }
}
