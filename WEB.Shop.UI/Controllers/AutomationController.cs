using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WEB.Shop.Application.Crawlers;
using WEB.Shop.Application.Logs;

namespace WEB.Shop.UI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AutomationController : ControllerBase
    {
        [HttpGet("WakeUpCall")]
        public async Task<IActionResult> WakeUpCheck([FromServices] CreateLogRecord  createLogRecord)
        {
            await createLogRecord.DoAsync(new CreateLogRecord.Request { Message = "Wake Up Call", TimeStamp = DateTime.Now });
            return Ok("Processed!");
        }

        [HttpGet("RunCrawlers")]
        public async Task<IActionResult> RunCrawlers([FromServices] CreateLogRecord createLogRecord, [FromServices] CrawlersCommander crawlersCommander)
        {
            await crawlersCommander.RunEngineAsync();
            await crawlersCommander.UpdateAllData();

            await createLogRecord.DoAsync(new CreateLogRecord.Request { Message = "Crawlers Run", TimeStamp = DateTime.Now });
            return Ok("Processed!");
        }
    }
}
