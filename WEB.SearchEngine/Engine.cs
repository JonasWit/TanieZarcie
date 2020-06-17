using System.Threading.Tasks;
using WEB.SearchEngine.Crawlers;
using WEB.SearchEngine.Enums;

namespace WEB.SearchEngine
{
    public sealed class Engine
    {
        public async Task<WebCrawler> RunCrawlerAsync(Shops shop)
        {
            switch (shop)
            {
                case Shops.Biedronka:
                    var crawlerBiedronka = new CrawlerBiedronka();
                    await Task.Run(() => crawlerBiedronka.GetData());
                    return crawlerBiedronka;
                case Shops.Lidl:
                    var crawlerLidl = new CrawlerLidl();
                    await Task.Run(() => crawlerLidl.GetData(false));
                    return crawlerLidl;
                case Shops.Kaufland:
                    var crawlerKaufland = new CrawlerKaufland();
                    await Task.Run(() => crawlerKaufland.GetData());
                    return crawlerKaufland;
                case Shops.Carrefour:
                    var crawlerCarrefour = new CrawlerCarrefour();
                    await Task.Run(() => crawlerCarrefour.GetData());
                    return crawlerCarrefour;
                case Shops.Auchan:
                    var crawlerAuchan = new CrawlerAuchan();
                    await Task.Run(() => crawlerAuchan.GetData());
                    return crawlerAuchan;
                case Shops.Stokrotka:
                    return null;
                case Shops.Zabka:
                    var crawlerZabka = new CrawlerZabka();
                    await Task.Run(() => crawlerZabka.GetData());
                    return crawlerZabka;
                case Shops.Castorama:
                    var crawlerCastorama = new CrawlerCastorama();
                    await Task.Run(() => crawlerCastorama.GetData());
                    return crawlerCastorama;
                case Shops.Obi:
                    var crawlerObi = new CrawlerObi();
                    await Task.Run(() => crawlerObi.GetData());
                    return crawlerObi;
                case Shops.LeroyMerlin:
                    var crawlerLeroyMerlin = new CrawlerLeroyMerlin();
                    await Task.Run(() => crawlerLeroyMerlin.GetData());
                    return crawlerLeroyMerlin;
                case Shops.Aldi:
                    var crawlerAldi = new CrawlerAldi();
                    await Task.Run(() => crawlerAldi.GetData());
                    return crawlerAldi;
                case Shops.MediaMarkt:
                    var crawlerMediaMarkt = new CrawlerMediaMarkt();
                    await Task.Run(() => crawlerMediaMarkt.GetData());
                    return crawlerMediaMarkt;
                case Shops.InterMarche:
                    var crawlerInterMarche = new CrawlerInterMarche();
                    await Task.Run(() => crawlerInterMarche.GetData());
                    return crawlerInterMarche;
                case Shops.Ikea:
                    var crawlerIkea = new CrawlerIkea();
                    await Task.Run(() => crawlerIkea.GetData());
                    return crawlerIkea;
                default:
                    return null;
            }
        }
    }
}
