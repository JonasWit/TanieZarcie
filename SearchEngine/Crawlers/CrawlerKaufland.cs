using HtmlAgilityPack;
using SearchEngine.SearchResultsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using SearchEngine.SearchEngine;
using System.Text;
using System.Text.RegularExpressions;

namespace SearchEngine.Crawlers
{
    public class CrawlerKaufland : WebCrawler, IWebCrawler
    {
        protected List<HtmlPatternKaufland> _htmlPattens;
        public List<HtmlPatternKaufland> HtmlPattens { get { return _htmlPattens; } set { _htmlPattens = value; } }

        public override string BaseUrl { get; set; } = "https://www.kaufland.pl/";

        public override void GetResultsForSingleUrl(Dictionary<string, List<Product>> resultDictionary, LinkStruct linkStruct)
        {
            List<Product> products = new List<Product>();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(linkStruct.Html);

            foreach (var htmlPattern in _htmlPattens)
            {
                var divs = htmlDocument.DocumentNode.Descendants(htmlPattern.TopNode.Descendant).Where(node => node.GetAttributeValue(htmlPattern.TopNode.AttributeName, "").Contains(htmlPattern.TopNode.AttributeValue)).ToList();

                if (divs.Count() == 0)
                {
                    return;
                }

                foreach (var div in divs)
                {
                    var product = new Product();

                    var price = div.Descendants(htmlPattern.PriceNode.Descendant).Where(node => node.GetAttributeValue(htmlPattern.PriceNode.AttributeName, "").Equals(htmlPattern.PriceNode.AttributeValue)).FirstOrDefault().InnerText;

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(price);
                    stringBuilder.Replace("zł", "");

                    string[] splittedPrice = stringBuilder.ToString().Split(',');

                    var priceZl = int.TryParse(splittedPrice[0], out int resultZl);
                    var priceGr = int.TryParse(splittedPrice[1], out int resultGr);

                    product.PriceZl = priceZl == true ? resultZl : 0;
                    product.PriceGr = priceGr == true ? resultGr : 0;

                    product.Provider = "Kaufland";

                    try
                    {
                        product.Name = div.Descendants(htmlPattern.Name.Descendant).Where(node => node.GetAttributeValue(htmlPattern.Name.AttributeName, "").Equals(htmlPattern.Name.AttributeValue)).FirstOrDefault().InnerText;
                    }
                    catch (NullReferenceException)
                    {
                        continue;
                    }

                    try
                    {
                        product.Name += $" {div.Descendants(htmlPattern.SubName.Descendant).Where(node => node.GetAttributeValue(htmlPattern.SubName.AttributeName, "").Equals(htmlPattern.SubName.AttributeValue)).FirstOrDefault().InnerText}";
                    }
                    catch (NullReferenceException)
                    {
                        continue;
                    }

                    try
                    {
                        var description = div.Descendants(htmlPattern.Description.Descendant).Where(node => node.GetAttributeValue(htmlPattern.Description.AttributeName, "").Equals(htmlPattern.Description.AttributeValue)).FirstOrDefault().InnerText;
                        product.Description = Regex.Replace(description, @"\t|\n|\r", "");
                    }
                    catch (NullReferenceException)
                    {
                        product.Description = "";
                    }

                    product.Url = linkStruct.Link;
                    product.DownloadDate = DateTime.Now;
                    product.Category = "";
                    product.Mark = "";
                    product.Quantity = 0;

                    products.Add(product);
                }

                resultDictionary.Add(linkStruct.Link, products);
            }
        }
    }
}
