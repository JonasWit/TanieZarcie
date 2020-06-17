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
    public class CrawlerInterMarche : WebCrawler
    {
        public override string[] BaseUrls { get { return new string[] { "https://intermarche.pl/" }; } }

        public CrawlerInterMarche()
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
                .ContainsAny("product-teaser__content"))
                .ToList();

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

            if (!productNode.Descendants("div").Any(x => x.GetAttributeValue("class", "") == "price price--prev oldPriceMarker trans"))
            {
                return new Product();
            }

            #endregion

            #region Get Name

            result.Name = productNode.Descendants("p")
                .FirstOrDefault(x => x.GetAttributeValue("class", "") == "product-teaser__content__head__title")?
                .InnerText
                .Trim();

            #endregion

            #region Get Description

            #endregion

            #region Get Producer

            #endregion

            #region Get Category

            result.Category = "Markety Spożywcze";

            #endregion

            #region Get Price and Sale Price

            var regularPrice = 
                productNode.Descendants("div")
                    .FirstOrDefault(x => x.GetAttributeValue("class", "") == "price price--prev oldPriceMarker trans")
                        .Descendants("span")
                        .FirstOrDefault(x => x.GetAttributeValue("class", "") == "price-int")
                        .InnerText
                        .RemoveNonNumeric() +
                productNode.Descendants("div")
                    .FirstOrDefault(x => x.GetAttributeValue("class", "") == "price price--prev oldPriceMarker trans")
                        .Descendants("span")
                        .FirstOrDefault(x => x.GetAttributeValue("class", "") == "price-dec")
                        .InnerText
                        .RemoveNonNumeric();

            var promoPrice =
                productNode.Descendants("div")
                    .FirstOrDefault(x => x.GetAttributeValue("class", "") == "price" && x.ParentNode.GetAttributeValue("class", "") == "product-teaser__content__details change")?
                        .Descendants("span")
                        .FirstOrDefault(x => x.GetAttributeValue("class", "") == "price-int")
                        .InnerText
                        .RemoveNonNumeric() +
                productNode.Descendants("div")
                    .FirstOrDefault(x => x.GetAttributeValue("class", "") == "price" && x.ParentNode.GetAttributeValue("class", "") == "product-teaser__content__details change")?
                        .Descendants("span")
                        .FirstOrDefault(x => x.GetAttributeValue("class", "") == "price-dec")
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
