using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface ICrawlersDataBaseManager
    {
        Task<int> UpdateDatabaseAsync(List<Product> products);
        Task<int> ClearDataBaseAsync();
        Task<int> DeleteProductFromShops(string shop);
        List<(string, int, DateTime)> CheckDataBase();
    }
}
