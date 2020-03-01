using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Domain.Enums;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.OrdersAdmin
{
    public class GetOrders
    {
        private IOrderManager _orderManager;

        public GetOrders(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }
            public string Email { get; set; }
        }

        public IEnumerable<Response> Do(int status) =>
            _orderManager.GetOrdersByStatus((OrderStatus)status,
                x => new Response
                {
                    Id = x.Id,
                    OrderRef = x.OrderReference,
                    Email = x.Email
                });
    }
}
