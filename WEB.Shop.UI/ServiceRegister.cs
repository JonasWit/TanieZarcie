using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Linq;
using System.Reflection;
using WEB.Shop.Application;
using WEB.Shop.Application.Automations;
using WEB.Shop.DataBase;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.UI.Automation;
using WEB.Shop.UI.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicaitonServices(this IServiceCollection @this)
        {
            var transientServiceType = typeof(TransientService);
            var scopedServiceType = typeof(ScopedService);
            var definedTypes = transientServiceType.Assembly.DefinedTypes;

            var transientServices = definedTypes
                .Where(x => x.GetTypeInfo().GetCustomAttribute<TransientService>() != null);

            var scopedServices = definedTypes
                .Where(x => x.GetTypeInfo().GetCustomAttribute<ScopedService>() != null);

            foreach (var service in transientServices)
            {
                @this.AddTransient(service);
            }

            foreach (var service in scopedServices)
            {
                @this.AddScoped(service);
            }

            @this.AddTransient<IStockManager, StockManager>();
            @this.AddTransient<IProductManager, ProductManager>();
            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddTransient<IFileManager, FileManager>();
            @this.AddTransient<INewsManager, NewsManager>();
            @this.AddTransient<ILogManager, LogManager>();
            @this.AddTransient<ICrawlersDataBaseManager, CrawlersDataBaseManager>();

            @this.AddScoped<ISessionManager, SessionManager>();

            @this.AddSingleton<AppSettingsService>();
            @this.AddSingleton<AutomationManager>();

            // Add Quartz services
            @this.AddSingleton<IJobFactory, SingletonJobFactory>();
            @this.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            @this.AddSingleton<JobWakeUpCall>();
            @this.AddSingleton(new JobSchedule(
                jobType: typeof(JobWakeUpCall),
                cronExpression: "0 0/1 * * * ?"));

            @this.AddSingleton<JobRunCrawlers>();
            @this.AddSingleton(new JobSchedule(
                jobType: typeof(JobRunCrawlers),
                cronExpression: "0 15 4 ? * *"));

            @this.AddHostedService<QuartzHostedService>();

            return @this;
        }
    }
}
