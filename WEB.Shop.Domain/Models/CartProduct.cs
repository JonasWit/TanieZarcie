namespace WEB.Shop.Domain.Models
{
    public class CartProduct
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Seller { get; set; }
        public string Producer { get; set; }
        public string Category { get; set; }
        public string SourceUrl { get; set; }
        public decimal Value { get; set; }

        public int StockId { get; set; }
        public int Quantity { get; set; }

    }
}
