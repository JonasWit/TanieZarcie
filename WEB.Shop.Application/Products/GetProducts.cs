using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Domain.Extensions;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Products
{
    [TransientService]
    public class GetProducts
    {
        private IProductManager _productManager;

        public enum LookupCriteria
        {
            SearchString = 0,
            PageNumber = 1,
            PageSize = 2,
            Shop = 3
        }

        public GetProducts(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> GetAllProducts() =>
            _productManager.GetProductsWithStock(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer,
                Seller = product.Seller,
                Category = product.Category,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(),
                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            });

        public IEnumerable<ProductViewModel> GetAllProductsWithPagination(int pageNumber, int pageSize) =>
            _productManager.GetProductsWithStockWithPagination(pageNumber, pageSize, product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer,
                Seller = product.Seller,
                Category = product.Category,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(),
                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            });

        public IEnumerable<ProductViewModel> GetProductsWithSearchString(string searchString) =>
            _productManager.GetProductsWithStockWithCondition(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer,
                Seller = product.Seller,
                Category = product.Category,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(),
                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Name.StandardSearch(searchString.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetProductsWithSearchStringAndPagination(int pageNumber, int pageSize, string searchString) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer,
                Seller = product.Seller,
                Category = product.Category,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(),
                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Name.StandardSearch(searchString.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProducts(string shop) =>
            _productManager.GetProductsWithStockWithCondition(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer,
                Seller = product.Seller,
                Category = product.Category,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(),
                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Seller.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProductsWithPagination(int pageNumber, int pageSize, string shop) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer,
                Seller = product.Seller,
                Category = product.Category,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(),
                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Seller.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProductsWithSearchString(string shop, string searchString) =>
            _productManager.GetProductsWithStockWithCondition(product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer,
                Seller = product.Seller,
                Category = product.Category,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(),
                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Seller.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) &&
               product.Name.StandardSearch(searchString.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProductsWithSearchStringAndPagination(int pageNumber, int pageSize, string shop, string searchString) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer,
                Seller = product.Seller,
                Category = product.Category,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(),
                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Seller.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) &&
               product.Name.StandardSearch(searchString.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProductsWithPagination(int pageNumber, int pageSize, string shop, string searchString) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, product => new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer,
                Seller = product.Seller,
                Category = product.Category,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(),
                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Seller.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) &&
               product.Name.StandardSearch(searchString.NormalizeWithStandardRegex()));

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
