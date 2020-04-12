﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PhotoSauce.MagicScaler;
using System;
using System.IO;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.DataBase
{
    public enum ContentStore
    { 
        News = 0,
        Carousel = 1,
    }

    public class FileManager : IFileManager
    {
        private readonly string _newsImagesPath;
        private readonly string _carouselImagesPath;

        public FileManager(IConfiguration configuration)
        { 
            _newsImagesPath = configuration["Path:News"];
            _carouselImagesPath = configuration["Path:Carousel"];
        } 

        public FileStream ImageStream(string image) => new FileStream(Path.Combine(_newsImagesPath, image), FileMode.Open, FileAccess.Read);

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            try
            {
                var savePath = Path.Combine(_newsImagesPath);

                if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

                var fileName = $"img_{DateTime.Now:dd-MM-yyyy-HH-mm-ss}{image.FileName.Substring(image.FileName.LastIndexOf('.'))}";

                using var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create);

                //await image.CopyToAsync(fileStream);
                await Task.Run(() => MagicImageProcessor.ProcessImage(image.OpenReadStream(), fileStream, ImageOptions()));

                return fileName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string[] GetAllPicturesFromContent(string contentSubfolder)
        {
            return contentSubfolder switch
            {
                "News" => Directory.GetFiles(Path.Combine(_newsImagesPath)),
                "Carousel" => Directory.GetFiles(Path.Combine(_carouselImagesPath)),
                _ => null,
            };
        }

        public bool DeleteImage(string image)
        {
            try
            {
                var file = Path.Combine(_newsImagesPath, image);
                if (File.Exists(file)) File.Delete(file);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        private ProcessImageSettings ImageOptions() => new ProcessImageSettings
        {
            Width = 800,
            Height = 500,
            ResizeMode = CropScaleMode.Crop,
            SaveFormat = FileFormat.Jpeg,
            JpegQuality = 100,
            JpegSubsampleMode = ChromaSubsampleMode.Subsample420
        };
    }
}
