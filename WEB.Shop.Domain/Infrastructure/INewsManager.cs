using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface INewsManager
    {
        Task<int> CreateOneNews(OneNews post);
        Task<int> UpdateOneNews(OneNews post);
        Task<int> DeleteOneNews(int id);
        TResult GetOneNews<TResult>(int id, Func<OneNews, TResult> selector);

        IEnumerable<TResult> GetNews<TResult>(string category, Func<OneNews, TResult> selector, Func<OneNews, bool> predicate);
        IEnumerable<TResult> GetNews<TResult>(Func<OneNews, TResult> selector);
    }
}
