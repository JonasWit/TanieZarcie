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

        [BindProperty]
        public string SelectedShop { get; set; }
        public List<string> Shops { get; set; } = new List<string> { "Biedronka", "Kaufland" };

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

        public void OnGet([FromServices] GetProducts getProducts, [FromServices] GetSearchString getSearchString)
        {
            getSearchString.Do(out string searchString);
            SearchString = searchString;
            HandleSearchActions(getProducts);
        }

        public void OnPost([FromServices] GetProducts getProducts, [FromServices] SaveSearchString saveSearchString)
        {
            saveSearchString.Do(SearchString);
            HandleSearchActions(getProducts);
        }

        private void HandleSearchActions(GetProducts getProducts)
        {
            if (!string.IsNullOrEmpty(SelectedShop) && SelectedShop == "All" && string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.Shop, SelectedShop},
                    { GetProducts.LookupCriteria.PageNumber, CurrentPage.ToString()},
                    { GetProducts.LookupCriteria.PageSize, PageSize.ToString()},
                });

                Count = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.Shop, SelectedShop},
                })
                .Count();
                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && SelectedShop == "All" && !string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.SearchString, SearchString},
                    { GetProducts.LookupCriteria.PageNumber, CurrentPage.ToString()},
                    { GetProducts.LookupCriteria.PageSize, PageSize.ToString()},
                });

                Count = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.SearchString, SearchString},
                })
                .Count();
                return;
            }

            if (string.IsNullOrEmpty(SelectedShop) && string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.PageNumber, CurrentPage.ToString()},
                    { GetProducts.LookupCriteria.PageSize, PageSize.ToString()},
                });

                Count = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>()).Count();

                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && !string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.SearchString, SearchString},
                    { GetProducts.LookupCriteria.Shop, SelectedShop},
                    { GetProducts.LookupCriteria.PageNumber, CurrentPage.ToString()},
                    { GetProducts.LookupCriteria.PageSize, PageSize.ToString()},
                });

                Count = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.SearchString, SearchString},
                    { GetProducts.LookupCriteria.Shop, SelectedShop},
                })
                .Count();

                return;
            }

            if (!string.IsNullOrEmpty(SelectedShop) && string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.Shop, SelectedShop},
                    { GetProducts.LookupCriteria.PageNumber, CurrentPage.ToString()},
                    { GetProducts.LookupCriteria.PageSize, PageSize.ToString()},
                });

                Count = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.Shop, SelectedShop},
                })
                .Count();

                return;
            }

            if (string.IsNullOrEmpty(SelectedShop) && !string.IsNullOrEmpty(SearchString))
            {
                Products = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.SearchString, SearchString},
                    { GetProducts.LookupCriteria.PageNumber, CurrentPage.ToString()},
                    { GetProducts.LookupCriteria.PageSize, PageSize.ToString()},
                });

                Count = getProducts.Do(new Dictionary<GetProducts.LookupCriteria, string>
                {
                    { GetProducts.LookupCriteria.SearchString, SearchString},
                })
                .Count();

                return;
            }
        }

    }
}
