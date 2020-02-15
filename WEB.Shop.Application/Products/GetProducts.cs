using System.Collections.Generic;
using System.Linq;
using WEB.Shop.DataBase;

namespace WEB.Shop.Application.Products
{
    public class GetProducts
    {
        private ApplicationDbContext _context;

        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductViewModel> Do() => 
            _context.Products.ToList().Select(i => new ProductViewModel
            {
                Name = i.Name,
                Description = i.Description,
                Producer = i.Producer,
                Seller = i.Seller,
                Category = i.Category,
                SourceUrl = i.SourceUrl,
                Value = $"{i.Value.ToString("N2")} Zł"
            });

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Producer { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public string SourceUrl { get; set; }
            public string Value { get; set; }
        }
    }
}
