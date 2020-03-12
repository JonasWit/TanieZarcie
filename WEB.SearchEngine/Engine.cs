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

        public async Task<int> RunAllCrawlersAsync()
        {
            var tasks = new List<Task>();

            Crawlers.Add(new CrawlerBiedronka(Shops.Biedronka));
            Crawlers.Add(new CrawlerKaufland(Shops.Kaufland));

            foreach (var crawler in Crawlers)
            {
                tasks.Add(Task.Run(() => crawler.GetData()));
            }

            await Task.Run(() => Task.WaitAll(tasks.ToArray()));
            return Crawlers.Sum(x => x.Products.Count);
        }

        public async Task<int> RunCrawlerForBiedronkaAsync()
        {
            var crawler = new CrawlerBiedronka(Shops.Biedronka);

            await Task.Run(() => crawler.GetData());
 
            Crawlers.Add(crawler);
            return crawler.Products.Count;
        }

        public async Task<int> RunCrawlerForKauflandAsync()
        {
            var crawler = new CrawlerKaufland(Shops.Kaufland);

            await Task.Run(() => crawler.GetData());

            Crawlers.Add(crawler);
            return crawler.Products.Count;
        }
    }
}
