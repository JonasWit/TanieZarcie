using HtmlAgilityPack;
using SearchEngine.SearchResultsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WEB.SearchEngine.Crawlers;
using WEB.SearchEngine.Enums;
using WEB.SearchEngine.Interfaces;

namespace WEB.SearchEngine
{
    public sealed class Engine
    {
        public List<ICrawler> Crawlers { get; private set; } = new List<ICrawler>();

        public bool RunCrawlerForBiedronkaAsync()
        {

            var crawler = new CrawlerBiedronka(Shops.Biedronka);
            crawler.GetData();


            Crawlers.Add(crawler);
            return true;
        }



    }
}
