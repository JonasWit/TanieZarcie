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

        public GetProducts(IProductManager productManager) => _productManager = productManager;

        public int CountProductForShop(string shop) =>
            _productManager.CountProductsWithStockWithCondition(product => product.Distributor.DistributorName.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()));

        public int CountProductOnSaleForShop(string shop) =>
            _productManager.CountProductsWithStockWithCondition(product => product.Distributor.DistributorName.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) && product.OnSale);

        public int CountAllProducts() => _productManager.CountAllProductsWithStock();

        public int CountAllProductsWithSearchString(string searchString) =>
            _productManager.CountProductsWithStockWithCondition(product => product.Name.StandardSearch(searchString));

        public int CountProductsForShopWithSearchString(string shop, string searchString) =>
            _productManager.CountProductsWithStockWithCondition(product => product.Distributor.DistributorName.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) && product.Name.StandardSearch(searchString));

        public IEnumerable<ProductViewModel> GetAllProducts() =>
            _productManager.GetProductsWithStock(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(false),

                OnSale = product.OnSale,
                SaleValue = product.SaleValue.MonetaryValue(false),
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                Discount = product.OnSale && product.SaleValue != 0 ? Math.Round(1 - (product.Value / product.SaleValue),2) : 0,

                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            });

        public IEnumerable<ProductViewModel> GetAllProductsWithPagination(int pageNumber, int pageSize) =>
            _productManager.GetProductsWithStockWithPagination(pageNumber, pageSize, product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(false),

                OnSale = product.OnSale,
                SaleValue = product.SaleValue.MonetaryValue(false),
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                Discount = product.OnSale && product.SaleValue != 0 ? Math.Round(1 - (product.Value / product.SaleValue),2) : 0,

                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            });

        public IEnumerable<ProductViewModel> GetProductsWithSearchString(string searchString) =>
            _productManager.GetProductsWithStockWithCondition(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(false),

                OnSale = product.OnSale,
                SaleValue = product.SaleValue.MonetaryValue(false),
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                Discount = product.OnSale && product.SaleValue != 0 ? Math.Round(1 - (product.Value / product.SaleValue),2) : 0,

                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Name.StandardSearch(searchString));

        public IEnumerable<ProductViewModel> GetProductsWithSearchStringAndPagination(int pageNumber, int pageSize, string searchString) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(false),

                OnSale = product.OnSale,
                SaleValue = product.SaleValue.MonetaryValue(false),
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                Discount = product.OnSale && product.SaleValue != 0 ? Math.Round(1 - (product.Value / product.SaleValue),2) : 0,

                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Name.StandardSearch(searchString));

        public IEnumerable<ProductViewModel> GetShopProducts(string shop) =>
            _productManager.GetProductsWithStockWithCondition(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(false),

                OnSale = product.OnSale,
                SaleValue = product.SaleValue.MonetaryValue(false),
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                Discount = product.OnSale && product.SaleValue != 0 ? Math.Round(1 - (product.Value / product.SaleValue),2) : 0,

                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Distributor.DistributorName.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProductsWithPagination(int pageNumber, int pageSize, string shop) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(false),

                OnSale = product.OnSale,
                SaleValue = product.SaleValue.MonetaryValue(false),
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                Discount = product.OnSale && product.SaleValue != 0 ? Math.Round(1 - (product.Value / product.SaleValue),2) : 0,

                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Distributor.DistributorName.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()));

        public IEnumerable<ProductViewModel> GetShopProductsWithSearchString(string shop, string searchString) =>
            _productManager.GetProductsWithStockWithCondition(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(false),

                OnSale = product.OnSale,
                SaleValue = product.SaleValue.MonetaryValue(false),
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                Discount = product.OnSale && product.SaleValue != 0 ? Math.Round(1 - (product.Value / product.SaleValue), 2) : 0,

                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Distributor.DistributorName.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) &&
               product.Name.StandardSearch(searchString));

        public IEnumerable<ProductViewModel> GetShopProductsWithSearchStringAndPagination(int pageNumber, int pageSize, string shop, string searchString) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(false),

                OnSale = product.OnSale,
                SaleValue = product.SaleValue.MonetaryValue(false),
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                Discount = product.OnSale && product.SaleValue != 0 ? Math.Round(1 - (product.Value / product.SaleValue),2) : 0,

                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Distributor.DistributorName.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) &&
               product.Name.StandardSearch(searchString));

        public IEnumerable<ProductViewModel> GetShopProductsWithPagination(int pageNumber, int pageSize, string shop, string searchString) =>
            _productManager.GetProductsWithStockWithPaginationAndCondition(pageNumber, pageSize, product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer.ProducerName,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(false),

                OnSale = product.OnSale,
                SaleValue = product.SaleValue.MonetaryValue(false),
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                Discount = product.OnSale && product.SaleValue != 0 ? Math.Round(1 - (product.Value / product.SaleValue),2) : 0,

                StockCount = product.Stock.Sum(stock => stock.Quantity),
                TimeStamp = product.TimeStamp
            }, product => product.Distributor.DistributorName.NormalizeWithStandardRegex().Equals(shop.NormalizeWithStandardRegex()) &&
               product.Name.StandardSearch(searchString));

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Producer { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public string SourceUrl { get; set; }
            public string Value { get; set; }

            public bool OnSale { get; set; }
            public string SaleValue { get; set; }
            public decimal Discount { get; set; }
            public string SaleDescription { get; set; }
            public DateTime SaleDeadline { get; set; }

            public int StockCount { get; set; }
            public DateTime TimeStamp { get; set; }
        }
    }
}
