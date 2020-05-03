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
    public class CrawlerObi : WebCrawler
    {
        public override string[] BaseUrls { get { return new string[] { "https://www.obi.pl/" }; } }

        public CrawlerObi()
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

            var standardDivs = htmlDocument.DocumentNode.Descendants("div")
                .AsParallel()
                .Where(node => node.GetAttributeValue("class", "")
                .ContainsAny("teaser-hochkant border-orange single-prod"))
                .ToList();

            var listDivs = htmlDocument.DocumentNode.Descendants("li")
                .AsParallel()
                .Where(node => node.GetAttributeValue("class", "")
                .ContainsAny("product large"))
                .ToList();

            var tasks = new List<Task>();

            if (standardDivs.Count() == 0 && listDivs.Count() == 0)
            {
                return result;
            }

            foreach (var container in standardDivs)
            {
                //ExtractProduct(container, linkStruct);
                var nodeToPass = container;
                tasks.Add(Task.Run(() => result.Add(ExtractProduct(nodeToPass, linkStruct))));
            }

            foreach (var container in listDivs)
            {
                //ExtractListProduct(container, linkStruct);
                var nodeToPass = container;
                tasks.Add(Task.Run(() => result.Add(ExtractListProduct(nodeToPass, linkStruct))));
            }

            Task.WaitAll(tasks.ToArray());

            result.RemoveAll(x => string.IsNullOrEmpty(x.Name));
            result.TrimExcess();
            return result;
        }

        private Product ExtractListProduct(HtmlNode productNode, LinkStruct linkStruct)
        {
            var result = new Product();

            #region Check if product node exists

            if (!productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "description", MatchDireciton.Equals)))) return new Product();

            #endregion

            #region Get Name

            var nameNode = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "description", MatchDireciton.Equals)))
                .FirstOrDefault();

            if (nameNode == null)
            {
                return new Product();
            }
            else
            { 
                var name = nameNode
                    .Descendants("p")
                    .FirstOrDefault()?
                    .InnerText;

                if (string.IsNullOrEmpty(name))
                {
                    return new Product();
                }

                result.Name = CrawlerRegex.RemoveMetaCharacters(name).Trim();
            }

            #endregion

            #region Get Description

            #endregion

            #region Get Producer

            #endregion

            #region Get Category

            #endregion

            #region Get Price and Sale Price, set OnSale Flag

            var priceNode = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price", MatchDireciton.Equals)))
                .FirstOrDefault();

            if (priceNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price-old", MatchDireciton.InputContainsMatch))))
            {
                var regularPrice = priceNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price-old", MatchDireciton.InputContainsMatch)))
                    .FirstOrDefault()?
                    .Descendants("del")
                    .FirstOrDefault()?
                    .Descendants("span")
                    .FirstOrDefault()?
                    .GetAttributeValue("data-csscontent", "")?
                    .RemoveNonNumeric();

                var promoPrice = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price-basic", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .GetAttributeValue("data-csscontent", "")?
                    .RemoveNonNumeric();

                if (decimal.TryParse(regularPrice, out decimal priceDecimal)) result.Value = priceDecimal / 100;
                if (decimal.TryParse(promoPrice, out decimal promoPriceDecimal)) result.SaleValue = promoPriceDecimal / 100;

                result.OnSale = true;
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

        private Product ExtractProduct(HtmlNode productNode, LinkStruct linkStruct)
        {
            var result = new Product();

            #region Check if product node exists

            if (!productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price", MatchDireciton.Equals)))) return new Product();

            #endregion

            #region Get Name

            var name = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "js-ellipsis", MatchDireciton.InputContainsMatch)))
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

            var priceNode = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price", MatchDireciton.Equals)))
                .FirstOrDefault();

            if (priceNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price-old price-absolute", MatchDireciton.Equals))))
            {
                var regularPrice = priceNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price-absolute", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText
                    .RemoveNonNumeric();

                var promoPriceNode = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price-old price-absolute", MatchDireciton.Equals)))
                    .FirstOrDefault();

                var promoPricePLN = promoPriceNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "p-nb", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText
                    .RemoveNonNumeric();

                var promoPriceGR = promoPriceNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "p-cents", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText
                    .RemoveNonNumeric();


                if (decimal.TryParse(regularPrice, out decimal priceDecimal)) result.Value = priceDecimal / 100;
                if (decimal.TryParse(promoPricePLN + promoPriceGR, out decimal promoPriceDecimal)) result.SaleValue = promoPriceDecimal / 100;

                result.OnSale = true;
            }
            else
            {
                var regularPriceNode = priceNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "standard", MatchDireciton.Equals)))
                    .FirstOrDefault();

                var regularPricePLN = regularPriceNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "p-nb", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText
                    .RemoveNonNumeric();

                var regularPriceGR = regularPriceNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "p-cents", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText
                    .RemoveNonNumeric();

                if (decimal.TryParse(regularPricePLN + regularPriceGR, out decimal priceDecimal)) result.Value = priceDecimal / 100;

                result.OnSale = false;
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
