using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Shop.Application.Products;
using WEB.Shop.Application.ViewModels;
using WEB.Shop.DataBase;

namespace WEB.Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ProductViewModel Product { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        private ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Products = new GetProducts(_context).Do();
        }

        public async Task<IActionResult> OnPost()
        {
            await new CreateProduct(_context).Do(Product);

            return RedirectToPage("Index");
        }
    }
}
