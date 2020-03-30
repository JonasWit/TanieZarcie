using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.StockAdmin
{
    [TransientService]
    public class DeleteStock
    {
        private IStockManager _stockManager;

        public DeleteStock(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        public Task<int> Do(int id) => _stockManager.DeleteStock(id);

    }
}
