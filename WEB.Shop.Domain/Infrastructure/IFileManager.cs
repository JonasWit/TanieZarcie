using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface IFileManager
    {
        Task<string> SaveImageAsync(IFormFile image);
        FileStream ImageStream(string image);
        bool DeleteImage(string image);
    }
}
