using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.News.NewsComments
{
    [TransientService]
    public class DeleteComment
    {
        private readonly INewsManager _newsManager;

        public DeleteComment(INewsManager newsManager) => _newsManager = newsManager;

        public async Task<int> DeleteMainComment(int mainCommentId)
        {
            if (await _newsManager.DeleteNewsMainComment(mainCommentId) > 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public async Task<int> DeleteSubComment(int subCommentId)
        {
            if (await _newsManager.DeleteNewsSubComment(subCommentId) > 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
