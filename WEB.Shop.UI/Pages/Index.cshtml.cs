using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Shop.Application.Products;
using WEB.Shop.Application.ProductsAdmin;
using WEB.Shop.Application.ViewModels;
using WEB.Shop.DataBase;

namespace WEB.Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductViewModel> Products { get; set; }

        public void OnGet()
        {
            Products = new Application.Products.GetProducts(_context).Do();
        }
    }
}
