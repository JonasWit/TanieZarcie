using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.News
{
    [TransientService]
    public class UpdateNews
    {
        private readonly INewsManager _newsManager;
        private readonly IFileManager _fileManager;

        public UpdateNews(INewsManager newsManager, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _newsManager = newsManager;
        }

        public async Task<Response> DoAsync(Request request)
        {
            var post = _newsManager.GetOneNews(request.Id, x => x);

            post.Title = request.Title;
            post.Body = request.Body;
            post.Created = request.Created;

            post.Description = request.Description;
            post.Tags = request.Tags;
            post.Category = request.Category;

            if (request.Image != null)
            {
                _fileManager.DeleteImage(post.Image);
                post.Image = await _fileManager.SaveImageAsync(request.Image);
            }

            await _newsManager.UpdateOneNews(post);

            return new Response
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Image = post.Image,
                Created = post.Created,
                Description = post.Description,
                Tags = post.Tags,
                Category = post.Category
            };
        }

        public class Request
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }

            public string Description { get; set; }
            public string Tags { get; set; }
            public string Category { get; set; }

            public IFormFile Image { get; set; } = null;
            public DateTime Created { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }

            public string Description { get; set; }
            public string Tags { get; set; }
            public string Category { get; set; }

            public string Image { get; set; } = null;
            public DateTime Created { get; set; }
        }
    }
}
