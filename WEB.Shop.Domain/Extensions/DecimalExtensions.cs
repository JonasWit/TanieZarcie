namespace WEB.Shop.Domain.Extensions
{
    public static class DecimalExtensions
    {
        public static string MonetaryValue(this decimal value)
        {
            if (value != 0)
            {
                return $"{value:N2} zł";
            }
            else
            {
                return null;
            }
        }
    }
}
