using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WEB.Shop.DataBase;
using WEB.Shop.Domain.Enums;

namespace WEB.Shop.Application.OrdersAdmin
{
    public class GetOrders
    {
        private ApplicationDbContext _context;

        public GetOrders(ApplicationDbContext context)
        {
            _context = context;
        }

        public class Response
        {
            public int Id { get; set; }
            public string OrderRef { get; set; }
            public string Email { get; set; }
        }

        public IEnumerable<Response> Do(int status) =>
            _context.Orders
                .Where(x => x.Status == (OrderStatus)status)
                .Select(x => new Response
                {
                    Id =x.Id,
                    OrderRef = x.OrderReference,
                    Email = x.Email
                })
                .ToList();

    }
}
