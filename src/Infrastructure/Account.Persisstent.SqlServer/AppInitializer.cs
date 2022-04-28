using Account.Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Account.Persisstent.SqlServer
{
    public class AppInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context=serviceScope.ServiceProvider.GetService<AccountContext>();
                if (!context.Countries.Any())
                {
                    context.Countries.AddRange(
                      new Country {Name="Bangladesh" },
                      new Country {Name = "India" }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
