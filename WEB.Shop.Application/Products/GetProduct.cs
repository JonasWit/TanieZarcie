using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.DataBase;

namespace WEB.Shop.Application.Products
{
    public class GetProduct
    {
        private ApplicationDbContext _context;

        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductViewModel Do(string name) =>
            _context.Products
            .Include(x => x.Stock)
            .Where(x => x.Name == name)
            .Select(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = $"{x.Value.ToString("N2")} pln",

                Stock = x.Stock.Select(y => new StockViewModel 
                { 
                    Id = y.Id,
                    Description = y.Description,
                    InStock = y.Quantity > 0
                })
            })
            .FirstOrDefault();

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool InStock { get; set; }
        }
    }
}
