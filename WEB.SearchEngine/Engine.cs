using System.Collections.Generic;
using System.Linq;
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

            Crawlers.Add(new CrawlerBiedronka(Shops.Biedronka));
            Crawlers.Add(new CrawlerKaufland(Shops.Kaufland));

            foreach (var crawler in Crawlers)
            {
                var crawlerToRun = crawler;
                tasks.Add(Task.Run(() => crawlerToRun.GetData()));
            }

            await Task.Run(() => Task.WaitAll(tasks.ToArray()));
        }

        public async Task RunCrawlerForBiedronkaAsync()
        {
            Crawlers.RemoveAll(c => c.Shop == Shops.Biedronka);
            var crawler = new CrawlerBiedronka(Shops.Biedronka);

            await Task.Run(() => crawler.GetData());
            Crawlers.Add(crawler);
        }

        public async Task RunCrawlerForKauflandAsync()
        {
            Crawlers.RemoveAll(c => c.Shop == Shops.Kaufland);
            var crawler = new CrawlerKaufland(Shops.Kaufland);

            await Task.Run(() => crawler.GetData());
            Crawlers.Add(crawler);
        }
    }
}
