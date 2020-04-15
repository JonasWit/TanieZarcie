using System;
using System.Collections.Generic;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface ISessionManager
    {
        string GetId();
        void AddProduct(CartProduct cartProduct);
        void RemoveProduct(int stockId, int quantity);
        IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector);
        void AddCustomerInformation(CustomerInformation customerInformation);
        CustomerInformation GetCustomerInformation();
        void ClearCart();
    }
}
