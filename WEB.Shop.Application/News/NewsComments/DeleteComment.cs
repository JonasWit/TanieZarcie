using System;
using System.Collections.Generic;
using System.Text;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.News.NewsComments
{
    [TransientService]
    public class DeleteComment
    {
        private readonly INewsManager _newsManager;

        public DeleteComment(INewsManager newsManager) => _newsManager = newsManager;

    }
}
