using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Cart
{
    [TransientService]
    public class AddToCart
    {
        private ISessionManager _sessionManager;
        private IStockManager _stockManager;

        public AddToCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public class Request
        {
            public int StockId { get; set; }
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

            var stock = _stockManager.GetStockWithProduct(request.StockId);

            var cartPorduct = new CartProduct()
            {
                ProductId = stock.ProductId,
                StockId = stock.Id,
                Quantity = request.Quantity,
                ProductName = stock.Product.Name,
                Description = stock.Product.Description,
                Seller = stock.Product.Distributor.DistributorName,
                Category = stock.Product.Category.CategoryName,
                Producer = stock.Product.Producer.ProducerName,
                SourceUrl = stock.Product.SourceUrl,
                Value = stock.Product.Value
            };

            _sessionManager.AddProduct(cartPorduct);

            return true;
        }
    }
}
