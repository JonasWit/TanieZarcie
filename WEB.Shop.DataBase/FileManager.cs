using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.DataBase
{
    public class FileManager : IFileManager
    {
        private readonly string _imagePath;

        public FileManager(IConfiguration configuration) => _imagePath = configuration["Path:Images"];

        public FileStream ImageStream(string image) => new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            try
            {
                var savePath = Path.Combine(_imagePath);

                if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

                var fileName = $"img_{DateTime.Now:dd-MM-yyyy-HH-mm-ss}{image.FileName.Substring(image.FileName.LastIndexOf('.'))}";

                using var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create);
                await image.CopyToAsync(fileStream);

                return fileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool DeleteImage(string image)
        {
            try
            {
                var file = Path.Combine(_imagePath, image);

                if (File.Exists(file)) File.Delete(file);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
