using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class StockManager : IStockManager
    {
        private ApplicationDbContext _context;

        public StockManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Stock GetStockWithProduct(int stockId)
        {
            return _context.Stock
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == stockId);
        }

        public Task RemoveStockFromHold(int stockId, int quantity, string sessionId)
        {
            var stockOnHold = _context
                .StocksOnHold
                .FirstOrDefault(x => x.StockId == stockId
                && x.SessionId == sessionId);

            var stock = _context.Stock.FirstOrDefault(x => x.Id == stockId);

            stock.Quantity += quantity;
            stockOnHold.Quantity -= quantity;

            if (stockOnHold.Quantity <= 0)
            {
                _context.Remove(stockOnHold);
            }

            return _context.SaveChangesAsync();
        }

        public Task RemoveStockFromHold(string sessionId)
        {
            var stockOnHold = _context.StocksOnHold
                .Where(x => x.SessionId == sessionId)
                .ToList();

            _context.StocksOnHold.RemoveRange(stockOnHold);
            return _context.SaveChangesAsync();
        }
    }
}
