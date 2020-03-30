using Quartz;
using System.Threading.Tasks;

namespace WEB.Shop.Application.Automations
{
    public class CrawlerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;

            await Task.FromResult(0);
        }
    }
}
