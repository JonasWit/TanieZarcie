using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.SearchEngine.Crawlers.JsonModels;
using WEB.SearchEngine.Enums;
using WEB.SearchEngine.Extensions;
using WEB.SearchEngine.RegexPatterns;
using WEB.SearchEngine.SearchResultsModels;

namespace WEB.SearchEngine.Crawlers
{
    public class CrawlerCarrefour : WebCrawler
    {
        public override string[] BaseUrls { get { return new string[] { "https://www.carrefour.pl/", "https://zakupycodzienne.carrefour.pl/" }; } }

        public CrawlerCarrefour()
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

        public override List<SearchResultsModels.Product> GetResultsForSingleUrl(LinkStruct linkStruct)
        {
            var result = new List<SearchResultsModels.Product>();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(linkStruct.Html);

            var json = htmlDocument.DocumentNode.Descendants("script").FirstOrDefault(x => x.Id == "__NEXT_DATA__")?.InnerText;

            CarrefourJsonModel dataSource;

            if (!string.IsNullOrEmpty(json))
            {
                dataSource = JsonConvert.DeserializeObject<CarrefourJsonModel>(json);
            }
            else
            {
                return result;
            }

            var tasks = new List<Task>();

            foreach (var productData in dataSource.props.initialState.products.data.content)
            {
                //ExtractProduct(productData, linkStruct);
                var dataToPass = productData;
                tasks.Add(Task.Run(() => result.Add(ExtractProduct(dataToPass, linkStruct))));
            }

            Task.WaitAll(tasks.ToArray());

            result.RemoveAll(x => x == null || string.IsNullOrEmpty(x.Name));
            result.TrimExcess();
            return result;
        }

        private SearchResultsModels.Product ExtractProduct(Content data, LinkStruct linkStruct)
        {
            var result = new SearchResultsModels.Product();

            #region Check if product is viable

            if (!data.actualSku.promotion)
            {
                return new SearchResultsModels.Product();
            }

            if (!string.IsNullOrEmpty(data.actualSku.amount.actualOldPriceString) ||
                !string.IsNullOrEmpty(data.actualSku.amount.actualGrossPriceString))
            {
                if (data.actualSku.amount.actualGrossPrice > data.actualSku.amount.actualOldPrice)
                {
                    return new SearchResultsModels.Product();
                }
                else
                {
                    result.OnSale = true;
                }
            }
            else
            {
                result.OnSale = true;      
            }

            #endregion

            #region Get Name

            result.Name = data.displayName;

            #endregion

            #region Get Description

            #endregion

            #region Get Producer

            #endregion

            #region Get Category

            result.Category = "Markety Spożywcze";

            #endregion

            #region Get Price and Sale Price, set OnSale Flag

            result.SaleValue = (decimal)data.actualSku.amount.actualOldPrice;
            result.Value = (decimal)data.actualSku.amount.actualGrossPrice;

            #endregion

            #region Get Sale Description

            #endregion

            #region Get Sale Deadline

            #endregion

            #region Get Seller, TimeStamp, URL

            result.Seller = this.GetType().Name.Replace("Crawler", "");
            result.TimeStamp = DateTime.Now;

            result.SourceUrl = new Uri(new Uri($"https://{ new Uri(linkStruct.Link).Host}/"), data.url).ToString();

            #endregion

            return result;
        }
    }
}
