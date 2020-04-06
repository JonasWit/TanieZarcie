using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.News
{
    [TransientService]
    public class DeleteNews
    {
        private readonly INewsManager _newsManager;
        public DeleteNews(INewsManager newsManager) => _newsManager = newsManager;

        public Task<int> Do(int id) =>  _newsManager.DeleteOneNews(id);
    }
}
