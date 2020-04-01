using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.Threading.Tasks;
using WEB.Shop.Application.Crawlers;

namespace WEB.Shop.Application.Automations
{
    public class AutomationManager
    {
        public IScheduler Scheduler { get; set; }
        public bool SchedulerPresent { get { if (Scheduler != null) { return true; } else { return false; } } }

        public async Task CreateScheduler()
        {
            var properties = new NameValueCollection
            {
                ["quartz.scheduler.instanceName"] = "DataFeedAutomation",
                ["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz",
                ["quartz.threadPool.threadCount"] = "3",
                ["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore, Quartz",
            };

            var schedulerFactory = new StdSchedulerFactory(properties);
            var scheduler = schedulerFactory.GetScheduler().Result;
            await scheduler.Start();
            Scheduler = scheduler;
        }

        public async Task DisposeScheduler()
        {
            if (Scheduler != null) await Scheduler.Shutdown(); Scheduler = null;     
        }

        public async Task ScheduleCrawlersAutomationJob(CrawlersCommander crawlersCommander) => 
            await Scheduler.ScheduleJob(CreateCrawlerAutomationJob(crawlersCommander), CreateCrawlersAutomationTrigger());

        private IJobDetail CreateCrawlerAutomationJob(CrawlersCommander crawlersCommander)
        {
            var jobDataMap = new JobDataMap { ["CrawlersCommander"] = crawlersCommander };

            return JobBuilder.Create<CrawlerJob>()
                .WithIdentity("MainCrawlerJob", "CrawlersAutomation")
                .UsingJobData(jobDataMap)
                .Build();
        }

        private ITrigger CreateCrawlersAutomationTrigger() =>
            TriggerBuilder.Create()
                .WithIdentity("Crawlers Trigger")
                .StartNow()
                    .WithSchedule(CronScheduleBuilder
                    .DailyAtHourAndMinute(03, 30))
                //.WithSimpleSchedule(x => x
                //    .WithIntervalInSeconds(2)
                //    .RepeatForever())
                .Build();
    }
}
