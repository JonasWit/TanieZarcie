using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Schema;
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
            Crawlers.Add(new CrawlerZabka());
            Crawlers.Add(new CrawlerObi());
            Crawlers.Add(new CrawlerLeroyMerlin());
            Crawlers.Add(new CrawlerAldi());
            //Crawlers.Add(new CrawlerMediaMarkt());
            Crawlers.Add(new CrawlerInterMarche());
            Crawlers.Add(new CrawlerIkea());

            foreach (var crawler in Crawlers)
            {
                var crawlerToRun = crawler;
                tasks.Add(Task.Run(() => crawlerToRun.GetData(crawler.Shop != Shops.Lidl)));
            }

            await Task.Run(() => Task.WaitAll(tasks.ToArray()));
        }

        public async Task RunCrawlerForSpecificShopAsync(Shops shop)
        {
            Crawlers.RemoveAll(c => c.Shop == shop);

            switch (shop)
            {
                case Shops.Biedronka:
                    var crawlerBiedronka = new CrawlerBiedronka();
                    await Task.Run(() => crawlerBiedronka.GetData());
                    Crawlers.Add(crawlerBiedronka);
                    break;
                case Shops.Lidl:
                    var crawlerLidl = new CrawlerLidl();
                    await Task.Run(() => crawlerLidl.GetData(false));
                    Crawlers.Add(crawlerLidl);
                    break;
                case Shops.Kaufland:
                    var crawlerKaufland = new CrawlerKaufland();
                    await Task.Run(() => crawlerKaufland.GetData());
                    Crawlers.Add(crawlerKaufland);
                    break;
                case Shops.Carrefour:
                    var crawlerCarrefour = new CrawlerCarrefour();
                    await Task.Run(() => crawlerCarrefour.GetData());
                    Crawlers.Add(crawlerCarrefour);
                    break;
                case Shops.Auchan:
                    var crawlerAuchan = new CrawlerAuchan();
                    await Task.Run(() => crawlerAuchan.GetData());
                    Crawlers.Add(crawlerAuchan);
                    break;
                case Shops.Stokrotka:
                    break;
                case Shops.Zabka:
                    var crawlerZabka = new CrawlerZabka();
                    await Task.Run(() => crawlerZabka.GetData());
                    Crawlers.Add(crawlerZabka);
                    break;
                case Shops.Castorama:
                    var crawlerCastorama = new CrawlerCastorama();
                    await Task.Run(() => crawlerCastorama.GetData());
                    Crawlers.Add(crawlerCastorama);
                    break;
                case Shops.Obi:
                    var crawlerObi = new CrawlerObi();
                    await Task.Run(() => crawlerObi.GetData());
                    Crawlers.Add(crawlerObi);
                    break;
                case Shops.LeroyMerlin:
                    var crawlerLeroyMerlin = new CrawlerLeroyMerlin();
                    await Task.Run(() => crawlerLeroyMerlin.GetData());
                    Crawlers.Add(crawlerLeroyMerlin);
                    break;
                case Shops.Aldi:
                    var crawlerAldi = new CrawlerAldi();
                    await Task.Run(() => crawlerAldi.GetData());
                    Crawlers.Add(crawlerAldi);
                    break;
                case Shops.MediaMarkt:
                    var crawlerMediaMarkt = new CrawlerMediaMarkt();
                    await Task.Run(() => crawlerMediaMarkt.GetData());
                    Crawlers.Add(crawlerMediaMarkt);
                    break;
                case Shops.InterMarche:
                    var crawlerInterMarche = new CrawlerInterMarche();
                    await Task.Run(() => crawlerInterMarche.GetData());
                    Crawlers.Add(crawlerInterMarche);
                    break;
                case Shops.Ikea:
                    var crawlerIkea = new CrawlerIkea();
                    await Task.Run(() => crawlerIkea.GetData());
                    Crawlers.Add(crawlerIkea);
                    break;
                default:
                    break;
            }
        }
    }
}
