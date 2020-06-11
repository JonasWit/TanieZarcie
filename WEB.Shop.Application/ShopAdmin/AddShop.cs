using System;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.ShopAdmin
{
    public class AddShop
    {
        private readonly IShopManager _shopManager;
        public AddShop(IShopManager shopManager) => _shopManager = shopManager;

        public async Task<Response> DoAsync(Request request)
        {
            var shop = new ShopData
            {
                Name = request.Name
            };

            if (await _shopManager.AddShop(shop) <= 0)
            {
                throw new Exception("Failed to add shop!");
            }

            return new Response
            {
                Id = shop.Id,
                Name = shop.Name
            };
        }

        public class Request
        {
            public string Name { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
