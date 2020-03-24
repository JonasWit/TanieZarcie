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

        public IEnumerable<ProductViewModel> GetAllProductsWithPagination(int pageNumber, int pageSize) =>
            _productManager.GetProductsWithStockWithPagination(pageNumber, pageSize, x => new ProductViewModel
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

        public IEnumerable<ProductViewModel> GetProductsWithSearchString(string searchString) =>
            _productManager.GetProductsWithStockWithCondition(x => new ProductViewModel
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
            }, x => x.Name.NormalizeWithStandardRegex().Contains(searchString.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetProductsWithSearchStringAndPagination(int pageNumber, int pageSize, string searchString) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, x => new ProductViewModel
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
            }, x => x.Name.NormalizeWithStandardRegex().Contains(searchString.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProducts(string shop) =>
            _productManager.GetProductsWithStockWithCondition(x => new ProductViewModel
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
            }, x => x.Seller.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProductsWithPagination(int pageNumber, int pageSize, string shop) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, x => new ProductViewModel
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
            },
            x => x.Seller.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProductsWithSearchString(string shop, string searchString) =>
            _productManager.GetProductsWithStockWithCondition(x => new ProductViewModel
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
            }, x => x.Seller.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) &&
               x.Name.NormalizeWithStandardRegex().Contains(searchString.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProductsWithSearchStringAndPagination(int pageNumber, int pageSize, string shop, string searchString) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, x => new ProductViewModel
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
            }, x => x.Seller.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) &&
               x.Name.NormalizeWithStandardRegex().Contains(searchString.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProductsWithPagination(int pageNumber, int pageSize, string shop, string searchString) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, x => new ProductViewModel
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
            }, x => x.Seller.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) &&
               x.Name.NormalizeWithStandardRegex().Contains(searchString.NormalizeWithStandardRegex()));

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
