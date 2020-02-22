using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WEB.Shop.Application.Cart;
using WEB.Shop.Application.Products;
using WEB.Shop.DataBase;

namespace WEB.Shop.UI.Pages
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
        public AddToCart.Request CartViewModel { get; set; }

        public async Task<IActionResult> OnGet(string name)
        {
            Product = await new GetProduct(_context).Do(name.Replace("-"," "));

            if (Product == null)
            {
                return RedirectToPage("Index"); //todo: create my own page
            }
            else 
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPost([FromServices] AddToCart addToCart)
        {
            var stockAdded = await addToCart.Do(CartViewModel);

            if (stockAdded)
            {
                return RedirectToPage("Cart");
            }
            else
            {
                //todo: add a warning
                return Page();
            }
        }
    }
}