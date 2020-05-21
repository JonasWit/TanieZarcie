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
    public class CrawlerLidl : WebCrawler
    {
        public override string[] BaseUrls { get { return new string[] { "https://www.lidl.pl/pl/" }; } }

        public CrawlerLidl()
        {
            if (Enum.TryParse(this.GetType().Name.Replace("Crawler", ""), true, out Shops shop))
            {
                Shop = shop;
            }
            else
            {
                Shop = Shops.None;
            }
        }

        public override List<Product> GetResultsForSingleUrl(LinkStruct linkStruct)
        {
            var result = new List<Product>();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(linkStruct.Html);

            var divs = htmlDocument.DocumentNode.Descendants("a")
                .AsParallel()
                .Where(node => node.GetAttributeValue("class", "")
                .ContainsAny("product__body"))
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

            #region Check if product node exists

            if (!productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "pricebox__price", MatchDireciton.InputContainsMatch)))) return new Product();

            #endregion

            #region Get Name

            var names = new List<string>
            {
                productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "product__title"))
                    .Select(z => z.InnerText)
                    .FirstOrDefault()?
                    .RemoveMetaCharacters()
                    .Trim(),

                productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && y.Value == "pricebox__highlight"))
                    .Select(z => z.InnerText)
                    .FirstOrDefault()?
                    .RemoveMetaCharacters()
                    .Trim(),
            };

            names.RemoveAll(x => string.IsNullOrEmpty(x));
            result.Name = String.Join(", ", names.ToArray());

            #endregion

            #region Get Description

            #endregion

            #region Get Producer

            #endregion

            #region Get Category

            result.Category = "Markety Spożywcze";

            #endregion

            #region Get Price and Sale Price

            var promoPrice = productNode.Descendants("span")
                .FirstOrDefault(x => x.GetAttributeValue("class", "") == "pricebox__price")?
                .InnerText
                .RemoveNonNumeric();

            var regularPrice = productNode.Descendants("span")
                .FirstOrDefault(x => x.GetAttributeValue("class", "") == "pricebox__recommended-retail-price")?
                .InnerText
                .RemoveNonNumeric();

            if (decimal.TryParse(promoPrice, out decimal promoPriceDecimal)) result.Value = promoPriceDecimal / 100;
            if (decimal.TryParse(regularPrice, out decimal regularPriceDecimal)) result.SaleValue = regularPriceDecimal / 100;

            result.OnSale = true;

            #endregion

            #region Get Sale Description

            #endregion

            #region Get Sale Deadline

            #endregion

            #region Get Seller, TimeStamp, URL

            result.Seller = this.GetType().Name.Replace("Crawler", "");
            result.TimeStamp = DateTime.Now;
            result.SourceUrl = linkStruct.Link;

            var productUrl = productNode.GetAttributeValue("href", "");

            result.SourceUrl = new Uri(new Uri(BaseUrls[0]), productUrl).ToString();

            #endregion

            return result;
        }
    }
}
