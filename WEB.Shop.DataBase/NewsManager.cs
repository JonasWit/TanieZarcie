using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class NewsManager : INewsManager
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public NewsManager(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public Task<int> CreateOneNews(OneNews post)
        {
            _applicationDbContext.News.Add(post);

            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> DeleteOneNews(int id)
        {
            var post = _applicationDbContext.News.FirstOrDefault(x => x.Id == id);
            _applicationDbContext.News.Remove(post);

            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateOneNews(OneNews post)
        {
            _applicationDbContext.News.Update(post);

            return _applicationDbContext.SaveChangesAsync();
        }

        public TResult GetOneNews<TResult>(int id, Func<OneNews, TResult> selector)
        {
            return _applicationDbContext.News
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();
        }

        public List<OneNews> GetNew() => _applicationDbContext.News.ToList();

        public IEnumerable<TResult> GetNews<TResult>(Func<OneNews, TResult> selector) =>
            _applicationDbContext.News.Select(selector).ToList();

        public IEnumerable<TResult> GetNews<TResult>(string category, Func<OneNews, TResult> selector, Func<OneNews, bool> predicate) =>
            _applicationDbContext.News.Where(predicate).Select(selector).ToList();

    }
}
