using System;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.News
{
    [TransientService]
    public class GetOneNews
    {
        private readonly IFileManager _fileManager;
        private readonly INewsManager _newsManager;
        public GetOneNews(INewsManager newsManager, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _newsManager = newsManager;
        }

        public Response Do(int id) =>
            _newsManager.GetOneNews(id, x => new Response
            {
                Id = x.Id,
                Title = x.Title,
                Body = x.Body,
                Image = x.Image,
                Created = x.Created,
                Description = x.Description,
                Tags = x.Tags,
                Category = x.Category
            });

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
