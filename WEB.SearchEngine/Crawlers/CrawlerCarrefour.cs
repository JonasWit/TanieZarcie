using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.SearchEngine.Enums;
using WEB.SearchEngine.Extensions;
using WEB.SearchEngine.RegexPatterns;
using WEB.SearchEngine.SearchResultsModels;

namespace WEB.SearchEngine.Crawlers
{
    public class CrawlerCarrefour : WebCrawler
    {
        public CrawlerCarrefour(Shops shop)
        {
            Shop = shop;
        }

        public override string BaseUrl { get { return ""; } }

        public override List<Product> GetResultsForSingleUrl(LinkStruct linkStruct)
        {
            var result = new List<Product>();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(linkStruct.Html);

            var divs = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .ContainsAny("productsimple-default"))
                .ToList();

            var tasks = new List<Task>();

            foreach (var div in divs)
            {
                //ExtractProduct(div, linkStruct);
                var nodeToPass = div;
                tasks.Add(Task.Run(() => result.Add(ExtractProduct(nodeToPass, linkStruct))));
            }

            Task.WaitAll(tasks.ToArray());

            result.RemoveAll(x => string.IsNullOrEmpty(x.Name));
            result.TrimExcess();
            return result;
        }

        private Product ExtractProduct(HtmlNode productNode, LinkStruct linkStruct)
        {
            var result = new Product();

            if (!productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price", MatchDireciton.Equals))))
            {
                return new Product();
            }

            var pln = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "pln", MatchDireciton.Equals)))
                .FirstOrDefault().InnerText;

            var gr = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "gr", MatchDireciton.Equals)))
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
                result.Description = " !PROMOCJA! " + String.Join(", ", promoCommnets.ToArray());
            }

            result.Seller = this.GetType().Name.Replace("Crawler", "");

            var name = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "tilename", MatchDireciton.InputContainsMatch)))
                .Select(z => z.InnerText)
                .FirstOrDefault();

            result.Name = name;

            result.TimeStamp = DateTime.Now;

            return result;
        }
    }
}
