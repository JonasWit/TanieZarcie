using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.StockAdmin
{
    [TransientService]
    public class GetStocks
    {
        private IProductManager _productManager;

        public GetStocks(IProductManager productManager)
        {
            _productManager = productManager;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            return _productManager.GetProductsWithStock(product => 
                new ProductViewModel
                {
                    Id = product.Id,
                    Description = product.Description,
                    Stock = product.Stock.Select(stock => new StockViewModel
                    {
                        Id = stock.Id,
                        Description = stock.Description,
                        Quantity = stock.Quantity,
                    })
                });
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

    }
}
