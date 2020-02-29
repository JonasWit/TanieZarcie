using System;
using System.Collections.Generic;
using System.Text;
using WEB.SearchEngine;

namespace WEB.Shop.Application.Crawlers
{
    public class CrawlersCommander
    {
        public Engine SearchEngine { get; set; }

        public CrawlersCommander()
        {
            SearchEngine = new Engine();
        }

        public int test { get; set; } = 0;



        public void Add()
        {
            test++;
        }

        public void Remove()
        {
            test--;
        }



    }
}
