using HtmlAgilityPack;
using SearchEngine.SearchResultsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WEB.SearchEngine.Crawlers
{
    public abstract class WebCrawler
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public virtual string BaseUrl { get; set; } 
        public virtual string BaseUrlForProducts { get; set; }

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

        public async Task GetData()
        {
            try
            {

                List<string> linksToVisit = ParseLinks(BaseUrl);

                List<Thread> threads = new List<Thread>();
                Dictionary<string, List<Product>> resultDictionary = new Dictionary<string, List<Product>>();
                List<LinkStruct> webStructs = new List<LinkStruct>();

                linksToVisit.RemoveAll(item => !item.Contains("http") || !item.Contains("https"));


                var tasks = new List<Task>();

                foreach (var link in linksToVisit)
                {
                    //Thread subThread = new Thread(new ThreadStart(() => webStructs.Add(new LinkStruct(link, GetSingleHtml(link)))));
                    //subThread.Start();
                    //threads.Add(subThread);

                    tasks.Add(Task.Run(() => webStructs.Add(new LinkStruct(link, GetSingleHtml(link)))));







                }


                await Task.WhenAll(tasks);


                foreach (var item in threads) { item.Join(); }
                threads.Clear();



 



                webStructs.RemoveAll(link => !link.Link.Contains("biedronka") && !link.Link.Contains("lidl") && !link.Link.Contains("kaufland"));

                foreach (var webStruct in webStructs)
                {
                    //GetResultsForSingleUrl(resultDictionary, webStruct);
                    Thread subThread = new Thread(new ThreadStart(() => GetResultsForSingleUrl(resultDictionary, webStruct)));
                    subThread.Start();
                    threads.Add(subThread);
                }

                foreach (var item in threads) { item.Join(); }
                threads.Clear();

                foreach (var products in resultDictionary.Values)
                {
                    //List<Product> distinctProducts = products.GroupBy(p => new { p.Description, p.Provider, p.PriceZl, p.PriceGr }).Select(g => g.First()).ToList();

                    foreach (var product in products)
                    {
                        Products.Add(product);
                    }
                }

                Products.TrimExcess();
            }
            catch (Exception)
            {
                return;
            }
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

        public virtual void GetResultsForSingleUrl(Dictionary<string, List<Product>> resultDictionary, LinkStruct linkStruct)
        {
            //To be always overriten by derived classes.
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
