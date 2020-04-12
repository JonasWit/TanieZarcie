using System;
using System.Collections.Generic;
using System.Linq;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.News
{
    [TransientService]
    public class GetOneNews
    {
        private readonly INewsManager _newsManager;
        public GetOneNews(INewsManager newsManager, IFileManager fileManager) => _newsManager = newsManager;

        public Response Do(int id) =>
            _newsManager.GetOneNews(id, singleNews => new Response
            {
                Id = singleNews.Id,
                Title = singleNews.Title,
                Body = singleNews.Body,
                Image = singleNews.Image,
                Created = singleNews.Created,
                Description = singleNews.Description,
                Tags = singleNews.Tags,
                Category = singleNews.Category,

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
                    }).ToList()
                }).ToList(),
            });

        public class Response
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }

            public string Description { get; set; }
            public string Tags { get; set; }
            public string Category { get; set; }

            public List<Comment> MainComments { get; set; }

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

            public List<Comment> SubComments { get; set; }
        }
    }
}
