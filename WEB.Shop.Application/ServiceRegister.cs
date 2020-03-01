using Microsoft.Extensions.DependencyInjection;
using WEB.Shop.Application.ApplicationCore;
using WEB.Shop.Application.Cart;
using WEB.Shop.Application.Crawlers;
using WEB.Shop.Application.OrdersAdmin;
using WEB.Shop.Application.Products;
using WEB.Shop.Application.UsersAdmin;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicaitonServices(this IServiceCollection @this)
        {
            @this.AddTransient<AddCustomerInformation>();
            @this.AddTransient<AddToCart>();
            @this.AddTransient<GetCart>();
            @this.AddTransient<GetCustomerInformation>();
            @this.AddTransient<RemoveFromCart>();
            @this.AddTransient<GetProducts>();

            @this.AddTransient<CreateUser>();

            @this.AddTransient<WEB.Shop.Application.Cart.GetOrder>();
            @this.AddTransient<WEB.Shop.Application.OrdersAdmin.GetOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();

            @this.AddSingleton<AppCore>();
            @this.AddSingleton<CrawlersCommander>();

            return @this;
        }
    }
}
