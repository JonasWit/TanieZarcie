using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Extensions;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Products
{
    [TransientService]
    public class GetProduct
    {
        private IStockManager _stockManager;
        private IProductManager _productManager;

        public GetProduct(IStockManager stockManager, IProductManager productManager)
        {
            _stockManager = stockManager;
            _productManager = productManager;
        }

        public async Task<ProductViewModel> DoAsync(string name)
        {
            await _stockManager.RetrieveExpiredStockOnHold();
            return _productManager.GetProductByName(name, product => new ProductViewModel
                {
                    Name = product.Name,
                    Description = product.Description,
                    Producer = product.Producer,
                    Seller = product.Seller,
                    Category = product.Category,
                    SourceUrl = product.SourceUrl,
                    Value = product.Value.MonetaryValue(),

                    OnSale = product.OnSale,
                    SaleValue = product.SaleValue,
                    SaleDescription = product.SaleDescription,
                    SaleDeadline = product.SaleDeadline,

                    Stock = product.Stock.Select(stock => new StockViewModel
                    {
                        Id = stock.Id,
                        Description = stock.Description,
                        Quantity = stock.Quantity,
                        InStock = stock.Quantity > 0
                    })
                });
        }

        public async Task<ProductViewModel> DoAsync(int id)
        {
            await _stockManager.RetrieveExpiredStockOnHold();
            return _productManager.GetProductById(id, product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Producer = product.Producer,
                Seller = product.Seller,
                Category = product.Category,
                SourceUrl = product.SourceUrl,
                Value = product.Value.MonetaryValue(),

                OnSale = product.OnSale,
                SaleValue = product.SaleValue,
                SaleDescription = product.SaleDescription,
                SaleDeadline = product.SaleDeadline,

                Stock = product.Stock.Select(stock => new StockViewModel
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    Quantity = stock.Quantity,
                    InStock = stock.Quantity > 0
                })
            });
        }

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
            public decimal SaleValue { get; set; }
            public string SaleDescription { get; set; }
            public DateTime SaleDeadline { get; set; }

            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public bool InStock { get; set; }
        }
    }
}
