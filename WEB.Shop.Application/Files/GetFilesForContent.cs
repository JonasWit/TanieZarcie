using System.Collections.Generic;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Files
{
    [TransientService]
    public class GetFilesForContent
    {
        private readonly IFileManager _fileManager;
        public GetFilesForContent(IFileManager fileManager) => _fileManager = fileManager;

        public string[] Do(string category) => _fileManager.GetAllPicturesFromContent(category);
    }
}
