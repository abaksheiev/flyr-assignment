using Flyr.App.Contracts;
using Flyr.App.Extensions;
using Flyr.App.Models;
using static Flyr.App.Models.PurchaseBasket;

namespace Flyr.App.Services
{
    internal class ProductCheckoutService
    {
        private IEnumerable<IPricingStrategy> _productStrategies;
        private PurchaseBasket _productCard;

        public ProductCheckoutService(IEnumerable<IPricingStrategy> strategies, PurchaseBasket productCard)
        {
            _productStrategies = strategies;
            _productCard = productCard;
        }

        public void Scan(Product item)
        {
            var strategy = _productStrategies.Where(w => w.Code == item.Code).SingleOrDefault();

            if (strategy != null)
            {
                strategy.Scan(item);
            }
        }

        public double TotalPrice()
        {
            var totalPrice = _productCard.GetTotalPrice();

            return Math.Round(totalPrice, 2);
        }

        internal int TotalProduct()
        {
            return _productCard.GetProductCount();
        }

        internal void Reset()
        {
            _productCard.Reset();
        }

        internal void PrintProduct()
        {
            foreach (ProductItem item in _productCard.Products)
            {
                var code = $"{item.Code}".PadRight(15);
                var codeName = $"{item.Code.GetStringValue()}".PadRight(10);
                var comments = $"{item.Price}".PadRight(20);
                var isDiscount = $"{item.Discount}".PadRight(5);

                Console.WriteLine($"|{code}|{codeName}|{isDiscount}|{comments}|");
            }
        }
    }
}
