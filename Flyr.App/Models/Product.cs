using Flyr.App.Enums;

namespace Flyr.App.Models
{
    public record Product
    {
        public ProductCode Code { get; private set; }

        public Product(ProductCode code)
        {
            this.Code = code;
        }

        public static Product CreateCoffee => new Product(ProductCode.Coffee);
        public static Product CreateGreenTea => new Product(ProductCode.GreenTea);
        public static Product CreateStrawberries => new Product(ProductCode.Strawberries);
    }
}
