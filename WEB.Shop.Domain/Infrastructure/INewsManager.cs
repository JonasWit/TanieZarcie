using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface INewsManager
    {
        Task<int> CreateOneNews(OneNews news);
        Task<int> UpdateOneNews(OneNews news);
        Task<int> DeleteOneNews(int id);

        int CountNews(string category);
        int CountNews();

        Task<int> CreateNewsMainComment(NewsMainComment mainComment);
        Task<int> CreateNewsSubComment(NewsSubComment subComment);

        Task<int> UpdateNewsMainComment(NewsMainComment mainComment);
        Task<int> UpdateNewsSubComment(NewsSubComment subComment);

        Task<int> DeleteNewsMainComment(int id);
        Task<int> DeleteNewsSubComment(int id);

        TResult GetOneNews<TResult>(int id, Func<OneNews, TResult> selector);

        IEnumerable<TResult> GetNews<TResult>(string category, Func<OneNews, TResult> selector, Func<OneNews, bool> predicate);
        IEnumerable<TResult> GetNews<TResult>(Func<OneNews, TResult> selector);

        IEnumerable<TResult> GetNews<TResult>(int pageSize, int pageNumber, string category, Func<OneNews, TResult> selector, Func<OneNews, bool> predicate);
        IEnumerable<TResult> GetNews<TResult>(int pageSize, int pageNumber, Func<OneNews, TResult> selector);
    }
}
