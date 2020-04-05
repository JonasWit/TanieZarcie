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

        public override string[] BaseUrls { get { return new string[] { "https://www.carrefour.pl/" }; } }

        public override List<Product> GetResultsForSingleUrl(LinkStruct linkStruct)
        {
            var result = new List<Product>();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(linkStruct.Html);

            var divs = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .ContainsAny("panel-body"))
                .ToList();

            var tasks = new List<Task>();

            foreach (var div in divs)
            {
                //todo JW - v1.1 - zalatwic crawlera
                ExtractProduct(div, linkStruct);
                var nodeToPass = div;
                //tasks.Add(Task.Run(() => result.Add(ExtractProduct(nodeToPass, linkStruct))));
            }

            Task.WaitAll(tasks.ToArray());

            result.RemoveAll(x => string.IsNullOrEmpty(x.Name));
            result.TrimExcess();
            return result;
        }

        private Product ExtractProduct(HtmlNode productNode, LinkStruct linkStruct)
        {
            //todo JW - v1.1 - wykorzystac nowe pola w modelu

            var result = new Product();

            if (!productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price-box", MatchDireciton.Equals))))
            {
                return new Product();
            }

            if (productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "regular-price", MatchDireciton.Equals))))
            {
                var priceNode = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "regular-price", MatchDireciton.Equals)))
                    .FirstOrDefault();

                var price = priceNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price", MatchDireciton.Equals)))
                    .FirstOrDefault()
                    .InnerText
                    .RemoveNonNumeric();

                if (decimal.TryParse(price, out decimal priceDecimal)) result.Value = priceDecimal / 100;




            }
            else
            {
                var regularPriceNode = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "old-price", MatchDireciton.Equals)))
                    .FirstOrDefault();

                var promoPriceNode = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "special-price", MatchDireciton.Equals)))
                    .FirstOrDefault();


                var regularPrice = regularPriceNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText;

                var promoPrice = regularPriceNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price text-red", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText;

                //var promoCommnets = productNode.Descendants()
                //    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "productpromo", MatchDireciton.InputContainsMatch)))
                //    .Select(z => z.InnerText)
                //    .ToList();

                //if (promoCommnets.Count != 0) result.Description = " !PROMOCJA! " + String.Join(", ", promoCommnets.ToArray());
            }


            //var pln = productNode.Descendants()
            //    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "pln", MatchDireciton.Equals)))
            //    .FirstOrDefault().InnerText;

            //var gr = productNode.Descendants()
            //    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "gr", MatchDireciton.Equals)))
            //    .FirstOrDefault().InnerText;

            //if (decimal.TryParse(pln, out decimal plnDecimal) && decimal.TryParse(gr, out decimal grDecimal)) result.Value = plnDecimal + (grDecimal / 100);


            result.SourceUrl = linkStruct.Link;

            result.Seller = this.GetType().Name.Replace("Crawler", "");

            var name = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "text4 text-primary", MatchDireciton.InputContainsMatch)))
                .Select(z => z.InnerText)
                .FirstOrDefault();

            result.Name = name;

            result.TimeStamp = DateTime.Now;

            return result;
        }
    }
}
