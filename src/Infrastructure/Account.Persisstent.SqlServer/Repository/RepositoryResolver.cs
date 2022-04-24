using Account.Application;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Account.Persisstent.SqlServer
{
    public interface IRepositoryResolver
    {
        IRepository<TEntity> BaseResolve<TEntity>() where TEntity : class;
        TRepository ChildResolve<TRepository>() where TRepository : class;
    }
    public class RepositoryResolver : IRepositoryResolver
    {
        IServiceScopeFactory serviceScopeFactory;
        IServiceProvider serviceProvider;
        public RepositoryResolver(IServiceProvider serviceProvider, IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.serviceProvider = serviceProvider;
        }
        public IRepository<TEntity> BaseResolve<TEntity>() where TEntity : class
        {
            return this.serviceProvider.GetService<IRepository<TEntity>>();
        }
        public TRepository ChildResolve<TRepository>() where TRepository : class
        {
            return this.serviceProvider.GetService<TRepository>();
        }
    }
}
