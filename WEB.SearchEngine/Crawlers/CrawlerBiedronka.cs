using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.SearchEngine.Enums;
using WEB.SearchEngine.Interfaces;
using WEB.SearchEngine.RegexPatterns;
using WEB.SearchEngine.SearchResultsModels;

namespace WEB.SearchEngine.Crawlers
{
    public class CrawlerBiedronka : WebCrawler, ICrawler
    {
        //protected List<HtmlPatternBiedronka> _htmlPattens;
        //public List<HtmlPatternBiedronka> HtmlPattens { get { return _htmlPattens; } set { _htmlPattens = value; } }

        public CrawlerBiedronka(Shops shop)
        {
            Shop = shop;
        }

        public override string BaseUrl { get; set; } = "https://www.biedronka.pl/pl/";

        public override List<Product> GetResultsForSingleUrl(LinkStruct linkStruct)
        {
            var result = new List<Product>();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(linkStruct.Html);

            var divs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Contains("productsimple-default")).ToList();

            var tasks = new List<Task>();

            foreach (var div in divs)
            {
                ExtractProduct(div, linkStruct);

                //tasks.Add(Task.Run(() => result.Add(ExtractProduct(div, linkStruct))));
            }

            Task.WaitAll(tasks.ToArray());



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

            return result;
        }

        private Product ExtractProduct(HtmlNode productNode, LinkStruct linkStruct)
        {
            var result = new Product();

            if (!productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "price")))
            {
                return new Product();
            }

            var pln = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "pln"))
                .FirstOrDefault().InnerText;

            var gr = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "gr"))
                .FirstOrDefault().InnerText;

            if (decimal.TryParse(pln, out decimal plnDecimal) && decimal.TryParse(gr, out decimal grDecimal))
            {
                result.Value = plnDecimal + (grDecimal / 100);
            }
            else
            {
                return new Product();
            }

            result.SourceUrl = linkStruct.Link;

            var promoCommnets = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "productpromo", MatchDireciton.InputContainsMatch)))
                .Select(z => z.InnerText)
                .ToList();

            if (promoCommnets.Count != 0)
            {
                result.Description = String.Join(", ", promoCommnets.ToArray());
            }

            result.Seller = this.GetType().Name.Replace("Crawler", "");

            var name = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "tilename", MatchDireciton.InputContainsMatch)))
                .Select(z => z.InnerText)
                .FirstOrDefault();


            return result;
        }
    }
}
