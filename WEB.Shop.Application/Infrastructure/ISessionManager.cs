using System.Collections.Generic;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Infrastructure
{
    public interface ISessionManager
    {
        string GetId();
        void AddProduct(int stockId, int quantity);
        void RemoveProduct(int stockId, int quantity, bool all);
        List<CartProduct> GetCart();
        void AddCustomerInformation(CustomerInformation customerInformation);
        CustomerInformation GetCustomerInformation();

    }
}
