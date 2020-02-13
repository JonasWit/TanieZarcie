using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Security.Claims;
using WEB.Shop.DataBase;

namespace WEB.Shop.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                using var scope = host.Services.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                context.Database.EnsureCreated();

                if (context.Users.Any())
                {
                    var adminUser = new IdentityUser()
                    {
                        UserName = "AdminTZ"
                    };

                    var managerUser = new IdentityUser()
                    {
                        UserName = "ManagerTZ"
                    };

                    userManager.CreateAsync(adminUser, "Iz@321637").GetAwaiter().GetResult();
                    userManager.CreateAsync(managerUser, "@dmin321637").GetAwaiter().GetResult();

                    var adminClaim = new Claim("Role", "AdminTZ");
                    var managerClaim = new Claim("Role", "ManagerTZ");

                    userManager.AddClaimAsync(adminUser, adminClaim).GetAwaiter().GetResult();
                    userManager.AddClaimAsync(managerUser, managerClaim).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }   
                
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
