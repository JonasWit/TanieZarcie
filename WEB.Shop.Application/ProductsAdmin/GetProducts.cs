using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Domain.Extensions;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.ProductsAdmin
{
    [TransientService]
    public class GetProducts
    {
        private IProductManager _productManager;

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<Response> GetAllProducts() =>
            _productManager.GetProductsWithStock(product => new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value,

                OnSale = product.OnSale,
                SaleValue = product.SaleValue,
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                StockCount = product.Stock.Sum(y => y.Quantity),
                TimeStamp = product.TimeStamp
            });

        public IEnumerable<Response> GetShopProducts(string shop) =>
            _productManager.GetProductsWithStockWithCondition(product => new Response
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value,

                OnSale = product.OnSale,
                SaleValue = product.SaleValue,
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                StockCount = product.Stock.Sum(y => y.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Distributor.DistributorName.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()));

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Producer { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public string SourceUrl { get; set; }
            public decimal Value { get; set; }

            public bool OnSale { get; set; }
            public decimal SaleValue { get; set; }
            public string SaleDescription { get; set; }
            public DateTime SaleDeadline { get; set; }

            public int StockCount { get; set; }
            public DateTime TimeStamp { get; set; }
        }
    }
}
