using System.Collections.Generic;

namespace WEB.Shop.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Producer { get; set; }
        public string Seller { get; set; }
        public string Category { get; set; }
        public string SourceUrl { get; set; }
        public decimal Value { get; set; }

        public ICollection<Stock> Stock { get; set; }
    }
}
