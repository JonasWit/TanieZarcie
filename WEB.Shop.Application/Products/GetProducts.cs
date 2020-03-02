using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Domain.Extensions;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Products
{
    [Service]
    public class GetProducts
    {
        private IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do() =>
            _productManager.GetProductsWithStock(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Producer = x.Producer,
                Seller = x.Seller,
                Category = x.Category,
                SourceUrl = x.SourceUrl,
                Value = x.Value.MonetaryValue(),
                StockCount = x.Stock.Sum(y => y.Quantity),
                TimeStamp = x.TimeStamp
            });

        public IEnumerable<ProductViewModel> Do(string searchString) =>
            _productManager.GetProductsWithStock(searchString, x => new ProductViewModel
             {
                 Name = x.Name,
                 Description = x.Description,
                 Producer = x.Producer,
                 Seller = x.Seller,
                 Category = x.Category,
                 SourceUrl = x.SourceUrl,
                 Value = x.Value.MonetaryValue(),
                 StockCount = x.Stock.Sum(y => y.Quantity),
                 TimeStamp = x.TimeStamp
             });

        public IEnumerable<ProductViewModel> Do(int pageNumber, int pageSize) =>
            _productManager.GetProductsWithStock(pageNumber, pageSize, x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Producer = x.Producer,
                Seller = x.Seller,
                Category = x.Category,
                SourceUrl = x.SourceUrl,
                Value = x.Value.MonetaryValue(),
                StockCount = x.Stock.Sum(y => y.Quantity),
                TimeStamp = x.TimeStamp
            });

        public IEnumerable<ProductViewModel> Do(int pageNumber, int pageSize, string searchString) =>
            _productManager.GetProductsWithStock(pageNumber, pageSize, searchString,  x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Producer = x.Producer,
                Seller = x.Seller,
                Category = x.Category,
                SourceUrl = x.SourceUrl,
                Value = x.Value.MonetaryValue(),
                StockCount = x.Stock.Sum(y => y.Quantity),
                TimeStamp = x.TimeStamp
            });


        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Producer { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public string SourceUrl { get; set; }
            public string Value { get; set; }
            public int StockCount { get; set; }
            public DateTime TimeStamp { get; set; }
        }
    }
}
