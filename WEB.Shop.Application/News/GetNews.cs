using System;
using System.Collections.Generic;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.News
{
    [TransientService]
    public class GetNews
    {
        private readonly INewsManager _newsManager;
        public GetNews(INewsManager newsManager) => _newsManager = newsManager;

        public IEnumerable<Response> Do() =>
            _newsManager.GetNews(post => new Response
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Image = post.Image,
                Created = post.Created,
                Description = post.Description,
                Tags = post.Tags,
                Category = post.Category
            });

        public IEnumerable<Response> Do(string category) =>
            _newsManager.GetNews(category, post => new Response
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Image = post.Image,
                Created = post.Created,
                Description = post.Description,
                Tags = post.Tags,
                Category = post.Category
            }, post => post.Category.ToUpper() == category.ToUpper());

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
