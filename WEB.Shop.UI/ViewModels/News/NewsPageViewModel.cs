using System;
using System.Collections.Generic;
using WEB.Shop.UI.ViewModels.Enums;

namespace WEB.Shop.UI.ViewModels.News
{
    public class NewsPageViewModel
    {
        public IEnumerable<NewsViewModel> News { get; set; }
        public int PageNumber { get; set; }
        public bool NextPage { get; set; }
        public string Category { get; set; }

        public List<string> Categories { get; set; }

        public NewsPageViewModel()
        {
            Categories = new List<string>();

            foreach (NewsCategory category in (NewsCategory[])Enum.GetValues(typeof(NewsCategory)))
            {
                Categories.Add(category.ToString());
            }
        }
    }
}
