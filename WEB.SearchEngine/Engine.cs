using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.SearchEngine.Crawlers;
using WEB.SearchEngine.Enums;
using WEB.SearchEngine.Interfaces;

namespace WEB.SearchEngine
{
    public sealed class Engine
    {
        public List<ICrawler> Crawlers { get; private set; } = new List<ICrawler>();

        public async Task<int> RunCrawlerForBiedronkaAsync()
        {
            var crawler = new CrawlerBiedronka(Shops.Biedronka);

            await Task.Run(() => crawler.GetData());
 
            Crawlers.Add(crawler);
            return crawler.Products.Count;
        }
    }
}
