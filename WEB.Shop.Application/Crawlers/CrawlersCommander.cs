using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.SearchEngine;
using WEB.SearchEngine.Enums;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Crawlers
{
    public class CrawlersCommander
    {
        private readonly ICrawlersDataBaseManager _crawlersDataBaseManager;
        private readonly Engine _searchEngine;

        public List<Product> Results { get; private set; }
        public Dictionary<string, int> DataCache { get; set; }
        public List<SearchEngine.SearchResultsModels.Product> EngineModels { get; private set; }
        public List<DataBaseSummary> DataBaseCheck { get; set; } = new List<DataBaseSummary>();

        public class DataBaseSummary
        {
            public string Shop { get; set; }
            public int ProductsCount { get; set; }
            public DateTime OldestEntry { get; set; }
        }

        public CrawlersCommander(ICrawlersDataBaseManager crawlersDataBaseManager)
        {
            _crawlersDataBaseManager = crawlersDataBaseManager;
            _searchEngine = new Engine();

            DataCache = new Dictionary<string, int>();

            foreach (var item in Enum.GetValues(typeof(Shops)))
            {
                DataCache.Add(item.ToString(), 0);
            }
        }

        #region Crawlers

        public async Task<int> RunEngineAsync()
        {
            await _searchEngine.RunAllCrawlersAsync();
            ConvertSearchModelsToDomainModels();

            DataCache[Shops.Biedronka.ToString()] = Results.Where(p => p.Seller == Shops.Biedronka.ToString()).Count();
            DataCache[Shops.Kaufland.ToString()] = Results.Where(p => p.Seller == Shops.Kaufland.ToString()).Count();
            DataCache[Shops.Lidl.ToString()] = Results.Where(p => p.Seller == Shops.Lidl.ToString()).Count();
            return Results.Count;
        }

        public async Task<int> RunEngineAsync(string shop)
        {
            var shopEnum = (Shops)Enum.Parse(typeof(Shops), shop, true);

            await _searchEngine.RunCrawlerForSpecificShopAsync(shopEnum);
            ConvertSearchModelsToDomainModels();

            DataCache[shopEnum.ToString()] = Results.Count;
            return Results.Count;
        }

        public async Task<int> ClearCacheAsync()
        {
            await Task.Run(() => Results.Clear());
            return Results.Count;
        }

        public async Task<int> ClearCacheAsync(string shop)
        {
            var shopEnum = (Shops)Enum.Parse(typeof(Shops), shop, true);

            await Task.Run(() => Results.RemoveAll(p => p.Seller == shopEnum.ToString()));
            return Results.Count;
        }

        private void ConvertSearchModelsToDomainModels()
        {
            Results = new List<Product>();

            foreach (var crawler in _searchEngine.Crawlers)
            {
                foreach (var product in crawler.Products)
                {
                    Results.Add(new Product
                    {
                        Name = product.Name,
                        Description = product.Description,
                        Category = product.Category,
                        Producer = product.Producer,
                        Seller = product.Seller,
                        SourceUrl = product.SourceUrl,
                        TimeStamp = product.TimeStamp,
                        Value = product.Value
                    });
                }
            }
        }

        #endregion

        #region Database Update

        public async Task UpdateAllData()
        {
            await ClearDataBaseAsync();
            await _crawlersDataBaseManager.UpdateDatabaseAsync(Results);
            await CheckDataBase();
        }

        public async Task UpdateDataBase(string shop)
        {
            var shopEnum = (Shops)Enum.Parse(typeof(Shops), shop, true);

            switch (shopEnum)
            {
                case Shops.Biedronka:
                    await ClearDataBaseAsync(shopEnum.ToString());
                    break;
                case Shops.Lidl:
                    break;
                case Shops.Kaufland:
                    await ClearDataBaseAsync(shopEnum.ToString());
                    break;
                case Shops.Carrefour:
                    break;
                case Shops.Auchan:
                    break;
                case Shops.Stokrotka:
                    break;
                case Shops.Zabka:
                    break;
                default:
                    break;
            }

            await _crawlersDataBaseManager.UpdateDatabaseAsync(Results.Where(p => p.Seller == shopEnum.ToString()).ToList());
            await CheckDataBase();
        }

        #endregion

        #region Database Cleanup

        public async Task ClearDataBaseAsync()
        {
            await _crawlersDataBaseManager.ClearDataBaseAsync();
            await CheckDataBase();
        }

        public async Task ClearDataBaseAsync(string shop)
        {
            var shopEnum = (Shops)Enum.Parse(typeof(Shops), shop, true);

            await _crawlersDataBaseManager.DeleteProductFromShops(shopEnum.ToString());
            await CheckDataBase();
        }

        #endregion

        public async Task CheckDataBase()
        {
            DataBaseCheck = new List<DataBaseSummary>();

            var check = await Task.Run(() => _crawlersDataBaseManager.CheckDataBase());

            foreach (var item in check)
            {
                DataBaseCheck.Add(new DataBaseSummary
                {
                    Shop = item.Item1,
                    ProductsCount = item.Item2,
                    OldestEntry = item.Item3
                });
            }
        }


    }
}
