using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.Shop.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public string Creator { get; set; }
    }
}
