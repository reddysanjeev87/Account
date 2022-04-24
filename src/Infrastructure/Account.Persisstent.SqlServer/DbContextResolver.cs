
using Account.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Persisstent.SqlServer
{
    public static class DbContextResolver
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(connectionString));
               

            //services.AddDbContext<JobSchedulerContext>(options =>
            //   options.UseSqlServer("Server=TESTSQL.julyservices.local;Database=JobScheduler;Integrated Security=True;MultipleActiveResultSets=True"))
            //   .AddDbContext<TPAManagerDbContext>(options =>
            //   options.UseSqlServer("Server=TESTSQL.julyservices.local;Database=TPAManager_Test;Integrated Security=True;MultipleActiveResultSets=True"));

            services.AddScoped<IUnitOfWork<AccountContext>, UnitOfWork<AccountContext>>();
            
            services.AddScoped<IRepositoryResolver, RepositoryResolver>();
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

           
            return services;
        }
    }
}
