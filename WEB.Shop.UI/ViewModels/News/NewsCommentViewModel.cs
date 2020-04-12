using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Shop.UI.ViewModels.News
{
    public class NewsCommentViewModel
    {
        public int Id { get; set; }
        [Required]
        public int NewsId { get; set; }
        [Required]
        public int NewsMainCommentId { get; set; }
        [Required]
        public string Message { get; set; }
        public List<NewsCommentViewModel> SubComments { get; set; }
        public string Creator { get; set; }
        public DateTime Created { get; set; }
    }
}
