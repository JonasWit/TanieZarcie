﻿using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Cart
{
    [Service]
    public class RemoveFromCart
    {
        private ISessionManager _sessionManager;
        private IStockManager _stockManager;

        public RemoveFromCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
            public bool All { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            if (request.Quantity <= 0)
            {
                return false;
            }

            //await _stockManager
            //    .RemoveStockFromHold(request.StockId, request.Quantity, _sessionManager.GetId());

            _sessionManager.RemoveProduct(request.StockId, request.Quantity);

            return true;
        }
    }
}
