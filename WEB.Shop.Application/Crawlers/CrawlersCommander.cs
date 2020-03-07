using System;
using System.Collections.Generic;
using System.Text;
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

        public void RunEngine()
        {
            SearchEngine.RunCrawlerForBiedronka();



        }



    }
}
