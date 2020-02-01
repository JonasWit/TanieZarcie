using System.Linq;
using WEB.Shop.DataBase;

namespace WEB.Shop.Application.ProductsAdmin
{
    public class GetProduct
    {
        private ApplicationDbContext _context;

        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public Response Do(int id) =>
            _context.Products.Where(i => i.Id == id).Select(i => new Response
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Value = i.Value
            })
            .FirstOrDefault();

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public string ValueDispaly => $"{Value.ToString("N2")} pln";
        }
    }
}
