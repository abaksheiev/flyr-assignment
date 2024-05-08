using Flyr.App.Enums;

namespace Flyr.App.Contracts
{
    interface IProductPriceService
    {
        double GetPriceForProduct(ProductCode code);
    }
}
