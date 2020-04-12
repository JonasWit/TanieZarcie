using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.News.NewsComments
{
    [TransientService]
    public class CreateComment
    {
        private readonly INewsManager _newsManager;

        public CreateComment(INewsManager newsManager) => _newsManager = newsManager;

        public async Task<int> CreateMainComment(MainCommentRequest request) =>
            await _newsManager.CreateNewsMainComment(new NewsMainComment
                { 
                    Created = request.Created,
                    Creator = request.Creator,
                    Message = request.Message,
                    OneNewsId = request.OneNewsId
                });

        public async Task<int> CreateSubComment(SubCommentRequest request) =>
            await _newsManager.CreateNewsSubComment(new NewsSubComment
                {
                    Created = request.Created,
                    Creator = request.Creator,
                    Message = request.Message,
                    NewsMainCommentId = request.NewsMainCommentId
                });


        public class MainCommentRequest
        {
            public int Id { get; set; }
            public int OneNewsId { get; set; }
            public string Message { get; set; }
            public DateTime Created { get; set; }
            public string Creator { get; set; }

            public IEnumerable<SubCommentRequest> SubComments { get; set; }
        }

        public class SubCommentRequest
        {
            public int Id { get; set; }
            public int NewsMainCommentId { get; set; }
            public string Message { get; set; }
            public DateTime Created { get; set; }
            public string Creator { get; set; }
        }

    }
}
