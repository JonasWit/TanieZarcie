using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Application.Products;
using WEB.Shop.Application.Session;

namespace WEB.Shop.UI.Pages
{
    public class MainProductsModel : PageModel
    {
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        [BindProperty(Name = "selectedShop", SupportsGet = true)]
        public string SelectedShop { get; set; }
        public List<string> Shops { get; set; } = new List<string> { "Biedronka", "Kaufland", "Lidl" };

        [BindProperty]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 20;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public void OnGet([FromServices] GetProducts getProducts, [FromServices] GetSearchString getSearchString, string shopToSelect = "")
        {
            if (!string.IsNullOrEmpty(shopToSelect))
            {
                SelectedShop = shopToSelect;

                getSearchString.Do(out string searchString);
                SearchString = searchString;
                HandleSearchActions(getProducts);
            }
            else
            { 
                getSearchString.Do(out string searchString);
                SearchString = searchString;
                HandleSearchActions(getProducts);         
            }
        }

        public void OnPost([FromServices] GetProducts getProducts, [FromServices] SaveSearchString saveSearchString)
        {
            saveSearchString.Do(SearchString);
            HandleSearchActions(getProducts);
        }

        private void HandleSearchActions(GetProducts getProducts)
        {
            if (string.IsNullOrEmpty(SelectedShop) && string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetAllProductsWithPagination(CurrentPage, PageSize);
                Count = getProducts.GetAllProducts().Count();
                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && SelectedShop == "All" && string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetAllProductsWithPagination(CurrentPage, PageSize);
                Count = getProducts.GetAllProducts().Count();
                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && SelectedShop == "All" && !string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetProductsWithSearchStringAndPagination(CurrentPage, PageSize, SearchString);
                Count = getProducts.GetProductsWithSearchString(SearchString).Count();
                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && !string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetShopProductsWithSearchStringAndPagination(CurrentPage, PageSize, SelectedShop, SearchString);
                Count = getProducts.GetShopProductsWithSearchString(SelectedShop, SearchString).Count();
                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetShopProductsWithPagination(CurrentPage, PageSize, SelectedShop);
                Count = getProducts.GetShopProducts(SelectedShop).Count();
                return;
            }

            if (string.IsNullOrEmpty(SelectedShop) && !string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetProductsWithSearchStringAndPagination(CurrentPage, PageSize, SearchString);
                Count = getProducts.GetProductsWithSearchString(SearchString).Count();
                return;
            }
        }

    }
}
