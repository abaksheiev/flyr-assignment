using Flyr.App.Contracts;
using Flyr.App.Enums;
using Flyr.App.Models;
using Flyr.App.Strategies;
using static Flyr.App.Models.PurchaseBasket;

namespace Flyr.App.Strategies
{
    internal class CoffeeDiscountStrategy : BaseStrategy, IPricingStrategy
    {
    
        public ProductCode Code => ProductCode.Coffee;

        private const int Threshold = 3;
        private const double Discount =2d/3d;
        private const string DiscountMessage = "Discount Bulk purchases all coffee with 2/3 of price";

        private readonly double PriceOriginal = 0;
        private readonly double PriceDiscount = 0;

        public CoffeeDiscountStrategy(IProductPriceService pps, PurchaseBasket productCard) : base(pps, productCard)
        {
            PriceOriginal = PriceService.GetPriceForProduct(ProductCode.Coffee);
            PriceDiscount = Math.Round(Discount * PriceOriginal, 2);
        }

        public void Scan(Product item)
        {
            var totalCoffee = PurchaseBasket.CountByCode(ProductCode.Coffee);

            PurchaseBasket.AddProduct(ProductCode.Coffee, PriceOriginal);

            if (totalCoffee >= Threshold - 1)
            {
                if (PurchaseBasket.Products.Any(p => p.Price == PriceOriginal)){

                    Func<ProductItem, bool> predicate = p => p.Code == ProductCode.Coffee;
                    Action<ProductItem> printSum = p => p.ApplyDiscountPrice(PriceDiscount, DiscountMessage);

                    PurchaseBasket.ApplyPriceChanges(predicate, printSum);
                }
            }
        }
    }
}
