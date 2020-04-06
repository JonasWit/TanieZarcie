using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Shop.Application.News;
using WEB.Shop.UI.ViewModels.News;

namespace WEB.Shop.UI.Controllers
{
    [Authorize(Policy = "Manager")]
    public class ManagerController : Controller
    {
        public IActionResult Index([FromServices] GetNews getNews)
        {
            var news = getNews.Do();
            var viewModels = new List<NewsViewModel>();

            foreach (var singleNews in news)
            {
                viewModels.Add(new NewsViewModel
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

            return View(viewModels);
        }

        public IActionResult Remove(int id,
            [FromServices] DeleteNews deleteNews)
        {
            deleteNews.Do(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id,
            [FromServices] GetOneNews getOneNews)
        {
            if (id == null)
            {
                return View(new NewsViewModel());
            }
            else
            {
                var singleNews = getOneNews.Do((int)id);
                return View(new NewsViewModel
                {
                    Id = singleNews.Id,
                    Title = singleNews.Title,
                    Body = singleNews.Body,
                    ImagePath = singleNews.Image,
                    Description = singleNews.Description,
                    Tags = singleNews.Tags,
                    Category = singleNews.Category
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewsViewModel vm,
            [FromServices] CreateNews createNews,
            [FromServices] UpdateNews updateNews)
        {
            if (vm.Id > 0)
            {
                //Update post if it exists
                await updateNews.DoAsync(new UpdateNews.Request
                {
                    Id = vm.Id,
                    Title = vm.Title,
                    Body = vm.Body,
                    Image = vm.Image,
                    Created = vm.Created,
                    Description = vm.Description,
                    Tags = vm.Tags,
                    Category = vm.Category
                });
            }
            else
            {
                //Create new post if not exists
                await createNews.DoAsync(new CreateNews.Request
                {
                    Title = vm.Title,
                    Body = vm.Body,
                    Image = vm.Image,
                    Created = DateTime.Now,
                    Description = vm.Description,
                    Tags = vm.Tags,
                    Category = vm.Category
                });
            }

            return RedirectToAction("Index");
        }
    }
}