using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WEB.SearchEngine.Enums;
using WEB.SearchEngine.Extensions;
using WEB.SearchEngine.SearchResultsModels;

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

        public void GetData()
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

                Task.WaitAll(tasks.ToArray());

                webStructs.RemoveAll(link => !LinkCleanUp(link.Link, Shop.ToString()));
                webStructs.GroupBy(x => x.Link).Select(x => x.First());

                Products = ExtractDataFromRecordsAsync(webStructs);
                Products.TrimExcess();
            }
            catch (Exception)
            {
                return;
            }
        }

        private bool LinkCleanUp(string link, string output)
        {
            if (link.MatchWithRegex(output, @"[^a-zA-Z0-9]", MatchDirection.InputContainsOutput))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Product> ExtractDataFromRecordsAsync(List<LinkStruct> webStructs)
        {
            var result = new List<Product>();

            foreach (var webStruct in webStructs)
            {
                result.AddRange(GetResultsForSingleUrl(webStruct));
            }

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
