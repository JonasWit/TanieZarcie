using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Application.Enums;
using WEB.Shop.Application.Products;

namespace WEB.Shop.UI.Pages
{
    public class ProductsOverviewModel : PageModel
    {
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        [BindProperty(Name = "selectedShop", SupportsGet = true)]
        public string SelectedShop { get; set; } = "Wszystkie";
        public List<string> Shops { get; set; } = Enum.GetNames(typeof(Shops)).ToList();

        [BindProperty]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 30;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage != TotalPages;

        public IActionResult OnGet(
            int currentPage, string searchString, string selectedShop,
            [FromServices] GetProducts getProducts)
        {
            if (currentPage < 1)
            {
                return RedirectToPage("ProductsOverview", new { currentPage = 1, searchString, selectedShop });
            }

            if (!string.IsNullOrEmpty(selectedShop))
            {
                SelectedShop = selectedShop;
            }

            CurrentPage = currentPage;
            SearchString = searchString;
            HandleSearchActions(getProducts);

            return Page();
        }

        public void OnPostShowAll(
            [FromServices] GetProducts getProducts)
        {
            SearchString = null;
            HandleSearchActions(getProducts);
        }

        public void OnPostSearch(
            [FromServices] GetProducts getProducts)
        {
            HandleSearchActions(getProducts);
        }

        private void HandleSearchActions(GetProducts getProducts)
        {
            if (string.IsNullOrEmpty(SelectedShop) && string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetAllProductsWithPagination(CurrentPage, PageSize);
                Count = getProducts.CountAllProducts();
                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && SelectedShop == "Wszystkie" && string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetAllProductsWithPagination(CurrentPage, PageSize);
                Count = getProducts.CountAllProducts();
                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && SelectedShop == "Wszystkie" && !string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetProductsWithSearchStringAndPagination(CurrentPage, PageSize, SearchString);
                Count = getProducts.CountAllProductsWithSearchString(SearchString);
                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && !string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetShopProductsWithSearchStringAndPagination(CurrentPage, PageSize, SelectedShop, SearchString);
                Count = getProducts.CountProductsForShopWithSearchString(SelectedShop, SearchString);
                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetShopProductsWithPagination(CurrentPage, PageSize, SelectedShop);
                Count = getProducts.CountProductForShop(SelectedShop);
                return;
            }

            if (string.IsNullOrEmpty(SelectedShop) && !string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.GetProductsWithSearchStringAndPagination(CurrentPage, PageSize, SearchString);
                Count = getProducts.CountAllProductsWithSearchString(SearchString);
                return;
            }
        }

        public IEnumerable<int> PageNumbers(int currentPage, int totalPages)
        {
            if (totalPages <= 5)
            {
                for (int i = 1; i <= totalPages; i++)
                {
                    yield return i;
                }
            }
            else
            {
                int midPoint = currentPage < 3 ? 3 : currentPage > totalPages - 2 ? totalPages - 2 : currentPage;

                int lowerBound = midPoint - 2;
                int upperBount = midPoint + 2;

                if (lowerBound != 1)
                {
                    yield return 1;
                    if (lowerBound - 1 > 1)
                    {
                        yield return -1;
                    }
                }

                for (int i = lowerBound; i <= upperBount; i++)
                {
                    yield return i;
                }

                if (upperBount != totalPages)
                {
                    if (totalPages - upperBount > 1)
                    {
                        yield return -1;
                    }
                    yield return totalPages;
                }
            }
        }

        public Tuple<bool, bool, bool, string> UpdateData(DateTime timeStamp)
        {
            var offerTime = DateTime.Now - timeStamp;

            var days = offerTime.Days;
            var hours = offerTime.Hours;
            var minutes = offerTime.Minutes;

            bool red = false;
            bool yellow = false;
            bool green = false;

            string dispalyTime;
            if (days == 1 && days != 0)
            {
                dispalyTime = $"Sprawdzone: {days} dzień temu";
                green = true;
            }
            else if (days < 3 && days != 0)
            {
                dispalyTime = $"Sprawdzone: {days} dni temu";
                yellow = true;
            }
            else if (days > 2 && days != 0)
            {
                dispalyTime = $"Informacje starsze niż 2 dni!";
                red = true;
            }
            else
            {
                if (hours == 0)
                {
                    dispalyTime = $"Sprawdzone: {minutes} min. temu";
                }
                else
                {
                    dispalyTime = $"Sprawdzone: {hours} godz. {minutes} min. temu";
                }

                green = true;
            }

            return new Tuple<bool, bool, bool, string>( red, yellow, green, dispalyTime );
        }
    }
}
