using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WEB.Shop.DataBase;
using static WEB.Shop.Application.Products.GetProducts;

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
