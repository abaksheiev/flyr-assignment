using Autofac.Core;
using Flyr.App.Contracts;
using Flyr.App.Extensions;
using Flyr.App.Models;
using System.Diagnostics;
using System.Xml.Linq;
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

        internal void PrintProductReports()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"|{"Code".PadRight(15)}|{"Code Name".PadRight(10)}|{"Discount".PadRight(3).PadLeft(3)}|{"Price".PadRight(10)}|");
            Console.WriteLine("------------------------------------------------");
            foreach (ProductItem item in _productCard.Products)
            {
                var code = $"{item.Code}".PadRight(15);
                var codeName = $"{item.Code.GetStringValue()}".PadRight(10);
                var price = $"{item.Price}".PadRight(10);
                var isDiscount = $"{(item.Discount?'x':' ')}".PadRight(8);

                Console.WriteLine($"|{code}|{codeName}|{isDiscount}|{price}|");
            }
            Console.WriteLine("------------------------------------------------");

            Console.WriteLine($"Count Product: {TotalProduct()}");
            Console.WriteLine($"Total price: {TotalPrice()}");
        }
    }
}
