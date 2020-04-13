using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface IFileManager
    {
        Task<string> SaveImageAsync(IFormFile image);
        FileStream ImageStream(string image);
        FileStream ImageStream(string image, string imageUsage, int width, int height);
        string[] GetAllPicturesFromContent(string contentSubfolder);
        bool DeleteImage(string image);
    }
}
