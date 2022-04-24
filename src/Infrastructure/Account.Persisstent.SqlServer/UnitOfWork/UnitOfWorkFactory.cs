using Account.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Persisstent.SqlServer
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        IServiceScopeFactory serviceScopeFactory;
        public UnitOfWorkFactory(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public IUnitOfWork Create(DbContextConfig dbContextConfig= DbContextConfig.Account)
        {
            return serviceScopeFactory.CreateScope().ServiceProvider.GetService<IUnitOfWork<AccountContext>>();
        }
    }
}
