using System;
using System.Collections.Generic;
using System.Text;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.News.NewsComments
{
    [TransientService]
    public class UpdateComment
    {
        private readonly INewsManager _newsManager;

        public UpdateComment(INewsManager newsManager) => _newsManager = newsManager;

    }
}
