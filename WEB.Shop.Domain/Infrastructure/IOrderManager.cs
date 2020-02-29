using System;
using System.Threading.Tasks;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface IOrderManager
    {
        bool OrderReferenceExists(string reference);
        TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector);
        TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector);
        Task<int> CreateOrder(Order order);
    }
}
