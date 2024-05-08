using Flyr.App.Contracts;
using Flyr.App.Enums;
using Flyr.App.Models;

namespace Flyr.App.Strategies
{
    internal class BuyOneGetOneFreeStrategy : BaseStrategy, IPricingStrategy
    {
        public ProductCode Code => ProductCode.GreenTea;

        private const string BonusGreenTea = "Bonus Green Tea";

        private readonly double PriceOriginal = 0;
        private readonly double PriceDiscount = 0;

        public BuyOneGetOneFreeStrategy(IProductPriceService pps, PurchaseBasket pc) : base(pps, pc)
        {
            PriceOriginal = PriceService.GetPriceForProduct(ProductCode.GreenTea);
        }

        public void Scan(Product item)
        {
            PurchaseBasket.AddProduct(item.Code, PriceOriginal);

            // Add Bonus Green Tea
            PurchaseBasket.AddProduct(item.Code, PriceDiscount, BonusGreenTea, isDiscount:true);
        }
    }
}
