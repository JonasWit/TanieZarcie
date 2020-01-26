using HtmlAgilityPack;
using SearchEngine.SearchEngine;
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

namespace SearchEngine.Crawlers
{
    public class CrawlerLidl : WebCrawler, IWebCrawler
    {
        public override string BaseUrl { get; set; } = "https://www.lidl.pl/pl/index.htm";

        public override string BaseUrlForProducts { get; set; } = "https://www.lidl.pl/pl/";

        public override void GetResultsForSingleUrl(Dictionary<string, List<Product>> resultDictionary, LinkStruct linkStruct)
        {
            List<Product> products = new List<Product>();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(linkStruct.Html);

            var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("product product--tile product--fullbleed")).ToList();

            if (divs.Count() == 0)
            {
                return;
            }

            foreach (var div in divs)
            {
                try
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(div.Descendants("strong").Where(node => node.GetAttributeValue("class", "").Equals("pricefield__price")).FirstOrDefault().InnerText);
                    stringBuilder.Replace("zł", "");

                    string[] splittedPrice = stringBuilder.ToString().Split(',');

                    var priceZl = int.TryParse(splittedPrice[0], out int resultZl);
                    var priceGr = int.TryParse(splittedPrice[1], out int resultGr);

                    var product = new Product
                    {
                        Provider = "Lidl",
                        Name = div.Descendants("h2").Where(node => node.GetAttributeValue("class", "").Equals("product__title")).FirstOrDefault().InnerText,
                        PriceZl = priceZl == true ? resultZl : 0,
                        PriceGr = priceGr == true ? resultGr : 0,
                        Url = $"{BaseUrlForProducts}{div.Descendants("a").Where(node => node.GetAttributeValue("class", "").Equals("product__body")).FirstOrDefault().Attributes["href"].Value}",
                        DownloadDate = DateTime.Now,
                        Category = "",
                        Mark = "",
                        Quantity = 0,
                        Description = ""
                    };

                    try
                    {
                        product.Description = div.Descendants("span").Where(node => node.GetAttributeValue("class", "").Equals("pricefield__header")).FirstOrDefault().InnerText;
                    }
                    catch (Exception)
                    {
                        continue;
                    }

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
