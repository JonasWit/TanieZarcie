using HtmlAgilityPack;
using SearchEngine.SearchResultsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WEB.SearchEngine.Enums;
using WEB.SearchEngine.Extensions;

namespace WEB.SearchEngine.Crawlers
{
    public abstract class WebCrawler
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public virtual string BaseUrl { get; set; } 
        public virtual string BaseUrlForProducts { get; set; }
        public Shops Shop { get; set; }

        public struct LinkStruct
        {
            public string Link { get; set; }
            public string Html { get; set; }
            public LinkStruct(string link, string html)
            {
                Link = link;
                Html = html;
            }
        }

        public async Task GetDataAsync()
        {
            try
            {
                var tasks = new List<Task>();      
                var webStructs = new List<LinkStruct>();

                var linksToVisit = ParseLinks(BaseUrl);
                linksToVisit.RemoveAll(item => !item.Contains("http") || !item.Contains("https"));

                foreach (var link in linksToVisit)
                {
                    tasks.Add(Task.Run(() => webStructs.Add(new LinkStruct(link, GetSingleHtml(link)))));
                }

                await Task.WhenAll(tasks);

                webStructs.RemoveAll(link => !LinkCleanUp(link.Link, Shop.ToString()));

                Products = await ExtractDataFromRecordsAsync(webStructs);
                Products.TrimExcess();
            }
            catch (Exception)
            {
                return;
            }
        }

        private bool LinkCleanUp(string link, string match)
        {
            if (link.MatchWithRegex(match, @"[^a-zA-Z0-9]", MatchDirection.Equals))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<List<Product>> ExtractDataFromRecordsAsync(List<LinkStruct> webStructs)
        {
            var tasks = new List<Task>();
            var result = new List<Product>();

            foreach (var webStruct in webStructs)
            {
                tasks.Add(Task.Run(() => result.AddRange(GetResultsForSingleUrl(webStruct))));
            }

            await Task.WhenAll(tasks);

            return result;
        }

        private string GetSingleHtml(string link)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = null;

                try
                {
                    responseMessage = client.GetAsync(link).Result;
                }
                catch (AggregateException)
                {
                    return "";
                }

                if (responseMessage.IsSuccessStatusCode)
                {
                    try
                    {
                        return client.GetStringAsync(link).Result;
                    }
                    catch (AggregateException)
                    {
                        return "";
                    }
                }
            }
            return "";
        }

        public virtual List<Product> GetResultsForSingleUrl(LinkStruct linkStruct)
        {
            //To be always overriten by derived classes.
            return null;
        }

        public List<string> ParseLinks(string urlToCrawl)
        {
            byte[] data;

            using (WebClient webClient = new WebClient())
            {
                data = webClient.DownloadData(urlToCrawl);
            }

            string download = Encoding.ASCII.GetString(data);

            HashSet<string> list = new HashSet<string>();

            var doc = new HtmlDocument();
            doc.LoadHtml(download);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//a[@href]");

            foreach (var n in nodes)
            {
                string href = n.Attributes["href"].Value;
                list.Add(GetAbsoluteUrlString(urlToCrawl, href));
            }

            return list.ToList();
        }

        private string GetAbsoluteUrlString(string baseUrl, string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);

            if (!uri.IsAbsoluteUri)
            {
                uri = new Uri(new Uri(baseUrl), uri);
            }

            return uri.ToString();
        }
    }
}
