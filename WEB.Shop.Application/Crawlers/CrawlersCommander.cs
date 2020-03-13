using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.SearchEngine;
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
        public int BiedronkaProducts { get; private set; }
        public int KauflandProducts { get; private set; }
        public List<SearchEngine.SearchResultsModels.Product> EngineModels { get; private set; }

        public CrawlersCommander(ICrawlersDataBaseManager crawlersDataBaseManager)
        {
            _crawlersDataBaseManager = crawlersDataBaseManager;
            _searchEngine = new Engine();
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

            BiedronkaProducts = Results.Count;
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

            KauflandProducts = Results.Count;
            return Results.Count;
        }

        public async Task<int> UpdateDataBase()
        {
            await ClearDataBaseAsync();
            return await _crawlersDataBaseManager.RefreshDatabaseAsync(Results);
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
