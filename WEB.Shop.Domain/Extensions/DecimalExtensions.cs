using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Shop.Domain.Extensions
{
    public static class DecimalExtensions
    {
        public static string MonetaryValue(this decimal value) => $"{value.ToString("N2")} zł";


    }
}
