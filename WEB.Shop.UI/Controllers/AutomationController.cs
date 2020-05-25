using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Application.Crawlers;
using WEB.Shop.Application.Logs;
using WEB.Shop.UI.API;
using WEB.Shop.UI.Automation;

namespace WEB.Shop.UI.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class AutomationController : ControllerBase
    {
        public class AutomationDetails
        {
            public string JobName { get; set; }
            public string CronExpression { get; set; }
        }

        [HttpGet("WakeUpCall")]
        public async Task<CreateLogRecord.Request> WakeUpCheck([FromServices] CreateLogRecord  createLogRecord)
        {
            var resuest = new CreateLogRecord.Request { Message = "Wake Up Call", TimeStamp = DateTime.Now };
            await createLogRecord.DoAsync(resuest);
            return resuest;
        }

        [HttpGet("RunCrawlers")]
        public async Task<CreateLogRecord.Request> RunCrawlers([FromServices] CreateLogRecord createLogRecord, [FromServices] CrawlersCommander crawlersCommander)
        {
            await crawlersCommander.RunEngineAsync();
            await crawlersCommander.UpdateAllData();

            var resuest = new CreateLogRecord.Request { Message = "Crawlers Run", TimeStamp = DateTime.Now };

            await createLogRecord.DoAsync(resuest);
            return resuest;
        }

        [HttpGet("ActiveJobs")]
        public async Task<List<AutomationDetails>> ActiveJobs([FromServices] IEnumerable<JobSchedule> jobSchedules)
        {
            var result = await Task.Run(() => jobSchedules
                .Select(x => new AutomationDetails 
                { 
                    JobName = x.JobType.FullName,
                    CronExpression = x.CronExpression
                }).ToList());

            return result;
        }
    }
}
