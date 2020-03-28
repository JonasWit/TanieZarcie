using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.UI.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private const string KeyCart = "Cart";
        private const string KeySearchString = "SearchString";
        private const string KeySelectedShop = "SelectedShop";
        private const string KeyCurrentPage = "KeyCurrentPage";
        private const string KeyCustomerInfo = "Customer-info";
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void AddCustomerInformation(CustomerInformation customerInformation)
        {
            var stringObject = JsonConvert.SerializeObject(customerInformation);
            _session.SetString(KeyCustomerInfo, stringObject);
        }

        public void AddProduct(CartProduct cartProduct)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString(KeyCart);

            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if (cartList.Any(x => x.StockId == cartProduct.StockId))
            {
                cartList.Find(x => x.StockId == cartProduct.StockId).Quantity += cartProduct.Quantity;
            }
            else
            {
                cartList.Add(cartProduct);
            }

            stringObject = JsonConvert.SerializeObject(cartList);
            _session.SetString(KeyCart, stringObject);
        }

        public void ClearCart()
        {
            _session.Remove(KeyCart);
        }

        public IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector)
        {
            var stringObject = _session.GetString(KeyCart);

            if (string.IsNullOrEmpty(stringObject))
            {
                return new List<TResult>();
            }

            var cartList = JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(stringObject);

            return cartList.Select(selector);
        }

        public CustomerInformation GetCustomerInformation()
        {
            var stringObject = _session.GetString(KeyCustomerInfo);

            if (string.IsNullOrEmpty(stringObject))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CustomerInformation>(stringObject);
        }

        public string GetId() => _session.Id;

        public void RemoveProduct(int stockId, int quantity)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString(KeyCart);

            if (string.IsNullOrEmpty(stringObject)) return;

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == stockId)) return;

            var product = cartList.First(x => x.StockId == stockId);
            product.Quantity -= quantity;

            if (product.Quantity <= 0)
            {
                cartList.Remove(product);
            }

            stringObject = JsonConvert.SerializeObject(cartList);
            _session.SetString(KeyCart, stringObject);
        }

        public void SaveSearchString(string input)
        {
            if (_session.Keys.Contains(KeySearchString))
            {
                _session.Remove(KeySearchString);
            }

            if (!string.IsNullOrEmpty(input))
            {
                _session.SetString(KeySearchString, input);
            }
        }        
        
        public bool GetSearchString(out string output)
        {
            if (_session.Keys.Contains(KeySearchString))
            {
                output = _session.GetString(KeySearchString);
                return true;
            }
            else
            {
                output = "";
                return false;
            }
        }

        public void SaveSelectedShop(string shop)
        {
            if (_session.Keys.Contains(KeySelectedShop))
            {
                _session.Remove(KeySelectedShop);
            }

            if (!string.IsNullOrEmpty(shop))
            {
                _session.SetString(KeySelectedShop, shop);
            }
        }

        public bool GetSelectedShop(out string output)
        {
            if (_session.Keys.Contains(KeySelectedShop))
            {
                output = _session.GetString(KeySelectedShop);
                return true;
            }
            else
            {
                output = "";
                return false;
            }
        }

        public void SaveCurrentPage(int input)
        {
            if (_session.Keys.Contains(KeyCurrentPage))
            {
                _session.Remove(KeyCurrentPage);
            }

            _session.SetString(KeyCurrentPage, input.ToString());
        }

        public bool GetCurrentPage(out int output)
        {
            if (_session.Keys.Contains(KeyCurrentPage))
            {
                if (int.TryParse(_session.GetString(KeyCurrentPage), out int result))
                {
                    output = result;
                    return true;
                }
                else
                {
                    output = 0;
                    return false;
                }
            }

            output = 0;
            return false;
        }
    }
}
