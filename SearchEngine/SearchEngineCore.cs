using HtmlAgilityPack;
using SearchEngine.Crawlers;
using SearchEngine.SearchEngine;
using SearchEngine.SearchResultsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    public class SearchEngineCore : ISearchEngineCore
    {
        public SearchEngineCore()
        {
            //test basia
        }

        public List<Product> GetDemoData()
        {
            List<Product> products = new List<Product>
            {
                new Product("Biedronka", "Piwo", "Romper", "Browar A", 1, 20),
                new Product("Lidl", "Piwo", "Fasberg", "Browar B", 1, 25),
                new Product("Kaufland", "Piwo", "Angus", "Browar C", 1, 30)
            };

            return products;
        }

        public List<Product> GetAllData()
        {
            int id = 1;

            List<Product> allProducts = new List<Product>();

            List<HtmlPatternBiedronka> htmlPatternBiedronka = new List<HtmlPatternBiedronka>
            {
                new HtmlPatternBiedronka
                {
                    PatternName = "BiedronkaMain",
                    TopNode = new HtmlPatternBase.NodeDetails { Descendant = "div", AttributeName = "class", AttributeValue = "productsimple-default" },
                    GrNode = new HtmlPatternBase.NodeDetails { Descendant = "span", AttributeName = "class", AttributeValue = "gr" },
                    ZlNode = new HtmlPatternBase.NodeDetails { Descendant = "span", AttributeName = "class", AttributeValue = "pln" },
                    Name = new HtmlPatternBase.NodeDetails { Descendant = "a", AttributeName = "", AttributeValue = "title" },
                    Description = new HtmlPatternBase.NodeDetails { Descendant = "span", AttributeName = "class", CombinedAttributeName = new List<string> { "product", "promo" } }
                }
            };


            List<HtmlPatternKaufland> htmlPatternKaufland = new List<HtmlPatternKaufland>
            {
                new HtmlPatternKaufland
                {
                    PatternName = "KauflandMain",
                    TopNode = new HtmlPatternBase.NodeDetails { Descendant = "div", AttributeName = "class", AttributeValue = "m-offer-tile m-offer-tile--line-through m-offer-tile--uppercase-subtitle " },
                    Name = new HtmlPatternBase.NodeDetails { Descendant = "h5", AttributeName = "class", AttributeValue = "m-offer-tile__subtitle" },
                    SubName = new HtmlPatternBase.NodeDetails { Descendant = "h4", AttributeName = "class", AttributeValue = "m-offer-tile__title" },
                    PriceNode = new HtmlPatternBase.NodeDetails { Descendant = "div", AttributeName = "class", AttributeValue = "a-pricetag__price" },
                    Description = new HtmlPatternBase.NodeDetails { Descendant = "div", AttributeName = "class", AttributeValue = "a-pricetag__discount" }
                }
            };


            List<IWebCrawler> crawlers = new List<IWebCrawler>
            {
                new CrawlerBiedronka
                {
                    HtmlPattens = htmlPatternBiedronka
                },
                new CrawlerLidl(),
                new CrawlerKaufland
                {
                    HtmlPattens = htmlPatternKaufland
                },
            };

            foreach (var crawler in crawlers)
            {
                crawler.GetData();
            }

            foreach (var crawler in crawlers)
            {
                foreach (var product in crawler.Products)
                {
                    product.Id = id;
                    id++;
                }
            }

            foreach (var crawler in crawlers)
            {
                allProducts.AddRange(crawler.Products);
            }

            //List<Product> result = allProducts.Select(item => item.Name).Distinct().Select(desc => allProducts.First(item => item.Description == desc)).ToList();
            allProducts.TrimExcess();

            return allProducts;
        }

        /// <summary>
        /// Stacic method which extracts all links from an url.
        /// </summary>
        /// <param name="urlToCrawl"></param>
        /// <returns></returns>
        public static List<string> ParseLinks(string urlToCrawl)
        {
            byte[] data;

            using (WebClient webClient = new WebClient())
            {
                data = webClient.DownloadData(urlToCrawl); 
            }

            string download = Encoding.ASCII.GetString(data);

            HashSet<string> list = new HashSet<string>();

            var doc = new HtmlDocument();
            doc.LoadHtml(download);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a[@href]");

            foreach (var n in nodes)
            {
                string href = n.Attributes["href"].Value;
                list.Add(GetAbsoluteUrlString(urlToCrawl, href));
            }
            return list.ToList();
        }


        public static string GetAbsoluteUrlString(string baseUrl, string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);

            if (!uri.IsAbsoluteUri)
            {
                uri = new Uri(new Uri(baseUrl), uri);
            }

            return uri.ToString();
        }
    }
}
