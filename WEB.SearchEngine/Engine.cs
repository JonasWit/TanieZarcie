﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.SearchEngine.Crawlers;
using WEB.SearchEngine.Enums;

namespace WEB.SearchEngine
{
    public sealed class Engine
    {
        public List<WebCrawler> Crawlers { get; private set; } = new List<WebCrawler>();

        public async Task RunAllCrawlersAsync()
        {
            var tasks = new List<Task>();
            Crawlers.Clear();

            Crawlers.Add(new CrawlerBiedronka());
            Crawlers.Add(new CrawlerKaufland());
            Crawlers.Add(new CrawlerLidl());
            Crawlers.Add(new CrawlerCarrefour());
            Crawlers.Add(new CrawlerAuchan());

            foreach (var crawler in Crawlers)
            {
                var crawlerToRun = crawler;
                tasks.Add(Task.Run(() => crawlerToRun.GetData()));
            }

            await Task.Run(() => Task.WaitAll(tasks.ToArray()));
        }

        public async Task RunCrawlerForSpecificShopAsync(Shops shop)
        {
            Crawlers.RemoveAll(c => c.Shop == shop);

            switch (shop)
            {
                case Shops.Biedronka:
                    var biedronkaCrawler = new CrawlerBiedronka();
                    await Task.Run(() => biedronkaCrawler.GetData());
                    Crawlers.Add(biedronkaCrawler);
                    break;
                case Shops.Lidl:
                    var lidlCrawler = new CrawlerLidl();
                    await Task.Run(() => lidlCrawler.GetData());
                    Crawlers.Add(lidlCrawler);
                    break;
                case Shops.Kaufland:
                    var kauflandCrawler = new CrawlerKaufland();
                    await Task.Run(() => kauflandCrawler.GetData());
                    Crawlers.Add(kauflandCrawler);
                    break;
                case Shops.Carrefour:
                    var carrefourCrawler = new CrawlerCarrefour();
                    await Task.Run(() => carrefourCrawler.GetData());
                    Crawlers.Add(carrefourCrawler);
                    break;
                case Shops.Auchan:
                    var auchanCrawler = new CrawlerAuchan();
                    await Task.Run(() => auchanCrawler.GetData());
                    Crawlers.Add(auchanCrawler);
                    break;
                case Shops.Stokrotka:
                    break;
                case Shops.Zabka:
                    break;
                default:
                    break;
            }
        }
    }
}
