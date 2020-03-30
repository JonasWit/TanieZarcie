using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Application.Crawlers;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Automations
{
    public class AutomationController
    {
        private readonly CrawlersCommander _crawlersCommander;
        private readonly AutomationManager _automationManager;

        public AutomationController(AutomationManager automationManager, CrawlersCommander crawlersCommander)
        {
            _crawlersCommander = crawlersCommander;
            _automationManager = automationManager;
        }

        public async Task SetUpScheduler()
        {
            await _automationManager.CreateScheduler();
        }

        public async Task ScheduleCrawlersAutomation()
        {
            await _automationManager.ScheduleCrawlersAutomationJob(_crawlersCommander);
        }

        public int GetCounter() => _automationManager.Counter;

        public bool CheckScheduler() => _automationManager.SchedulerPresent;
     
    }
}
