using Flyr.App.Contracts;
using Flyr.App.Enums;
using Flyr.App.Models;

namespace Flyr.App.Strategies
{
    internal class BulkDiscountStrategy : BaseStrategy, IPricingStrategy
    {
        private const int Threshold = 3;
        private const double DiscountPrice = 4.5d;
        private const string DiscountMessage = "Discount Bulk purchases";

        private readonly double PriceOriginal = 0;
        public BulkDiscountStrategy(IProductPriceService pps, PurchaseBasket productCard) : base(pps, productCard) {
            PriceOriginal = PriceService.GetPriceForProduct(ProductCode.Strawberries);
        }

        public ProductCode Code => ProductCode.Strawberries;

        public void Scan(Product item)
        {
            // Check how many product already in PurchaseBasket
            var totalAlreadyPresent = PurchaseBasket.Products.Count(c => c.Code == ProductCode.Strawberries && !c.Discount);

            // Apply discount for already present items
            if (totalAlreadyPresent >= Threshold - 1)
            {
                PurchaseBasket.AddProduct(item.Code, DiscountPrice, DiscountMessage, isDiscount: true);
                return;
            }

            // Add normal product Item with original price
            PurchaseBasket.AddProduct(item.Code, PriceOriginal);
        }
    }
}
