using System;
using System.Collections.Generic;
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
        public string CurrentAction { get; private set; }

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

        public async Task<int> ClearDataBaseAsync()
        {
            return await _crawlersDataBaseManager.ClearDataBaseAsync();
        }

        public async Task<int> RunEngineAsync()
        {
            await _searchEngine.RunAllCrawlersAsync();

            ConvertSearchModelsToDomainModels();

            return Results.Count;
        }

        public async Task<int> RunBiedronkaEngineAsync()
        {
            CurrentAction = "Clearing records for Biedronka";
            await _crawlersDataBaseManager.DeleteProductFromShops("Biedronka");

            CurrentAction = "Running crawler for Biedronka";
            await _searchEngine.RunCrawlerForBiedronkaAsync();

            CurrentAction = "Converting models";
            ConvertSearchModelsToDomainModels();

            DataCache[Shops.Biedronka.ToString()] = Results.Count;
            
            return Results.Count;
        }

        public async Task<int> RunKauflandEngineAsync()
        {
            CurrentAction = "Clearing records for Kaufland";
            await _crawlersDataBaseManager.DeleteProductFromShops("Kaufland");

            CurrentAction = "Running crawler for Kaufland";
            await _searchEngine.RunCrawlerForKauflandAsync();

            CurrentAction = "Converting models";
            ConvertSearchModelsToDomainModels();

            DataCache[Shops.Kaufland.ToString()] = Results.Count;
            
            return Results.Count;
        }

        public async Task<int> UpdateDataBase()
        {
            await ClearDataBaseAsync();
            return await _crawlersDataBaseManager.RefreshDatabaseAsync(Results);
        }

        public async Task CheckDataBase()
        {
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
