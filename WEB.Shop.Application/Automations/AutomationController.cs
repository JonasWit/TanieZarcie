using Quartz;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Shop.Application.Crawlers;

namespace WEB.Shop.Application.Automations
{
    [ScopedService]
    public class AutomationController
    {
        private readonly CrawlersCommander _crawlersCommander;
        private readonly AutomationManager _automationManager;

        public AutomationController(AutomationManager automationManager, CrawlersCommander crawlersCommander)
        {
            _crawlersCommander = crawlersCommander;
            _automationManager = automationManager;
        }

        public async Task CreateScheduler() => await _automationManager.CreateScheduler();

        public async Task DisposeScheduler() => await _automationManager.DisposeScheduler();

        public async Task ScheduleCrawlersAutomation() => await _automationManager.ScheduleCrawlersAutomationJob(_crawlersCommander);

        public bool CheckScheduler() => _automationManager.SchedulerPresent;

        public List<Response> GetActiveJobs()
        {
            var result = new List<Response>();

            if (!_automationManager.SchedulerPresent) return result;

            var scheduler = _automationManager.Scheduler;
            var allTriggerKeys = scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());

            foreach (var triggerKey in allTriggerKeys.Result)
            {
                var triggerDetails = scheduler.GetTrigger(triggerKey);
                var jobDetails = scheduler.GetJobDetail(triggerDetails.Result.JobKey);

                result.Add(new Response
                {
                    JobKey = triggerDetails.Result.JobKey.Name,
                    TriggerName = triggerDetails.Result.Key.Name,
                    JobStatus = jobDetails.Status.ToString(),
                    JobInitialized = triggerDetails.Result.StartTimeUtc.DateTime,
                    JobName = jobDetails.Result.Key.Name,
                    JobGroup = jobDetails.Result.Key.Group
                });
            }

             return result;
        }

        public class Response
        {
            public string JobKey { get; set; }
            public string JobName { get; set; }
            public string JobGroup { get; set; }
            public DateTime JobInitialized { get; set; }
            public string JobSchedule { get; set; }
            public string JobStatus { get; set; }
            public string TriggerName { get; set; }
        }
     
    }
}
