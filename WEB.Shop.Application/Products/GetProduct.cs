using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Products
{
    public class GetProduct
    {
        private IStockManager _stockManager;
        private IProductManager _productManager;

        public GetProduct(IStockManager stockManager, IProductManager productManager)
        {
            _stockManager = stockManager;
            _productManager = productManager;
        }

        public async Task<ProductViewModel> Do(string name)
        {
            await _stockManager.RetrieveExpiredStockOnHold();
            return _productManager.GetProductByName(name, x => new ProductViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Producer = x.Producer,
                    Seller = x.Seller,
                    Category = x.Category,
                    SourceUrl = x.SourceUrl,
                    Value = x.Value,

                    Stock = x.Stock.Select(y => new StockViewModel
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Quantity = y.Quantity,
                        InStock = y.Quantity > 0
                    })
                });
        }

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Producer { get; set; }
            public string Seller { get; set; }
            public string Category { get; set; }
            public string SourceUrl { get; set; }
            public decimal Value { get; set; }

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
