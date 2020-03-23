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

        public IEnumerable<ProductViewModel> Do(Dictionary<LookupCriteria, string> lookupParameters)
        {
            if (lookupParameters.Keys.Count == 0)
            {
                return _productManager.GetProductsWithStock(x => new ProductViewModel
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
            }

            if (lookupParameters.Keys.Contains(LookupCriteria.SearchString) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.PageNumber) == false &&
                lookupParameters.Keys.Contains(LookupCriteria.PageSize) == false &&
                lookupParameters.Keys.Contains(LookupCriteria.Shop) == false)
            {
                return _productManager.GetProductsWithStockSearchString(lookupParameters[LookupCriteria.SearchString], x => new ProductViewModel
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
            }

            if (lookupParameters.Keys.Contains(LookupCriteria.SearchString) == false &&
                lookupParameters.Keys.Contains(LookupCriteria.PageNumber) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.PageSize) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.Shop) == false)
            {
                return _productManager.GetProductsWithStockPagination(int.Parse(lookupParameters[LookupCriteria.PageNumber]), int.Parse(lookupParameters[LookupCriteria.PageSize]), x => new ProductViewModel
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
            }

            if (lookupParameters.Keys.Contains(LookupCriteria.SearchString) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.PageNumber) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.PageSize) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.Shop) == false)
            {
                return _productManager.GetProductsWithStockPaginationSearchString(int.Parse(lookupParameters[LookupCriteria.PageNumber]), int.Parse(lookupParameters[LookupCriteria.PageSize]), lookupParameters[LookupCriteria.SearchString], x => new ProductViewModel
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
            }

            if (lookupParameters.Keys.Contains(LookupCriteria.SearchString) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.PageNumber) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.PageSize) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.Shop) == true)
            {
                return _productManager.GetProductsWithStockPaginationSearchStringShop(
                    int.Parse(lookupParameters[LookupCriteria.PageNumber]), int.Parse(lookupParameters[LookupCriteria.PageSize]), lookupParameters[LookupCriteria.SearchString], lookupParameters[LookupCriteria.Shop], x => new ProductViewModel
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
            }

            if (lookupParameters.Keys.Contains(LookupCriteria.SearchString) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.PageNumber) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.PageSize) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.Shop) == true)
            {
                return _productManager.GetProductsWithStockSearchStringShop(lookupParameters[LookupCriteria.SearchString], lookupParameters[LookupCriteria.Shop], x => new ProductViewModel
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
            }

            if (lookupParameters.Keys.Contains(LookupCriteria.SearchString) == false &&
                lookupParameters.Keys.Contains(LookupCriteria.PageNumber) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.PageSize) == true &&
                lookupParameters.Keys.Contains(LookupCriteria.Shop) == true)
            {
                return _productManager.GetProductsWithStockPaginationShop(int.Parse(lookupParameters[LookupCriteria.PageNumber]), int.Parse(lookupParameters[LookupCriteria.PageSize]), lookupParameters[LookupCriteria.Shop], x => new ProductViewModel
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
            }

            return new List<ProductViewModel>();
        }

        //public IEnumerable<ProductViewModel> Do() =>
        //    _productManager.GetProductsWithStock(x => new ProductViewModel
        //    {
        //        Name = x.Name,
        //        Description = x.Description,
        //        Producer = x.Producer,
        //        Seller = x.Seller,
        //        Category = x.Category,
        //        SourceUrl = x.SourceUrl,
        //        Value = x.Value.MonetaryValue(),
        //        StockCount = x.Stock.Sum(y => y.Quantity),
        //        TimeStamp = x.TimeStamp
        //    });

        //public IEnumerable<ProductViewModel> Do(string searchString) =>
        //    _productManager.GetProductsWithStockSearchString(searchString, x => new ProductViewModel
        //     {
        //         Name = x.Name,
        //         Description = x.Description,
        //         Producer = x.Producer,
        //         Seller = x.Seller,
        //         Category = x.Category,
        //         SourceUrl = x.SourceUrl,
        //         Value = x.Value.MonetaryValue(),
        //         StockCount = x.Stock.Sum(y => y.Quantity),
        //         TimeStamp = x.TimeStamp
        //     });

        //public IEnumerable<ProductViewModel> Do(int pageNumber, int pageSize) =>
        //    _productManager.GetProductsWithStockPagination(pageNumber, pageSize, x => new ProductViewModel
        //    {
        //        Name = x.Name,
        //        Description = x.Description,
        //        Producer = x.Producer,
        //        Seller = x.Seller,
        //        Category = x.Category,
        //        SourceUrl = x.SourceUrl,
        //        Value = x.Value.MonetaryValue(),
        //        StockCount = x.Stock.Sum(y => y.Quantity),
        //        TimeStamp = x.TimeStamp
        //    });

        //public IEnumerable<ProductViewModel> Do(int pageNumber, int pageSize, string searchString) =>
        //    _productManager.GetProductsWithStockPaginationSearchString(pageNumber, pageSize, searchString,  x => new ProductViewModel
        //    {
        //        Name = x.Name,
        //        Description = x.Description,
        //        Producer = x.Producer,
        //        Seller = x.Seller,
        //        Category = x.Category,
        //        SourceUrl = x.SourceUrl,
        //        Value = x.Value.MonetaryValue(),
        //        StockCount = x.Stock.Sum(y => y.Quantity),
        //        TimeStamp = x.TimeStamp
        //    });

        //public IEnumerable<ProductViewModel> Do(int pageNumber, int pageSize, string searchString, string shop) =>
        //    _productManager.GetProductsWithStockPaginationSearchStringShop(pageNumber, pageSize, searchString, shop, x => new ProductViewModel
        //    {
        //        Name = x.Name,
        //        Description = x.Description,
        //        Producer = x.Producer,
        //        Seller = x.Seller,
        //        Category = x.Category,
        //        SourceUrl = x.SourceUrl,
        //        Value = x.Value.MonetaryValue(),
        //        StockCount = x.Stock.Sum(y => y.Quantity),
        //        TimeStamp = x.TimeStamp
        //    });


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
