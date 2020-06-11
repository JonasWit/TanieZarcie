using System;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.ShopAdmin
{
    public class AddUrl
    {
        private readonly IShopManager _shopManager;
        public AddUrl(IShopManager shopManager) => _shopManager = shopManager;

        public async Task<Response> DoAsync(Request request)
        {
            var promoUrl = new PromoSheetUrl
            {
                ShopId = request.ShopId,
                Url = request.Url
            };

            if (await _shopManager.AddUrl(promoUrl) <= 0)
            {
                throw new Exception("Failed to add promo sheets url!");
            }

            return new Response
            {
                Id = promoUrl.Id,
                Url = promoUrl.Url
            };
        }

        public class Request
        {
            public int ShopId { get; set; }
            public string Url { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Url { get; set; }
            public int ShopId { get; set; }
        }
    }
}
