﻿using System;
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
            public string Shop { get; set; }
            public decimal Products { get; set; }
            public decimal ProductsOnSale { get; set; }
            public string SalePercentage => Products != 0 ? $"{Math.Round((ProductsOnSale * 100) / Products, 2)} %" : "";
        }

        public List<ShopOverview> GetShopsData()
        {
            var data = _getProducts.GetAllProducts().ToList();
            var result = new List<ShopOverview>();

            foreach (var shop in Enum.GetNames(typeof(Shops)).ToList())
            {
                result.Add(new ShopOverview
                { 
                    Shop = shop.ToString(),
                    Products = data.Count(product => product.Seller == shop),
                    ProductsOnSale = data.Count(product => product.Seller == shop && product.OnSale)
                });    
            }

            return result;
        }
    }
}
