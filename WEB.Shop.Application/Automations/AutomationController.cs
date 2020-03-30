using System.Threading.Tasks;
using WEB.Shop.Application.Crawlers;

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

        public async Task CreateScheduler() => await _automationManager.CreateScheduler();

        public async Task DisposeScheduler() => await _automationManager.DisposeScheduler();

        public async Task ScheduleCrawlersAutomation() => await _automationManager.ScheduleCrawlersAutomationJob(_crawlersCommander);

        public bool CheckScheduler() => _automationManager.SchedulerPresent;
     
    }
}
