using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WEB.SearchEngine.Enums;
using WEB.SearchEngine.RegexPatterns;
using WEB.SearchEngine.SearchResultsModels;

namespace WEB.SearchEngine.Crawlers
{
    public class CrawlerIkea : WebCrawler
    {
        public override string[] BaseUrls { get { return new string[] { "https://www.ikea.com/pl/pl/" }; } }

        public CrawlerIkea()
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
                .AsParallel()
                .Where(
                    node => node.GetAttributeValue("class", "").Equals("product-compact") ||
                            node.GetAttributeValue("class", "").Equals("product"))
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

            if (!productNode.Descendants("span")
                .Any(n => n.Attributes.Any(x => x.Name == "class" && x.Value == "product-compact__prev-price")))
            {
                return new Product();
            }

            #endregion

            #region Get Name

            var header = productNode
                .Descendants("span")
                .FirstOrDefault(x => x.HasClass("product-compact__name"))
                .InnerText;

            var description = productNode
                .Descendants("span")
                .FirstOrDefault(x => x.HasClass("product-compact__type"))
                .InnerText
                .RemoveMetaCharacters()
                .Trim();

            if (header != null)
            {
                result.Name += $" {header}";
            }

            if (description != null)
            {
                result.Name += $" {description}";
            }

            result.Name = Regex.Replace(result.Name, @"\s+", " ");

            if (string.IsNullOrEmpty(result.Name))
            {
                return new Product();
            }

            #endregion

            #region Get Description


            #endregion

            #region Get Producer


            #endregion

            #region Get Category

            result.Category = "Markety Budowlane";

            #endregion

            #region Get Price and Sale Price

            var value = productNode.GetAttributeValue("data-price", "");

            var saleValue = productNode.Descendants("span")
                .FirstOrDefault(x => x.HasClass("product-compact__comparable-price-element"))?
                .InnerText?
                .RemoveMetaCharacters();

            if (!Regex.IsMatch(value, @"\.[0-9][0-9]$"))
            {
                value = value.RemoveNonNumeric();
                value += "00";
            }
            else
            {
                value = value.Replace(@".", "");
            }

            if (!Regex.IsMatch(saleValue, @"\,[0-9][0-9]$"))
            {
                saleValue = saleValue.RemoveNonNumeric();
                saleValue += "00";
            }
            else
            {
                saleValue = saleValue.Replace(",", "");
            }

            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(saleValue))
            {
                return new Product();
            }

            if (decimal.TryParse(value, out decimal valueDecimal) &&
            decimal.TryParse(saleValue, out decimal saleValueDecimal))
            {
                result.SaleValue = saleValueDecimal / 100;
                result.Value = valueDecimal / 100;
                result.OnSale = true;
            }
            else
            {
                return new Product();
            }

            #endregion

            #region Get Sale Description



            #endregion

            #region Get Sale Deadline



            #endregion

            #region Get Seller, TimeStamp, URL

            result.Seller = this.GetType().Name.Replace("Crawler", "");
            result.TimeStamp = DateTime.Now;
            result.SourceUrl = productNode.Descendants("a").FirstOrDefault()?.GetAttributeValue("href","");

            #endregion

            return result;
        }
    }
}
