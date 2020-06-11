namespace WEB.Shop.Domain.Models
{
    public class PromoSheetUrl
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int ShopId { get; set; }
        public ShopData Shop { get; set; }
    }
}
