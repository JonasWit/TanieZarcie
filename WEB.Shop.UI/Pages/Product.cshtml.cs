using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WEB.Shop.Application.Cart;
using WEB.Shop.Application.Products;

namespace WEB.Shop.UI.Pages
{
    public class ProductModel : PageModel
    {
        public GetProduct.ProductViewModel Product { get; set; }

        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public async Task<IActionResult> OnGet(string name, [FromServices] GetProduct getProduct)
        {
            Product = await getProduct.Do(name.Replace("-", " "));
            if (Product == null)
                return RedirectToPage("Index");
            else
                return Page();
        }

        public async Task<IActionResult> OnPost([FromServices] AddToCart addToCart)
        {
            var stockAdded = await addToCart.Do(CartViewModel);

            if (stockAdded)
                return RedirectToPage("Cart");
            else
                //TODO: add a warning
                return Page();
        }
    }
}