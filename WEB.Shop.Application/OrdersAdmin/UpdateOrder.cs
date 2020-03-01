using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.OrdersAdmin
{
    [Service]
    public class UpdateOrder
    {
        private IOrderManager _orderManager;

        public UpdateOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Task<int> DoAsync(int id)
        {
            return _orderManager.AdvanceOrder(id);
        }
    }
}
