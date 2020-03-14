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
        private ICrawlersDataBaseManager _crawlersDataBaseManager;
        private Engine _searchEngine;

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

        #region Database Cleanup

        public async Task<int> ClearDataBaseAsync()
        {
            return await _crawlersDataBaseManager.ClearDataBaseAsync();
        }

        public async Task<int> ClearKauflandDataAsync()
        {
            return await _crawlersDataBaseManager.DeleteProductFromShops("Kaufland");
        }
        public async Task<int> ClearBiedronkaDataAsync()
        {
            return await _crawlersDataBaseManager.DeleteProductFromShops("Biedronka");
        }

        #endregion

        #region Crawlers

        public async Task<int> RunEnginesAsync()
        {
            await _searchEngine.RunAllCrawlersAsync();
            ConvertSearchModelsToDomainModels();
            return Results.Count;
        }

        public async Task<int> RunBiedronkaEngineAsync()
        {
            await _searchEngine.RunCrawlerForBiedronkaAsync();
            ConvertSearchModelsToDomainModels();
            DataCache[Shops.Biedronka.ToString()] = Results.Count;
            return Results.Count;
        }

        public async Task<int> RunKauflandEngineAsync()
        {
            await _searchEngine.RunCrawlerForKauflandAsync();
            ConvertSearchModelsToDomainModels();
            DataCache[Shops.Kaufland.ToString()] = Results.Count;
            return Results.Count;
        }

        public async Task<int> ClearCacheFullAsync()
        {
            await Task.Run(() => Results.Clear());
            return Results.Count;
        }

        public async Task<int> ClearCacheBiedronkaEngineAsync()
        {
            await Task.Run(() => Results.RemoveAll(p => p.Seller == "Biedronka"));
            return Results.Count;
        }

        public async Task<int> ClearCacheKauflandEngineAsync()
        {
            await Task.Run(() => Results.RemoveAll(p => p.Seller == "Kaufland"));
            return Results.Count;
        }

        #endregion

        #region Database Update
        public async Task<int> UpdateAllData()
        {
            await ClearDataBaseAsync();
            return await _crawlersDataBaseManager.UpdateDatabaseAsync(Results);
        }

        public async Task<int> UpdateBiedronkaBase()
        {
            await ClearBiedronkaDataAsync();
            return await _crawlersDataBaseManager.UpdateDatabaseAsync(Results.Where(p => p.Seller == "Biedronka").ToList());
        }

        public async Task<int> UpdateKauflandBase()
        {
            await ClearKauflandDataAsync();
            return await _crawlersDataBaseManager.UpdateDatabaseAsync(Results.Where(p => p.Seller == "Kaufland").ToList());
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
    }
}
