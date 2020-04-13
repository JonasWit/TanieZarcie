using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using WEB.Shop.UI.ViewModels.Enums;

namespace WEB.Shop.UI.ViewModels.News
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }

        public string Description { get; set; }
        public string Tags { get; set; }
        public string Category { get; set; }

        public List<NewsCommentViewModel> MainComments { get; set; }

        public IFormFile Image { get; set; } = null;
        public DateTime Created { get; set; }

        public List<string> Categories { get; set; }

        public NewsViewModel()
        {
            Categories = new List<string>();

            foreach (NewsCategory category in (NewsCategory[])Enum.GetValues(typeof(NewsCategory)))
            {
                Categories.Add(category.ToString());
            }
        }
    }
}
