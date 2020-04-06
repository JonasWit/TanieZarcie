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
            Product = await getProduct.DoAsync(int.Parse(name));
            if (Product == null)
                return RedirectToPage("Index");
            else
                return Page();
        }

        //public async Task<IActionResult> OnGet(string id, [FromServices] GetProduct getProduct)
        //{
        //    Product = await getProduct.DoAsync(int.Parse(id));
        //    if (Product == null)
        //        return RedirectToPage("Index");
        //    else
        //        return Page();
        //}

        public async Task<IActionResult> OnPost([FromServices] AddToCart addToCart)
        {
            var stockAdded = await addToCart.DoAsync(CartViewModel);

            if (stockAdded)
                return RedirectToPage("Cart");
            else
                return Page();
        }

        public IActionResult OnPostReturn()
        {
            return RedirectToPage("MainProducts", "Redirection");
        }
    }
}