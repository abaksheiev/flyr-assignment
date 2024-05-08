using Flyr.App.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flyr.App.Enums
{
    public enum ProductCode
    {
        [StringValue("GR1")]
        GreenTea,

        [StringValue("SR1")]
        Strawberries,

        [StringValue("CF1")]
        Coffee
    }
}
