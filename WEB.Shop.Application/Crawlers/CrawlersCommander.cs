﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.SearchEngine;
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

        public List<Product> Results { get; private set; }
        public Dictionary<string, int> DataCacheCount { get; set; }
        public Dictionary<string, bool> CrawlerIssues { get; set; }
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

            DataCacheCount = new Dictionary<string, int>();
            Results = new List<Product>();

            foreach (var item in Enum.GetValues(typeof(Shops)))
            {
                DataCacheCount.Add(item.ToString(), 0);
            }
        }

        #region Crawlers

        public async Task<int> RunEngineAsync()
        {
            await _searchEngine.RunAllCrawlersAsync();
            ConvertSearchModelsToDomainModels();

            DataCacheCount[Shops.Biedronka.ToString()] = Results.Where(p => p.Seller == Shops.Biedronka.ToString()).Count();
            DataCacheCount[Shops.Kaufland.ToString()] = Results.Where(p => p.Seller == Shops.Kaufland.ToString()).Count();
            DataCacheCount[Shops.Lidl.ToString()] = Results.Where(p => p.Seller == Shops.Lidl.ToString()).Count();
            DataCacheCount[Shops.Carrefour.ToString()] = Results.Where(p => p.Seller == Shops.Lidl.ToString()).Count();
            DataCacheCount[Shops.Auchan.ToString()] = Results.Where(p => p.Seller == Shops.Lidl.ToString()).Count();
            return Results.Count;
        }

        public async Task<int> RunEngineAsync(string shop)
        {
            var shopEnum = (Shops)Enum.Parse(typeof(Shops), shop, true);

            await _searchEngine.RunCrawlerForSpecificShopAsync(shopEnum);
            ConvertSearchModelsToDomainModels();

            DataCacheCount[shopEnum.ToString()] = Results.Count;
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

            DataCacheCount[shop.ToString()] = 0;

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
                        Value = product.Value,

                        OnSale = product.OnSale,
                        SaleDeadline = product.SaleDeadline,
                        SaleDescription = product.SaleDescription,
                        SaleValue = product.SaleValue
                    });
                }
            }
        }

        #endregion

        #region Database Update

        public async Task UpdateAllData()
        {
            await _crawlersDataBaseManager.ClearDataBaseAsync();
            await _crawlersDataBaseManager.UpdateDatabaseAsync(Results);
            await CheckDataBase();
        }

        public async Task UpdateAllDataAutomated()
        {
            await _crawlersDataBaseManager.ClearDataBaseAsync();
            await _crawlersDataBaseManager.UpdateDatabaseAsync(Results);
        }

        public async Task UpdateDataBase(string shop)
        {
            var shopEnum = (Shops)Enum.Parse(typeof(Shops), shop, true);

            await ClearDataBaseAsync(shopEnum.ToString());
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
