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
        private readonly IFileManager _fileManager;

        public DeleteNews(INewsManager newsManager, IFileManager fileManager)
        { 
            _newsManager = newsManager;
            _fileManager = fileManager;
        }

        public async Task<int> Do(int id)
        {
            var post = _newsManager.GetOneNews(id, x => x);

            if (await _newsManager.DeleteOneNews(id) > 0)
            {
                _fileManager.DeleteImage(post.Image);
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
