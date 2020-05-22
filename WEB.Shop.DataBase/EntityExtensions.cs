using Microsoft.EntityFrameworkCore;
using System.Linq;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public static class EntityExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }

        public static void DeleteProductsFromShop(this DbSet<Product> dbSet, string shop)
        {
            dbSet.RemoveRange(dbSet.Where(x => x.Distributor.DistributorName == shop));
        }
    }
}
