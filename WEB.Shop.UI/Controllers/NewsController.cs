using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WEB.Shop.Application.Files;
using WEB.Shop.Application.News;
using WEB.Shop.UI.ViewModels.News;

namespace WEB.Shop.UI.Controllers
{
    public class NewsController : Controller
    {
        [HttpGet]
        public IActionResult NewsOverview(string category, [FromServices] GetNews getNews)
        {
            var posts = string.IsNullOrEmpty(category) ? getNews.Do() : getNews.Do(category);
            var viewModels = new List<NewsViewModel>();

            foreach (var post in posts)
            {
                viewModels.Add(new NewsViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    ImagePath = post.Image,
                    Created = post.Created,
                    Description = post.Description,
                    Tags = post.Tags,
                    Category = post.Category
                });
            }

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult SingleNewsDisplay(int id, [FromServices] GetOneNews getSingleNews)
        {
            var singleNews = getSingleNews.Do(id);

            return View(new NewsViewModel
            {
                Id = singleNews.Id,
                Title = singleNews.Title,
                Body = singleNews.Body,
                ImagePath = singleNews.Image,
                Created = singleNews.Created,
                Description = singleNews.Description,
                Tags = singleNews.Tags,
                Category = singleNews.Category
            });
        }

        [HttpGet("/image/{image}")]
        public IActionResult Image(string image, [FromServices] GetFile getFile) => new FileStreamResult(getFile.Do(image), $"image/{image.Substring(image.LastIndexOf('.') + 1)}");

        [HttpGet]
        public IActionResult TEST([FromServices] GetFilesForContent getFilesForContent)
        {
            getFilesForContent.Do("News");

            return RedirectToAction("NewsOverview", new NewsViewModel());
        }
    }
}