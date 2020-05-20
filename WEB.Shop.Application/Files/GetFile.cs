using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Files
{
    [TransientService]
    public class GetFile
    {
        private readonly IFileManager _fileManager;
        public GetFile(IFileManager fileManager) => _fileManager = fileManager;

        public FileStream Do(string image) => _fileManager.ImageStream(image);

        public FileStream GetShopCardImage(string image) => _fileManager.ImageStreamShopCard(image);
    }
}
