using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.News
{
    [TransientService]
    public class GetNews
    {
        private readonly INewsManager _newsManager;
        public GetNews(INewsManager newsManager) => _newsManager = newsManager;

        public IEnumerable<Response> Do() =>
            _newsManager.GetNews(singleNews => new Response
            {
                Id = singleNews.Id,
                Title = singleNews.Title,
                Body = singleNews.Body,
                Image = singleNews.Image,
                Created = singleNews.Created,
                Description = singleNews.Description,
                Tags = singleNews.Tags,

                MainComments = singleNews.MainComments.Select(comment => new Comment
                {
                    Id = comment.Id,
                    Created = comment.Created,
                    Creator = comment.Creator,
                    Message = comment.Message,
                    SubComments = comment.SubComments.Select(subComment => new Comment
                    {
                        Id = subComment.Id,
                        Created = subComment.Created,
                        Creator = subComment.Creator,
                        Message = subComment.Message,
                        NewsMainCommentId = subComment.NewsMainCommentId,
                    })
                }),

                Category = singleNews.Category
            });

        public IEnumerable<Response> Do(string category) =>
            _newsManager.GetNews(category, singleNews => new Response
            {
                Id = singleNews.Id,
                Title = singleNews.Title,
                Body = singleNews.Body,
                Image = singleNews.Image,
                Created = singleNews.Created,
                Description = singleNews.Description,
                Tags = singleNews.Tags,

                MainComments = singleNews.MainComments.Select(comment => new Comment
                {
                    Id = comment.Id,
                    Created = comment.Created,
                    Creator = comment.Creator,
                    Message = comment.Message,
                    SubComments = comment.SubComments.Select(subComment => new Comment
                    { 
                        Id = subComment.Id,
                        Created = subComment.Created,
                        Creator = subComment.Creator,
                        Message = subComment.Message,
                        NewsMainCommentId = subComment.NewsMainCommentId,
                    })
                }),

                Category = singleNews.Category
            }, post => post.Category.ToUpper() == category.ToUpper());

        public class Response
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }

            public string Description { get; set; }
            public string Tags { get; set; }
            public string Category { get; set; }

            public IEnumerable<Comment> MainComments { get; set; }

            public string Image { get; set; } = null;
            public DateTime Created { get; set; }
        }

        public class Comment
        {
            public int Id { get; set; }
            public int NewsMainCommentId { get; set; }
            public string Message { get; set; }
            public DateTime Created { get; set; }
            public string Creator { get; set; }

            public IEnumerable<Comment> SubComments { get; set; }
        }
    }
}
