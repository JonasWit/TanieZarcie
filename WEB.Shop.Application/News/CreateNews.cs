﻿using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.News
{
    [TransientService]
    public class CreateNews
    {
        private readonly IFileManager _fileManager;
        private readonly INewsManager _newsManager;
        public CreateNews(INewsManager newsManager, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _newsManager = newsManager;
        }

        public async Task<Response> DoAsync(Request request)
        {
            var singleNews = new OneNews
            {
                Title = request.Title,
                Body = request.Body,
                Created = request.Created,

                Description = request.Description,
                Tags = request.Tags,
                Category = request.Category,

                Image = await _fileManager.SaveImageAsync(request.Image)
            };

            if (await _newsManager.CreateOneNews(singleNews) <= 0)
            {
                throw new Exception("Failed to add post!");
            }

            return new Response
            {
                Id = singleNews.Id,
                Title = singleNews.Title,
                Body = singleNews.Body,
                Image = singleNews.Image,
                Created = singleNews.Created,
                Description = singleNews.Description,
                Tags = singleNews.Tags,
                Category = singleNews.Category,
            };
        }

        public class Request
        {
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
