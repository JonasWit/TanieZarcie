using System.Threading.Tasks;
using WEB.Shop.Application.ViewModels;
using WEB.Shop.DataBase;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;

        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Do(ProductViewModel vm)
        {
            _context.Products.Add(new Product 
            { 
                Name = vm.Name, 
                Description = vm.Description, 
                Value = vm.Value 
            });

            await _context.SaveChangesAsync();
        }
    }
}
