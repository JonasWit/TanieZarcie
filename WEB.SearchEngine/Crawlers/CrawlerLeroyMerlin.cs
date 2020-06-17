using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.SearchEngine.Enums;
using WEB.SearchEngine.RegexPatterns;
using WEB.SearchEngine.SearchResultsModels;

namespace WEB.SearchEngine.Crawlers
{
    public class CrawlerLeroyMerlin : WebCrawler
    {
        public override string[] BaseUrls { get { return new string[] { "https://www.leroymerlin.pl/" }; } }

        public CrawlerLeroyMerlin()
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

            var divs = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'product')]")?.ToList();

            if (divs == null) return result;

            var tasks = new List<Task>();

            foreach (var div in divs)
            {
                //ExtractProduct(div, linkStruct);
                var nodeToPass = div;
                tasks.Add(Task.Run(() => result.Add(ExtractProduct(nodeToPass, linkStruct))));
            }

            Task.WaitAll(tasks.ToArray());

            result.RemoveAll(x => x == null || string.IsNullOrEmpty(x.Name));
            result.TrimExcess();
            return result;
        }

        private Product ExtractProduct(HtmlNode productNode, LinkStruct linkStruct)
        {
            var result = new Product();

            #region Check if product node exists

            var pricesNode = productNode.Descendants("span")
                .Where(n => n.Attributes.Any(x => x.Name == "class" && CrawlerRegex.StandardMatch(x.Value, "prices", MatchDireciton.InputContainsMatch)))
                .FirstOrDefault();

            if (pricesNode == null || productNode.Descendants("h3").FirstOrDefault() == null)
            {
                return result;
            }

            #endregion

            #region Get Name

            result.Name = productNode.Descendants("h3")
                .FirstOrDefault()?
                .InnerText
                .RemoveMetaCharacters();

            #endregion

            #region Get Description

            #endregion

            #region Get Producer

            #endregion

            #region Get Category

            result.Category = "Markety Budowlane";

            #endregion

            #region Get Price and Sale Price, set OnSale Flag

            if (pricesNode.Descendants("span")
                .Any(n => n.Attributes.Any(x => x.Name == "class" && x.Value.Contains("product-price promotional"))))
            {
                try
                {
                    var prices = pricesNode.Descendants("span")
                                        .Where(n => n.Attributes.Any(x => x.Name == "class" && x.Value == "price"))
                                        .ToList();

                    var price1Int = Int32.Parse(prices[0].Descendants("span")
                                        .Where(n => n.Attributes.Any(x => x.Name == "class" && x.Value == "integer"))
                                        .FirstOrDefault()?
                                        .InnerText
                                        .RemoveNonNumeric()) * 100;

                    var price1Frac =Int32.Parse(prices[0].Descendants("span")
                                        .Where(n => n.Attributes.Any(x => x.Name == "class" && x.Value == "fractional"))
                                        .FirstOrDefault()?
                                        .InnerText
                                        .RemoveNonNumeric());

                    var price2Int = Int32.Parse(prices[1].Descendants("span")
                                        .Where(n => n.Attributes.Any(x => x.Name == "class" && x.Value == "integer"))
                                        .FirstOrDefault()?
                                        .InnerText
                                        .RemoveNonNumeric()) * 100;

                    var price2Frac = Int32.Parse(prices[1].Descendants("span")
                                        .Where(n => n.Attributes.Any(x => x.Name == "class" && x.Value == "fractional"))
                                        .FirstOrDefault()?
                                        .InnerText
                                        .RemoveNonNumeric());

                    if (decimal.TryParse((price1Int + price1Frac).ToString(), out decimal promoPriceDecimal)) result.Value = promoPriceDecimal / 100;
                    if (decimal.TryParse((price2Int + price2Frac).ToString(), out decimal regularPriceDecimal)) result.SaleValue = regularPriceDecimal / 100;

                    if ((promoPriceDecimal == regularPriceDecimal) || (promoPriceDecimal < (regularPriceDecimal * 0.3m)))
                    { 
                        return new Product();
                    } 

                    result.OnSale = true;
                }
                catch (Exception)
                {
                    return new Product();
                }
            }

            #endregion

            #region Get Sale Description

            #endregion

            #region Get Sale Deadline

            #endregion

            #region Get Seller, TimeStamp, URL

            result.Seller = this.GetType().Name.Replace("Crawler", "");
            result.TimeStamp = DateTime.Now;

            var productUrl = productNode
                .Descendants("a")
                .FirstOrDefault(x => x.Attributes.Any(y => y.Name == "class" && y.Value.NormalizeWithStandardRegex() == "Url".NormalizeWithStandardRegex()))?
                .GetAttributeValue("href", "");

            result.SourceUrl = new Uri(new Uri(BaseUrls[0]), productUrl).ToString();

            #endregion

            return result;
        }
    }
}
