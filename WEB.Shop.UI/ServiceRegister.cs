using System.Linq;
using System.Reflection;
using WEB.Shop.Application;
using WEB.Shop.Application.Crawlers;
using WEB.Shop.DataBase;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.UI.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicaitonServices(this IServiceCollection @this)
        {
            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;

            var services = definedTypes
                .Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach (var service in services)
            {
                @this.AddTransient(service);
            }

            @this.AddScoped<CrawlersCommander>();
            @this.AddTransient<IStockManager, StockManager>();
            @this.AddTransient<IProductManager, ProductManager>();
            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddTransient<ICrawlersDataBaseManager, CrawlersDataBaseManager>();
            @this.AddScoped<ISessionManager, SessionManager>();

            return @this;
        }
    }
}
