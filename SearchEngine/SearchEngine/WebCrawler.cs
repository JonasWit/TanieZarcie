using HtmlAgilityPack;
using SearchEngine.Crawlers;
using SearchEngine.SearchResultsModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SearchEngine.SearchEngine
{
    public abstract class WebCrawler
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public virtual string BaseUrl { get; set; } 

        public virtual string BaseUrlForProducts { get; set; }

        public struct LinkStruct
        {
            public string Link { get; set; }

            public string Html { get; set; }

            public LinkStruct(string link, string html)
            {
                Link = link;
                Html = html;
            }
        }

        public void GetData()
        {
            try
            {

                List<string> linksToVisit = SearchEngineCore.ParseLinks(BaseUrl);
                List<Thread> threads = new List<Thread>();
                Dictionary<string, List<Product>> resultDictionary = new Dictionary<string, List<Product>>();
                List<LinkStruct> webStructs = new List<LinkStruct>();

                linksToVisit.RemoveAll(item => !item.Contains("http") || !item.Contains("https"));

                foreach (var link in linksToVisit)
                {
                    Thread subThread = new Thread(new ThreadStart(() => webStructs.Add(new LinkStruct(link, GetSingleHtml(link)))));
                    subThread.Start();
                    threads.Add(subThread);
                }

                foreach (var item in threads) { item.Join(); }
                threads.Clear();

                webStructs.RemoveAll(link => !link.Link.Contains("biedronka") && !link.Link.Contains("lidl") && !link.Link.Contains("kaufland"));

                foreach (var webStruct in webStructs)
                {
                    //GetResultsForSingleUrl(resultDictionary, webStruct);
                    Thread subThread = new Thread(new ThreadStart(() => GetResultsForSingleUrl(resultDictionary, webStruct)));
                    subThread.Start();
                    threads.Add(subThread);
                }

                foreach (var item in threads) { item.Join(); }
                threads.Clear();

                foreach (var products in resultDictionary.Values)
                {
                    //List<Product> distinctProducts = products.GroupBy(p => new { p.Description, p.Provider, p.PriceZl, p.PriceGr }).Select(g => g.First()).ToList();

                    foreach (var product in products)
                    {
                        Products.Add(product);
                    }
                }

                Products.TrimExcess();
            }
            catch (Exception)
            {
                return;
            }
        }

        private string GetSingleHtml(string link)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = null;

                try
                {
                    responseMessage = client.GetAsync(link).Result;
                }
                catch (AggregateException)
                {
                    return "";
                }

                if (responseMessage.IsSuccessStatusCode)
                {
                    try
                    {
                        return client.GetStringAsync(link).Result;
                    }
                    catch (AggregateException)
                    {
                        return "";
                    }
                }
            }
            return "";
        }

        public virtual void GetResultsForSingleUrl(Dictionary<string, List<Product>> resultDictionary, LinkStruct linkStruct)
        {
            //To be always overriten by derived classes.
        }


    }
}
