using System.Threading.Tasks;
using WEB.Shop.Application.Infrastructure;
using WEB.Shop.DataBase;

namespace WEB.Shop.Application.Cart
{
    public class AddToCart
    {
        private ISessionManager _sessionManager;
        private ApplicationDbContext _context;

        public AddToCart(ISessionManager sessionManager, ApplicationDbContext context)
        {
            _sessionManager = sessionManager;
            _context = context;
        }

        public async Task<bool> Do(Request request)
        {
            #region Stock Race

            //var stockOnHold = _context.StocksOnHold.Where(x => x.SessionId == _session.Id).ToList();
            //var stockToHold = _context.Stock.Where(x => x.Id == request.StockId).FirstOrDefault();

            //if (stockToHold.Quantity >= request.Quantity)
            //{
            //    return false;
            //}

            //if (stockOnHold.Any(x => x.StockId == request.StockId))
            //{
            //    stockOnHold.Find(x => x.StockId == request.StockId).Quantity += request.Quantity;
            //}
            //else 
            //{
            //    _context.StocksOnHold.Add(new StockOnHold
            //    {
            //        StockId = stockToHold.Id,
            //        SessionId = _session.Id,
            //        Quantity = request.Quantity,
            //        ExpiryDate = DateTime.Now.AddMinutes(20)
            //    });
            //}

            //stockToHold.Quantity = stockToHold.Quantity - request.Quantity;

            //foreach (var stock in stockOnHold)
            //{
            //    stock.ExpiryDate = DateTime.Now.AddMinutes(20);
            //}

            //await _context.SaveChangesAsync();

            #endregion

            _sessionManager.AddProduct(request.StockId, request.Quantity);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
