using System.Collections.Generic;
using WEB.SearchEngine;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Crawlers
{
    public class CrawlersCommander
    {
        public Engine SearchEngine { get; private set; }
        public List<Product> DomainModels { get; private set; }
        public List<SearchEngine.SearchResultsModels.Product> EngineModels { get; private set; }

        public int test { get; set; } = 2;

        public void Add()
        {
            test++;
        }

        public void Remove()
        {
            test--;
        }

        public CrawlersCommander()
        {
            SearchEngine = new Engine();
        }

        public bool RunEngine()
        {
            SearchEngine.RunCrawlerForBiedronkaAsync();

            return true;

        }





    }
}
