using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.News.NewsComments
{
    [TransientService]
    public class UpdateComment
    {
        private readonly INewsManager _newsManager;

        public UpdateComment(INewsManager newsManager) => _newsManager = newsManager;

        public async Task<Response> DoAsync(Request request)
        {
            var oneNews = _newsManager.GetOneNews(request.Id, x => x);





            return new Response();
        }

        public class Request
        {
            public int OneNewsId { get; set; }
            public int Id { get; set; }
            public string Message { get; set; }
            public DateTime Created { get; set; }
            public string Creator { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public DateTime Created { get; set; }
            public string Creator { get; set; }
        }





    }
}
