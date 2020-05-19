using System.Linq;
using System.Reflection;
using WEB.Shop.Application;
using WEB.Shop.Application.Automations;
using WEB.Shop.DataBase;
using WEB.Shop.Domain.Infrastructure;
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
            @this.AddTransient<IUsersManager, UsersManager>();
            @this.AddTransient<ICrawlersDataBaseManager, CrawlersDataBaseManager>();

            @this.AddScoped<ISessionManager, SessionManager>();

            @this.AddSingleton<AppSettingsService>();
            @this.AddSingleton<AutomationManager>();

            return @this;
        }
    }
}
