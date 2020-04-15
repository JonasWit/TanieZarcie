using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Application.Enums;
using WEB.Shop.Application.Products;

namespace WEB.Shop.Application.BlazorServices
{
    [ScopedService]
    public class SummariesBlazorService
    {
        private readonly GetProducts _getProducts;

        public SummariesBlazorService(GetProducts getProducts) => _getProducts = getProducts;

        public class ShopOverview
        {
            public int Id { get; set; }
            public string Shop { get; set; }
            public decimal TotalProducts { get; set; }
            public decimal TotalProductsOnSale { get; set; }
            public string SalePercentage => TotalProducts != 0 ? $"{Math.Round((TotalProductsOnSale * 100) / TotalProducts, 2)} %" : "";
        }

        public List<ShopOverview> GetShopsData()
        {
            var data = _getProducts.GetAllProducts().ToList();
            var result = new List<ShopOverview>();
            var id = 1;

            foreach (var shop in Enum.GetNames(typeof(Shops)).ToList())
            {
                result.Add(new ShopOverview
                { 
                    Id = id,
                    Shop = shop.ToString(),
                    TotalProducts = data.Count(product => product.Seller == shop),
                    TotalProductsOnSale = data.Count(product => product.Seller == shop && product.OnSale)
                });
                id++;
            }

            return result;
        }
    }
}
