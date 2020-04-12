using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Shop.Domain.Models
{
    public class NewsSubComment : Comment
    {
        public int NewsMainCommentId { get; set; }
    }
}
