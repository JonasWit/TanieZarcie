using Microsoft.EntityFrameworkCore;
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

        public int CountNews(string category) => _applicationDbContext.News.Count(news => news.Category == category);
        
        public int CountNews() => _applicationDbContext.News.Count();

        public Task<int> CreateOneNews(OneNews news)
        {
            _applicationDbContext.News.Add(news);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> DeleteOneNews(int id)
        {
            var post = _applicationDbContext.News.FirstOrDefault(x => x.Id == id);
            _applicationDbContext.News.Remove(post);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateOneNews(OneNews news)
        {
            _applicationDbContext.News.Update(news);
            return _applicationDbContext.SaveChangesAsync();
        }

        public TResult GetOneNews<TResult>(int id, Func<OneNews, TResult> selector) =>
            _applicationDbContext.News
                .Include(x => x.MainComments)
                    .ThenInclude(x => x.SubComments)
                .Where(x => x.Id == id)
                .Select(selector)
                .FirstOrDefault();

        public List<OneNews> GetNew() => _applicationDbContext.News.ToList();

        public IEnumerable<TResult> GetNews<TResult>(Func<OneNews, TResult> selector) =>
            _applicationDbContext.News
                .Include(x => x.MainComments)
                    .ThenInclude(x => x.SubComments)
                .Select(selector)
                .ToList();

        public IEnumerable<TResult> GetNews<TResult>(string category, Func<OneNews, TResult> selector, Func<OneNews, bool> predicate) =>
            _applicationDbContext.News
                .Include(x => x.MainComments)
                    .ThenInclude(x => x.SubComments)
                .Where(predicate)
                .Select(selector)
                .ToList();

        public IEnumerable<TResult> GetNews<TResult>(int pageSize, int pageNumber, string category, Func<OneNews, TResult> selector, Func<OneNews, bool> predicate) => _applicationDbContext.News
                .Include(x => x.MainComments)
                    .ThenInclude(x => x.SubComments)
                .Where(predicate)
                .Select(selector)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();
  
        public IEnumerable<TResult> GetNews<TResult>(int pageSize, int pageNumber, Func<OneNews, TResult> selector) => 
            _applicationDbContext.News
               .Include(x => x.MainComments)
                   .ThenInclude(x => x.SubComments)
               .Select(selector)
               .Skip(pageSize * (pageNumber - 1))
               .Take(pageSize)
               .ToList();

        public Task<int> CreateNewsMainComment(NewsMainComment mainComment)
        {
            _applicationDbContext.NewsMainComments.Add(mainComment);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> CreateNewsSubComment(NewsSubComment subComment)
        {
            _applicationDbContext.NewsSubComments.Add(subComment);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateNewsMainComment(NewsMainComment mainComment)
        {
            _applicationDbContext.NewsMainComments.Update(mainComment);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> UpdateNewsSubComment(NewsSubComment subComment)
        {
            _applicationDbContext.NewsSubComments.Update(subComment);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> DeleteNewsMainComment(int id)
        {
            var comment = _applicationDbContext.NewsMainComments.FirstOrDefault(x => x.Id == id);
            _applicationDbContext.NewsMainComments.Remove(comment);
            return _applicationDbContext.SaveChangesAsync();
        }

        public Task<int> DeleteNewsSubComment(int id)
        {
            var comment = _applicationDbContext.NewsSubComments.FirstOrDefault(x => x.Id == id);
            _applicationDbContext.NewsSubComments.Remove(comment);
            return _applicationDbContext.SaveChangesAsync();
        }
    }
}
