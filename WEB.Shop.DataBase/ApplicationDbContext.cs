﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStock> OrderStocks { get; set; }
        public DbSet<StockOnHold> StocksOnHold { get; set; }
        public DbSet<OneNews> News { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NewsMainComment> NewsMainComments { get; set; }
        public DbSet<NewsSubComment> NewsSubComments { get; set; }
        public DbSet<LogRecord> LogRecords { get; set; }
        public DbSet<ShopData> ShopsData { get; set; }
        public DbSet<PromoSheetUrl> PromoSheetsUrls { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderStock>()
                .HasKey(x => new { x.StockId, x.OrderId });

            builder.SeedData();
        }
    }
}
