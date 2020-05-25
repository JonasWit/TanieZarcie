using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WEB.Shop.Application.Logs;

namespace WEB.Shop.UI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AutomationController : ControllerBase
    {
        [HttpGet("CheckUpCall")]
        public async Task<IActionResult> WakeUpCheck([FromServices] CreateLogRecord  createLogRecord)
        {
            await createLogRecord.DoAsync(new CreateLogRecord.Request { Message = "Wake Up Call", TimeStamp = DateTime.Now });
            return Ok();
        }

        [HttpGet("AutomatedCrawlers")]
        public async Task<IActionResult> AutomatedCrawlers([FromServices] CreateLogRecord createLogRecord)
        {
            await createLogRecord.DoAsync(new CreateLogRecord.Request { Message = "Crawlers Run", TimeStamp = DateTime.Now });
            return Ok();
        }
    }
}
