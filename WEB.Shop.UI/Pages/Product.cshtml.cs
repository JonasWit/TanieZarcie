using Microsoft.AspNetCore.Http;
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

        [BindProperty]
        public Test ProductTest { get; set; }

        public class Test
        {

            public string Id { get; set; }

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

        public IActionResult OnPost()
        {
            var currentId = HttpContext.Session.GetString("id");

            HttpContext.Session.SetString("id", ProductTest.Id);

            return RedirectToPage("Index");
        }

    }
}