using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class OderManager : IOrderManager
    {
        private ApplicationDbContext _context;

        public OderManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            return _context.SaveChangesAsync();
        }

        private TResult GetOrder<TResult>(Func<Order, bool> condition, Func<Order, TResult> selector)
        {
            return _context.Orders
                .Where(x => condition(x))
                .Include(x => x.OrderStocks)
                    .ThenInclude(x => x.Stock)
                        .ThenInclude(x => x.Product)
                .Select(selector)
                .FirstOrDefault();
        }

        public TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector)
        {
            return GetOrder(order => order.Id == id, selector);
        }

        public TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector)
        {
            return GetOrder(order => order.OrderReference == reference, selector);
        }

        public bool OrderReferenceExists(string reference)
        {
            return _context.Orders.Any(x => x.OrderReference == reference);
        }
    }
}
