using Flyr.App.Contracts;
using Flyr.App.Models;

namespace Flyr.App.Strategies
{
    internal class BaseStrategy
    {
        protected IProductPriceService PriceService;
        protected PurchaseBasket PurchaseBasket;

        public BaseStrategy(IProductPriceService priceService, PurchaseBasket purchaseBasket)
        {
            PriceService = priceService ?? throw new ArgumentNullException(nameof(priceService));
            PurchaseBasket = purchaseBasket ?? throw new ArgumentNullException(nameof(purchaseBasket));
        }
    }
}
