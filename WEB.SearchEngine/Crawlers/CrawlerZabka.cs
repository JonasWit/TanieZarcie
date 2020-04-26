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
    public class CrawlerZabka : WebCrawler
    {
        public override string[] BaseUrls { get { return new string[] { "https://www.zabka.pl/" }; } }

        public CrawlerZabka()
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

            var divs = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .EqualsTrim("product"))
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

            #region Check if product node exists and if it is on sale

            if (!productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "product__title title", MatchDireciton.Equals)))) return new Product();

            #endregion

            #region Get Name

            var name = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "product__title title", MatchDireciton.InputContainsMatch)))
                .Select(z => z.InnerText)
                .FirstOrDefault();

            result.Name = CrawlerRegex.RemoveMetaCharacters(name).Trim();

            #endregion

            #region Get Description

            #endregion

            #region Get Producer

            #endregion

            #region Get Category

            #endregion

            #region Get Price and Sale Price, set OnSale Flag

            if (productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "product__price-wrapper", MatchDireciton.Equals))))
            {
                var priceNode = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "product__price-wrapper", MatchDireciton.Equals)))
                    .FirstOrDefault();

                if (priceNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price product__old-price", MatchDireciton.Equals))))
                {
                    var regularPriceNode = productNode.Descendants()
                        .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price product__price", MatchDireciton.Equals)))
                        .FirstOrDefault();

                    var promoPriceNode = productNode.Descendants()
                        .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price product__old-price", MatchDireciton.Equals)))
                        .FirstOrDefault();

                    var regularPricePLN = regularPriceNode.InnerText.RemoveNonNumeric();
                    var promoPricePLN = promoPriceNode.InnerText.RemoveNonNumeric();

                    if (decimal.TryParse(regularPricePLN, out decimal priceDecimal)) result.Value = priceDecimal / 100;
                    if (decimal.TryParse(promoPricePLN, out decimal promoPriceDecimal)) result.SaleValue = promoPriceDecimal / 100;

                    result.OnSale = true;
                }
                else
                {
                    var regularPriceNode = productNode.Descendants()
                        .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price product__price", MatchDireciton.Equals)))
                        .FirstOrDefault();

                    var regularPricePLN = regularPriceNode.InnerText.RemoveNonNumeric();

                    var regularPriceGR = regularPriceNode.Descendants()
                        .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "p-cents", MatchDireciton.Equals)))
                        .FirstOrDefault()?
                        .InnerText
                        .RemoveNonNumeric();

                    if (decimal.TryParse(regularPricePLN + regularPriceGR, out decimal priceDecimal)) result.Value = priceDecimal / 100;

                    result.OnSale = false;
                }
            }
            else
            {
                return new Product();
            }

            if (result.Value == 0) return new Product();

            #endregion

            #region Get Sale Description

            #endregion

            #region Get Sale Deadline

            #endregion

            #region Get Seller, TimeStamp, URL

            result.Seller = this.GetType().Name.Replace("Crawler", "");
            result.TimeStamp = DateTime.Now;
            result.SourceUrl = linkStruct.Link;

            #endregion

            return result;
        }
    }
}
