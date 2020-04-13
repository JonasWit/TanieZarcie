using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Application.Files;
using WEB.Shop.Application.News;
using WEB.Shop.Application.News.NewsComments;
using WEB.Shop.UI.ViewModels.News;

namespace WEB.Shop.UI.Controllers
{
    public class NewsController : Controller
    {
        public int PageSize { get; set; } = 5;

        [HttpGet]
        public IActionResult NewsOverview(int pageNumber, string category, [FromServices] GetNews getNews)
        {
            if (pageNumber < 1)
            {
                return RedirectToAction("NewsOverview", new { pageNumber = 1, category });
            }

            var news = string.IsNullOrEmpty(category) ? getNews.Do(PageSize, pageNumber) : getNews.Do(PageSize, pageNumber, category);
            var newsVm = new List<NewsViewModel>();

            foreach (var singleNews in news)
            {
                newsVm.Add(new NewsViewModel
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

            newsVm = newsVm.OrderByDescending(x => x.Created).ToList();

            var viewModel = new NewsPageViewModel
            {
                News = newsVm,
                PageNumber = pageNumber,
                Category = category            
            };

            int newsCount = string.IsNullOrEmpty(category) ? getNews.Count() : getNews.Count(category);
            int skipAmount = PageSize * (pageNumber - 1);
            int capacity = skipAmount + PageSize;

            if (newsCount > capacity)
            {
                viewModel.NextPage = true;
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult SingleNewsDisplay(int id, [FromServices] GetOneNews getOneNews)
        {
            var singleNews = getOneNews.Do(id);
            return View(new NewsViewModel
            {
                Id = singleNews.Id,
                Title = singleNews.Title,
                Body = singleNews.Body,
                ImagePath = singleNews.Image,
                Created = singleNews.Created,
                Description = singleNews.Description,
                Tags = singleNews.Tags,
                Category = singleNews.Category,

                MainComments = singleNews.MainComments?.Select(comment => new NewsCommentViewModel
                {
                    Id = comment.Id,
                    Created = comment.Created,
                    Creator = comment.Creator,
                    Message = comment.Message,
                    SubComments = comment.SubComments?.Select(subComment => new NewsCommentViewModel
                    {
                        Id = subComment.Id,
                        Created = subComment.Created,
                        Creator = subComment.Creator,
                        Message = subComment.Message,
                        NewsMainCommentId = subComment.NewsMainCommentId,
                    }).ToList()
                }).ToList(),
            });
        }

        [HttpGet("/image/{image}")]
        [ResponseCache(CacheProfileName = "Weekly")]
        public IActionResult Image(string image, [FromServices] GetFile getFile) => new FileStreamResult(getFile.Do(image), $"image/{image.Substring(image.LastIndexOf('.') + 1)}");

        [HttpPost]
        public async Task<IActionResult> Comment(NewsCommentViewModel vm,
            [FromServices] GetOneNews getOneNews,
            [FromServices] CreateComment createComment)
        {
            if (!ModelState.IsValid)
            {
                var singleNews = getOneNews.Do(vm.NewsId);
                return RedirectToAction("SingleNewsDisplay", new { id = singleNews.Id });
            }

            var news = getOneNews.Do(vm.NewsId);

            if (vm.NewsMainCommentId == 0)
            {
                await createComment.CreateMainComment(new CreateComment.MainCommentRequest
                {
                    OneNewsId = news.Id,
                    Created = DateTime.Now,
                    Creator = "",
                    Message = vm.Message,
                });
            }
            else
            {
                await createComment.CreateSubComment(new CreateComment.SubCommentRequest
                {
                    NewsMainCommentId = vm.NewsMainCommentId,
                    Created = DateTime.Now,
                    Creator = "",
                    Message = vm.Message,
                });
            }

            return RedirectToAction("SingleNewsDisplay", new { id = news.Id });
        }

        public async Task<IActionResult> DeleteMainComment(int newsId, int commentId,
            [FromServices] DeleteComment deleteComment)
        {
            await deleteComment.DeleteMainComment(commentId);
            return RedirectToAction("SingleNewsDisplay", new { id = newsId });
        }

        public async Task<IActionResult> DeleteSubComment(int newsId, int commentId,
            [FromServices] DeleteComment deleteComment)
        {
            await deleteComment.DeleteSubComment(commentId);
            return RedirectToAction("SingleNewsDisplay", new { id = newsId });
        }

    }
}