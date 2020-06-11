using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Enums;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext _context;

        public OrderManager(ApplicationDbContext context)
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
                .Include(x => x.OrderStocks)
                    .ThenInclude(x => x.Stock)
                        .ThenInclude(x => x.Product)
                        .AsEnumerable()
                            .Where(x => condition(x))
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

        public IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector)
        {
            return _context.Orders
                .Where(x => x.Status == status)
                .Select(selector)
                .ToList();
        }

        public Task<int> AdvanceOrder(int id)
        {
            _context.Orders.FirstOrDefault(x => x.Id == id).Status++;

            return _context.SaveChangesAsync();
        }
    }
}
