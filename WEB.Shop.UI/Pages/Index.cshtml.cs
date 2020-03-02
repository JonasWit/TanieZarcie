using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Application.Products;

namespace WEB.Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 2;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public void OnGet([FromServices] GetProducts getProducts)
        {
            Products = getProducts.Do(CurrentPage, PageSize);
            Count = getProducts.Do().Count();
        }

        public void OnPost([FromServices] GetProducts getProducts)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.Do(CurrentPage, PageSize, SearchString);
                Count = getProducts.Do(SearchString).Count();
            }
            else 
            {
                Products = getProducts.Do(CurrentPage, PageSize);
                Count = getProducts.Do().Count();
            }


      

            //todo: pagnination
        }
    }
}
