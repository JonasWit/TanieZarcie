using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Cart
{
    [TransientService]
    public class AddToCart
    {
        private readonly ISessionManager _sessionManager;
        private readonly IProductManager _productManager;

        public AddToCart(ISessionManager sessionManager, IProductManager productManager)
        {
            _sessionManager = sessionManager;
            _productManager = productManager;
        }

        public class Request
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        public async Task<bool> DoAsync(Request request)
        {
            #region Stock Race

            //var stockToHold = _context.Stock.Where(x => x.Id == request.StockId).FirstOrDefault();
            //if (stockToHold.Quantity < request.Quantity)
            //{
            //    return false;
            //}

            //var stockOnHold = _context.StocksOnHold.Where(x => x.SessionId == _sessionManager.GetId()).ToList();
            //if (stockOnHold.Any(x => x.StockId == request.StockId))
            //{
            //    stockOnHold.Find(x => x.StockId == request.StockId).Quantity += request.Quantity;
            //}
            //else
            //{
            //    _context.StocksOnHold.Add(new StockOnHold
            //    {
            //        StockId = request.StockId,
            //        SessionId = _sessionManager.GetId(),
            //        Quantity = request.Quantity,
            //        ExpiryDate = DateTime.Now.AddMinutes(20)
            //    });
            //}

            //foreach (var stock in stockOnHold)
            //{
            //    stock.ExpiryDate = DateTime.Now.AddMinutes(20);
            //}

            //stockToHold.Quantity = stockToHold.Quantity - request.Quantity;

            //await _context.SaveChangesAsync();

            #endregion

            var product = _productManager.GetProductById(request.ProductId);

            var cartPorduct = new CartProduct()
            {
                ProductId = product.Id,
                StockId = product.Id,
                Quantity = request.Quantity,
                ProductName = product.Name,
                Description = product.Description,
                Seller = product.Distributor.DistributorName,
                Category = product.Category.CategoryName,
                Producer = product.Producer.ProducerName,
                SourceUrl = product.SourceUrl,
                Value = product.Value
            };

            await Task.Run(() => _sessionManager.AddProduct(cartPorduct));

            return true;
        }
    }
}
