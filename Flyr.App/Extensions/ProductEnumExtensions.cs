using Flyr.App.Enums;
using Flyr.App.Extensions.Attributes;

namespace Flyr.App.Extensions
{
    public static class ProductEnumExtensions
    {
        public static string? GetStringValue(this ProductCode productCode)
        {
            var fieldInfo = typeof(ProductCode).GetField(productCode.ToString());
            var attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(StringValueAttribute)) as StringValueAttribute;
            return attribute?.Value;
        }
    }
}
