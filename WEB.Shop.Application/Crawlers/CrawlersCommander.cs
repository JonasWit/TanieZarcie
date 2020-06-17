using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.SearchEngine;
using WEB.SearchEngine.Crawlers;
using WEB.SearchEngine.Enums;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Crawlers
{
    [ScopedService]
    public class CrawlersCommander
    {
        private readonly ICrawlersDataBaseManager _crawlersDataBaseManager;
        private readonly Engine _searchEngine;

        public Dictionary<string, bool> CrawlerIssues { get; set; }
        public List<SearchEngine.SearchResultsModels.Product> EngineModels { get; private set; }
        public List<DataBaseSummary> DataBaseCheck { get; set; } = new List<DataBaseSummary>();

        public class DataBaseSummary
        {
            public string Shop { get; set; }
            public int ProductsCount { get; set; }
            public int PromoProductsCount { get; set; }
            public DateTime OldestEntry { get; set; }
        }

        public CrawlersCommander(ICrawlersDataBaseManager crawlersDataBaseManager)
        {
            _crawlersDataBaseManager = crawlersDataBaseManager;
            _searchEngine = new Engine();
        }

        #region Crawlers

        public async Task<int> RunEngineAsync()
        {
            var totalProducts = 0;

            foreach (Shops shop in Enum.GetValues(typeof(Shops)))
            {
                var crawler = await _searchEngine.RunCrawlerAsync(shop);

                if (crawler == null)
                {
                    continue;
                }

                await ClearDataBaseAsync(shop.ToString());

                var result = ConvertSearchModelsToDomainModels(crawler);
                await _crawlersDataBaseManager.UpdateDatabaseAsync(result);
                await CheckDataBaseAsync();

                totalProducts += result.Count;
            }

            return totalProducts;
        }

        public async Task<int> RunEngineAsync(string shop)
        {
            var crawler = await _searchEngine.RunCrawlerAsync((Shops)Enum.Parse(typeof(Shops), shop));
            var result = ConvertSearchModelsToDomainModels(crawler);

            await ClearDataBaseAsync(shop.ToString());
            await _crawlersDataBaseManager.UpdateDatabaseAsync(result);
            await CheckDataBaseAsync();

            return result.Count;
        }

        private List<Product> ConvertSearchModelsToDomainModels(WebCrawler crawler)
        {
            var result = new List<Product>();

            foreach (var product in crawler.Products)
            {
                result.Add(new Product
                {
                    Name = product.Name,
                    Description = product.Description,
                    Category = new Category { CategoryName = product.Category },
                    Producer = new Producer { ProducerName = product.Producer },
                    Distributor = new Distributor { DistributorName = product.Seller },
                    SourceUrl = product.SourceUrl,
                    TimeStamp = product.TimeStamp,
                    Value = product.Value,

                    OnSale = product.OnSale,
                    SaleDeadline = product.SaleDeadline,
                    SaleDescription = product.SaleDescription,
                    SaleValue = product.SaleValue
                });
            }

            return result;
        }

        #endregion

        #region Database Cleanup

        public async Task ClearDataBaseAsync()
        {
            await _crawlersDataBaseManager.ClearDataBaseAsync();
            await CheckDataBaseAsync();
        }

        public async Task ClearDataBaseAsync(string shop)
        {
            var shopEnum = (Shops)Enum.Parse(typeof(Shops), shop, true);

            await _crawlersDataBaseManager.DeleteProductFromShops(shopEnum.ToString());
            await CheckDataBaseAsync();
        }

        #endregion

        public async Task CheckDataBaseAsync()
        {
            DataBaseCheck = new List<DataBaseSummary>();

            await Task.Run(() => _crawlersDataBaseManager.CleanUpDataBaseAsync());
            var check = await Task.Run(() => _crawlersDataBaseManager.CheckDataBase());

            foreach (var item in check)
            {
                DataBaseCheck.Add(new DataBaseSummary
                {
                    Shop = item.Item1,
                    ProductsCount = item.Item2,
                    PromoProductsCount = item.Item3,
                    OldestEntry = item.Item4
                });
            }
        }
    }
}
