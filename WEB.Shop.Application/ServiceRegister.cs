using System;
using System.Collections.Generic;
using System.Text;
using WEB.Shop.Application.ApplicationCore;
using WEB.Shop.Application.OrdersAdmin;
using WEB.Shop.Application.UsersAdmin;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicaitonServices(this IServiceCollection @this)
        {
            @this.AddTransient<CreateUser>();

            @this.AddTransient<GetOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();

            @this.AddSingleton<AppCore>();


            return @this;
        }
    }
}
