using Microsoft.AspNetCore.Mvc;
using System;
using WEB.Shop.Application.Files;

namespace WEB.Shop.UI.Controllers
{
    public class ImagesController : Controller
    {
        [HttpGet("/ShopCardImage/{image}")]
        [ResponseCache(CacheProfileName = "Weekly")]
        public IActionResult GetImageShopCard(string image, [FromServices] GetFile getFile)
        {
            try
            {
                return new FileStreamResult(getFile.GetShopCardImage(image), $"image/{image.Substring(image.LastIndexOf('.') + 1)}");
            }
            catch (Exception)
            {
                return new FileStreamResult(getFile.GetShopCardImage("Placeholder.jpg"), $"image/{"Placeholder.jpg".Substring("Placeholder.jpg".LastIndexOf('.') + 1)}");
            }
        }
    }
}