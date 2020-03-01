using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface IStockManager
    {
        Task<int> CreateStock(Stock stock);
        Task<int> UpdateStockRange(List<Stock> stockList);
        Task<int> DeleteStock(int id);

        Stock GetStockWithProduct(int stockId);
        Task RemoveStockFromHold(int stockId, int quantity, string sessionId);
        Task RemoveStockFromHold(string sessionId);
        Task RetrieveExpiredStockOnHold();
    }
}
