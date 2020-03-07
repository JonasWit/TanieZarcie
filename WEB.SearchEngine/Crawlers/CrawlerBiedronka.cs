using HtmlAgilityPack;
using SearchEngine.SearchResultsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.SearchEngine.Interfaces;

namespace WEB.SearchEngine.Crawlers
{
    public class CrawlerBiedronka : WebCrawler, ICrawler
    {
        //protected List<HtmlPatternBiedronka> _htmlPattens;
        //public List<HtmlPatternBiedronka> HtmlPattens { get { return _htmlPattens; } set { _htmlPattens = value; } }

        public override string BaseUrl { get; set; } = "https://www.biedronka.pl/pl/";

        public override void GetResultsForSingleUrl(Dictionary<string, List<Product>> resultDictionary, LinkStruct linkStruct)
        {
            List<Product> products = new List<Product>();
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(linkStruct.Html);

            //foreach (var htmlPattern in _htmlPattens)
            //{
            //    var divs = htmlDocument.DocumentNode.Descendants(htmlPattern.TopNode.Descendant).Where(node => node.GetAttributeValue(htmlPattern.TopNode.AttributeName, "").Contains(htmlPattern.TopNode.AttributeValue)).ToList();

            //    if (divs.Count() == 0)
            //    {
            //        return;
            //    }

            //    foreach (var div in divs)
            //    {
            //        var product = new Product();
            //        //todo: SE zrobic metody na kazde wyszukanie
            //        product.Seller = "Biedronka";
            //        product.Name = div.Descendants(htmlPattern.Name.Descendant).FirstOrDefault().Attributes[htmlPattern.Name.AttributeValue].Value.Replace(@"&quot;","");
            //        product.SourceUrl = linkStruct.Link;
            //        product.TimeStamp = DateTime.Now;
            //        product.Category = "";
            //        product.Producer = "";

            //        try
            //        {
            //            product.Description = div.Descendants(htmlPattern.Description.Descendant).Where(node => htmlPattern.Description.CombinedAttributeName.Any(node.GetAttributeValue(htmlPattern.Description.AttributeName, "").Contains)).FirstOrDefault().InnerText;
            //        }
            //        catch
            //        {
            //            product.Description = "Ni ma komentarza, pewno coś nowego, kliknij IDŹ!";
            //        }

            //        try
            //        {
            //            if (int.TryParse(div.Descendants(htmlPattern.ZlNode.Descendant).Where(node => node.GetAttributeValue(htmlPattern.ZlNode.AttributeName, "").Equals(htmlPattern.ZlNode.AttributeValue)).FirstOrDefault().InnerText, out int resultZl))
            //            {
            //                product.PriceZl = resultZl;
            //            }
            //            else
            //            {
            //                continue;
            //            }
            //        }
            //        catch
            //        {
            //            continue;
            //        }

            //        try
            //        {
            //            if (int.TryParse(div.Descendants(htmlPattern.GrNode.Descendant).Where(node => node.GetAttributeValue(htmlPattern.GrNode.AttributeName, "").Equals(htmlPattern.GrNode.AttributeValue)).FirstOrDefault().InnerText, out int resultGr))
            //            {
            //                product.PriceGr = resultGr;
            //            }
            //            else
            //            {
            //                continue;
            //            }
            //        }
            //        catch
            //        {
            //            continue;
            //        }

            //        products.Add(product);
            //    }

            //    resultDictionary.Add(linkStruct.Link, products);

            //}
        }
    }
}
