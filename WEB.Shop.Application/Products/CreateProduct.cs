using System.Threading.Tasks;
using WEB.Shop.DataBase;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Products
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;

        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Do(string name, string description, decimal value)
        {
            _context.Products.Add(new Product 
            { 
                Name = name, 
                Description = description, 
                Value = value 
            });

            await _context.SaveChangesAsync();
        }






    }
}
