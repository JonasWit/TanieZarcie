using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB.Shop.Application.Products;
using WEB.Shop.DataBase;

namespace WEB.Shop.UI
{
    public class ProductModel : PageModel
    {
        private ApplicationDbContext _context;

        public GetProduct.ProductViewModel Product { get; set; }

        public ProductModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string name)
        {
            Product = new GetProduct(_context).Do(name.Replace("-"," "));

            if (Product == null)
            {
                return RedirectToPage("Index"); //todo: create my own page
            }
            else 
            {
                return Page();
            }
        }
    }
}