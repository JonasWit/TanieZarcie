﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using WEB.Shop.Application.Products;

namespace WEB.Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        [BindProperty]
        public string SearchString { get; set; }

        [BindProperty]
        public int PageNumber { get; set; } = 0;

        public void OnGet([FromServices] GetProducts getProducts)
        {

            Products = getProducts.Do(PageNumber);

            //todo: pagnination
        }

        public void OnPost([FromServices] GetProducts getProducts)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.Do(PageNumber, SearchString);
            }
            else 
            {
                Products = getProducts.Do(PageNumber);
            }

            //todo: pagnination
        }
    }
}
