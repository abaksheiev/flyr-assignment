using Flyr.App.Enums;
using Flyr.App.Models;

namespace Flyr.App.Contracts
{
    internal interface IPricingStrategy
    {
        ProductCode Code { get; }
        void Scan(Product item);
    }
}
