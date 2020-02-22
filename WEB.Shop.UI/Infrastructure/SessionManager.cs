using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Application.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCustomerInformation(CustomerInformation customerInformation)
        {
            var stringObject = JsonConvert.SerializeObject(customerInformation);
            _session.SetString("Customer-info", stringObject);
        }

        public void AddProduct(int stockId, int quantity)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("Cart");

            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if (cartList.Any(x => x.StockId == stockId))
            {
                cartList.Find(x => x.StockId == stockId).Quantity += quantity;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = stockId,
                    Quantity = quantity
                });
            }

            stringObject = JsonConvert.SerializeObject(cartList);
            _session.SetString("Cart", stringObject);
        }

        public List<CartProduct> GetCart()
        {
            var stringObject = _session.GetString("Cart");

            if (string.IsNullOrEmpty(stringObject))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
        }

        public CustomerInformation GetCustomerInformation()
        {
            var stringObject = _session.GetString("Customer-info");

            if (string.IsNullOrEmpty(stringObject))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CustomerInformation>(stringObject);
        }

        public string GetId() => _session.Id;

        public void RemoveProduct(int stockId, int quantity, bool all)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("Cart");

            if (string.IsNullOrEmpty(stringObject)) return;

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == stockId)) return;

            var product = cartList.First(x => x.StockId == stockId);

            if (product.Quantity <= 0)
            {
                cartList.Remove(product);
                return;
            }

            if (all)
            {
                cartList.Remove(product);
            }
            else
            {
                cartList.Find(x => x.StockId == stockId).Quantity -= quantity;
            }

            stringObject = JsonConvert.SerializeObject(cartList);
            _session.SetString("Cart", stringObject);

        }
    }
}
