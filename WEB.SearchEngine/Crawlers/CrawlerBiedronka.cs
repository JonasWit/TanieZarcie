﻿using HtmlAgilityPack;
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
    public class CrawlerBiedronka : WebCrawler
    {
        public override string[] BaseUrls { get { return new string[] { "https://www.biedronka.pl/pl/" }; } }

        public CrawlerBiedronka()
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

            result.RemoveAll(x => x == null || string.IsNullOrEmpty(x.Name));
            result.TrimExcess();
            return result;
        }

        private Product ExtractProduct(HtmlNode productNode, LinkStruct linkStruct)
        {
            var result = new Product();

            #region Check if product node exists

            if (!productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price", MatchDireciton.Equals) || CrawlerRegex.StandardMatch(y.Value, "price-wrapper", MatchDireciton.Equals)))) return new Product();

            #endregion

            #region Get Name

            var name = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "tilename", MatchDireciton.InputContainsMatch)))
                .Select(z => z.InnerText)
                .FirstOrDefault()?
                .RemoveMetaCharacters()
                .RemoveUnwantedStrings()
                .Replace(";", "");

            result.Name = name;

            #endregion

            #region Get Description

            #endregion

            #region Get Producer

            #endregion

            #region Get Category

            result.Category = "Markety Spożywcze";

            #endregion

            #region Get Price and Sale Price, set OnSale Flag

            if (productNode.Descendants().Any(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price-old", MatchDireciton.Equals))))
            {
                var pln = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "pln", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText
                    .RemoveMetaCharacters();

                var gr = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "gr", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText
                    .RemoveMetaCharacters();

                if (decimal.TryParse(pln, out decimal plnDecimal) && decimal.TryParse(gr, out decimal grDecimal)) result.Value = plnDecimal + (grDecimal / 100);
                else return new Product();

                var oldPrice = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "price-old", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText
                    .RemoveNonNumeric();

                if (decimal.TryParse(oldPrice, out decimal oldPriceDecimal)) result.SaleValue = oldPriceDecimal / 100;
                else return new Product();

                result.OnSale = true;
            }
            else
            {
                var pln = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "pln", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText
                    .RemoveMetaCharacters();

                var gr = productNode.Descendants()
                    .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "gr", MatchDireciton.Equals)))
                    .FirstOrDefault()?
                    .InnerText
                    .RemoveMetaCharacters();

                if (decimal.TryParse(pln, out decimal plnDecimal) && decimal.TryParse(gr, out decimal grDecimal)) result.Value = plnDecimal + (grDecimal / 100);
                else return new Product();

                result.OnSale = false;
            }

            #endregion

            #region Get Sale Description

            var promoCommnets = productNode.Descendants()
                .Where(x => x.Attributes.Any(y => y.Name == "class" && CrawlerRegex.StandardMatch(y.Value, "productpromo", MatchDireciton.InputContainsMatch)))
                .Select(z => z.InnerText.RemoveMetaCharacters())
                .ToList();

            if (promoCommnets.Count != 0)
            {
                result.SaleDescription = String.Join(", ", promoCommnets.ToArray());
                result.OnSale = true;
            }

            #endregion

            #region Get Sale Deadline

            #endregion

            #region Get Seller, TimeStamp, URL

            result.Seller = this.GetType().Name.Replace("Crawler", "");
            result.TimeStamp = DateTime.Now;

            var productUrl = productNode.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

            result.SourceUrl = new Uri(new Uri(BaseUrls[0]), productUrl).ToString();

            #endregion

            return result;
        }
    }
}
