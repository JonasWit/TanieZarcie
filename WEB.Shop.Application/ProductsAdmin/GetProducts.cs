using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Application.ViewModels;
using WEB.Shop.DataBase;

namespace WEB.Shop.Application.ProductsAdmin
{
    public class GetProducts
    {
        private ApplicationDbContext _context;

        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Response> Do() => 
            _context.Products.ToList().Select(i => new Response
            {
                Id = i.Id,
                Name = i.Name,
                Value = i.Value
            });

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
            public string ValueDispaly => $"{Value.ToString("N2")} pln";
        }
    }
}
