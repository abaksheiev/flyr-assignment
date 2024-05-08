using Flyr.App.Enums;
using Flyr.App.Extensions;
using System.Linq;

namespace Flyr.App.Models
{
    internal class PurchaseBasket
    {
        private List<ProductItem> _products = new List<ProductItem>();

        public IEnumerable<ProductItem> Products => _products;

        internal void AddProduct(ProductCode Code, double price, string comment = null, bool isDiscount = false)
        {
            _products.Add(new ProductItem(Code, price, comment = comment, isDiscount: isDiscount));
        }

        internal int CountByCode(ProductCode code) {
            return _products.Count(p => p.Code == code);    
        }

        internal class ProductItem {

            public ProductCode Code { get; private set; }

            public double Price { get; private set; }

            public string Comment { get; private set; }

            public bool Discount { get; private set; }

            public ProductItem(ProductCode code, double price, string comment = null, bool isDiscount = false)
            {
                Code = code;
                Price = price;
                Comment = comment;
                Discount = isDiscount;
            }

            internal void ApplyDiscountPrice(double discountPrice, string comment)
            {
                Price = discountPrice;
                Comment = comment;
                Discount = true;
            }
        }

        public double GetTotalPrice()
        {
            return _products.Sum(p => p.Price);
        }

        internal void ApplyPriceChanges(Func<ProductItem, bool> predicate, Action<ProductItem> action)
        {
            foreach (var item in _products.Where(predicate))
            {
                action(item);
            }
        }

        internal int GetProductCount()
        {
            return _products.Count();
        }

        internal void Reset()
        {
           _products.Clear();
        }
    }
}
