using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class StockManager : IStockManager
    {
        private readonly ApplicationDbContext _context;

        public StockManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CreateStock(Stock stock)
        {
            _context.Stock.Add(stock);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteStock(int id)
        {
            var stock = _context.Stock.FirstOrDefault(x => x.Id == id);
            _context.Stock.Remove(stock);
            return _context.SaveChangesAsync();
        }

        public Task<int> UpdateStockRange(List<Stock> stockList)
        {
            _context.Stock.UpdateRange(stockList);
            return _context.SaveChangesAsync();
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

        public Task RetrieveExpiredStockOnHold()
        {
            var stockOnHold = _context.StocksOnHold
                .Where(x => x.ExpiryDate < DateTime.Now)
                .ToList();

            if (stockOnHold.Count > 0)
            {
                var stockToReturn = _context.Stock
                    .Where(x => stockOnHold.Any(y => y.StockId == x.Id))
                    .ToList();

                foreach (var stock in stockToReturn)
                {
                    stock.Quantity += stockOnHold
                        .FirstOrDefault(x => x.StockId == stock.Id)
                        .Quantity;
                }

                _context.StocksOnHold
                    .RemoveRange(stockOnHold);
                return _context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

    }
}
