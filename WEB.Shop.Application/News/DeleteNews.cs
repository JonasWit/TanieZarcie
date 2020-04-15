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

        public int Do(int id)
        {
            var news = _newsManager.GetOneNews(id, x => x);

            if (_newsManager.DeleteOneNews(id) > 0)
            {
                _fileManager.DeleteImage(news.Image);
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
