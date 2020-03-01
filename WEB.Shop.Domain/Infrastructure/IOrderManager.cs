using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Shop.Domain.Enums;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface IOrderManager
    {
        bool OrderReferenceExists(string reference);
   
        IEnumerable<TResult> GetOrdersByStatus<TResult>(OrderStatus status, Func<Order, TResult> selector);
        TResult GetOrderByReference<TResult>(string reference, Func<Order, TResult> selector);
        TResult GetOrderById<TResult>(int id, Func<Order, TResult> selector);
        Task<int> CreateOrder(Order order);
        Task<int> AdvanceOrder(int id);
    } 
}
