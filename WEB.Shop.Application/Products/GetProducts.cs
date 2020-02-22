using Microsoft.EntityFrameworkCore;
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
            _context.Products
                .Include(x => x.Stock)
                .Select(x => new ProductViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Producer = x.Producer,
                    Seller = x.Seller,
                    Category = x.Category,
                    SourceUrl = x.SourceUrl,
                    Value = $"{x.Value.ToString("N2")} Zł",
                    StockCount = x.Stock.Sum(y => y.Quantity)
                })
                .ToList();

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Producer { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public string SourceUrl { get; set; }
            public string Value { get; set; }
            public int StockCount { get; set; }
        }
    }
}
