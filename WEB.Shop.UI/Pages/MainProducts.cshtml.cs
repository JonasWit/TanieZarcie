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

        public void OnGet([FromServices] GetProducts getProducts, 
            [FromServices] GetSearchString getSearchString,
            [FromServices] GetSelectedShop getSelectedShop)
        {
            getSearchString.Do(out string searchString);
            getSelectedShop.Do(out string selectedShop);

            SearchString = searchString;
            SelectedShop = selectedShop;

            HandleSearchActions(getProducts);
        }

        public void OnPostShowAll([FromServices] GetProducts getProducts)
        {
            Products = getProducts.GetAllProductsWithPagination(CurrentPage, PageSize);
            Count = getProducts.GetAllProducts().Count();
            return;
        }

        public void OnPostSearch(
            [FromServices] GetProducts getProducts, 
            [FromServices] SaveSearchString saveSearchString, 
            [FromServices] SaveSelectedShop saveSelectedShop,
            [FromServices] SaveCurrentPage saveCurrentPage,
            [FromServices] GetSearchString getSearchString,
            [FromServices] GetSelectedShop getSelectedShop,
            [FromServices] GetCurrentPage getCurrentPage)
        {
            getSearchString.Do(out string searchString);
            getSelectedShop.Do(out string selectedShop);
            getCurrentPage.Do(out int currentPage);

            if (searchString != SearchString)
            {
                CurrentPage = 1;
                saveSearchString.Do(SearchString);
            }

            if (selectedShop != SelectedShop)
            {
                CurrentPage = 1;
                saveSelectedShop.Do(SelectedShop);
            }

            if (currentPage != CurrentPage)
            {
                saveCurrentPage.Do(CurrentPage);
            }

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

            if (!string.IsNullOrEmpty(SelectedShop) && SelectedShop == "Wszystkie" && string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetAllProductsWithPagination(CurrentPage, PageSize);
                Count = getProducts.GetAllProducts().Count();
                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && SelectedShop == "Wszystkie" && !string.IsNullOrEmpty(SearchString))
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
