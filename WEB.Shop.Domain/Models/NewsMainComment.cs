using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Shop.Domain.Models
{
    public class NewsMainComment : Comment
    {
        public int OneNewsId { get; set; }
        public List<NewsSubComment> SubComments { get; set; }
    }
}
