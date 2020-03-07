using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.SearchEngine;

namespace WEB.Shop.Application.Crawlers
{
    public class CrawlersCommander
    {
        public Engine SearchEngine { get; private set; }


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

        public async Task<bool> RunEngineAsync()
        {
            await SearchEngine.RunCrawlerForBiedronkaAsync();

            return true;

        }



    }
}
