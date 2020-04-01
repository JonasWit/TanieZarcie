using Quartz;
using System.Threading.Tasks;
using WEB.Shop.Application.Crawlers;

namespace WEB.Shop.Application.Automations
{
    public class CrawlerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var commander = (CrawlersCommander)dataMap["CrawlersCommander"];

            await commander.RunEngineAsync();
            await commander.UpdateAllData();
        }
    }
}
