using HtmlAgilityPack;
using SearchEngine.SearchResultsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using SearchEngine.SearchEngine;

namespace SearchEngine.Crawlers
{
    public class CrawlerCarrefour : WebCrawler, IWebCrawler
    {
        public override string BaseUrl { get; set; } = "https://www.biedronka.pl/pl/";

        public override void GetResultsForSingleUrl(Dictionary<string, List<Product>> resultDictionary, LinkStruct linkStruct)
        {
            List<Product> products = new List<Product>();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(linkStruct.Html);

            var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("productsimple-default")).ToList();

            if (divs.Count() == 0)
            {
                return;
            }

            foreach (var div in divs)
            {
                try
                {
                    var priceZl = int.TryParse(div.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("pln")).FirstOrDefault().InnerText, out int resultZl);
                    var priceGr = int.TryParse(div.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("gr")).FirstOrDefault().InnerText, out int resultGr);

                    var product = new Product
                    {
                        Provider = "Biedronka",
                        Name = div.Descendants("a").FirstOrDefault().Attributes["title"].Value,
                        PriceZl = priceZl == true ? resultZl : 0,
                        PriceGr = priceGr == true ? resultGr : 0,
                        Url = linkStruct.Link,
                        DownloadDate = DateTime.Now,
                        Category = "",
                        Mark = "",
                        Quantity = 0,
                        Description = ""
                    };

                    products.Add(product);
                }
                catch (Exception)
                {

                    continue;
                }
            }

            try
            {
                resultDictionary.Add(linkStruct.Link, products);
            }
            catch (Exception)
            {

                return;
            }
        }
    }
}
