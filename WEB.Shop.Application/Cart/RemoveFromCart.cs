using System.Threading.Tasks;
using WEB.Shop.Application.Infrastructure;
using WEB.Shop.DataBase;

namespace WEB.Shop.Application.Cart
{
    public class RemoveFromCart
    {
        private ISessionManager _sessionManager;
        private ApplicationDbContext _context;

        public RemoveFromCart(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public async Task<bool> Do(Request request)
        {
            _sessionManager.RemoveProduct(request.StockId, request.Quantity, request.All);

            //var stockOnHold = _context.StocksOnHold
            //    .FirstOrDefault(x => x.StockId == request.StockId
            //    && x.SessionId == _sessionManager.GetId());

            //var stock = _context.Stock.FirstOrDefault(x => x.Id == request.StockId);

            //if (request.All)
            //{
            //    stock.Quantity += stockOnHold.Quantity;
            //    stockOnHold.Quantity = 0;
            //}
            //else
            //{
            //    stock.Quantity += stockOnHold.Quantity;
            //    stockOnHold.Quantity -= request.Quantity;
            //}

            //if (stockOnHold.Quantity <= 0)
            //{
            //    _context.Remove(stockOnHold);
            //}

            //await _context.SaveChangesAsync();

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
            public bool All { get; set; }
        }
    }
}
