using System.Collections.Generic;
using WEB.SearchEngine;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Crawlers
{
    public class CrawlersCommander
    {
        private ICrawlersDataBaseManager _crawlersDataBaseManager;
        private Engine _searchEngine;

        public List<Product> DomainModels { get; private set; }
        public List<SearchEngine.SearchResultsModels.Product> EngineModels { get; private set; }

        public CrawlersCommander(ICrawlersDataBaseManager crawlersDataBaseManager)
        {
            _crawlersDataBaseManager = crawlersDataBaseManager;
            _searchEngine = new Engine();
        }

        public bool RunEngine()
        {
            _searchEngine.RunCrawlerForBiedronkaAsync();

            return true;

        }

        private void ConvertSearchModelsToDomainModels()
        {
            DomainModels = new List<Product>();






        }





    }
}
