using Microsoft.EntityFrameworkCore;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public static class Seed
    {
        public static void SeedData(this ModelBuilder builder)
        {
            builder.Entity<ShopData>().HasData(
                new ShopData { Id = 1, Name = "Biedronka" },
                new ShopData { Id = 2, Name = "Lidl" },
                new ShopData { Id = 3, Name = "Kaufland" },
                new ShopData { Id = 4, Name = "Carrefour" },
                new ShopData { Id = 5, Name = "Auchan" },
                new ShopData { Id = 6, Name = "Aldi" },
                new ShopData { Id = 7, Name = "InterMarche" });

            builder.Entity<PromoSheetUrl>().HasData(
                new PromoSheetUrl { ShopId = 1, Id = 1, Url = "https://www.biedronka.pl/pl/gazetki" },
                new PromoSheetUrl { ShopId = 2, Id = 2, Url = "https://www.lidl.pl/informacje-dla-klienta/nasze-gazetki" },
                new PromoSheetUrl { ShopId = 3, Id = 3, Url = "https://www.kaufland.pl/gazeta.html" },
                new PromoSheetUrl { ShopId = 4, Id = 4, Url = "https://www.carrefour.pl/promocje/gazetka-promocyjna" },
                new PromoSheetUrl { ShopId = 5, Id = 5, Url = "https://www.auchan.pl/pl/gazetki" },
                new PromoSheetUrl { ShopId = 6, Id = 6, Url = "https://www.aldi.pl/gazetki.html" },
                new PromoSheetUrl { ShopId = 7, Id = 7, Url = "https://intermarche.pl/gazetka" });
        }
    }
}
