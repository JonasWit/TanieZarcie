namespace WEB.Shop.Domain.Extensions
{
    public static class DecimalExtensions
    {
        public static string MonetaryValue(this decimal value) => $"{value:N2} zł";
    }
}
