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

        public IEnumerable<ProductViewModel> Do() => 
            _context.Products.ToList().Select(i => new ProductViewModel
            {
                Name = i.Name,
                Description = i.Description,
                Value = i.Value
            });
    }
}
