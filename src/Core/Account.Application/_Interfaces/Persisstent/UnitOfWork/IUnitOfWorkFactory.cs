namespace Account.Application
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(DbContextConfig dbContextConfig = DbContextConfig.Account);
    }
   
}
