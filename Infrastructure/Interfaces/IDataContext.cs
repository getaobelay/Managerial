using Infrastructure.Context;

namespace Infrastructure.Interfaces
{
    public interface IDataContext
    {
        IUnitOfWorkRepository<ManagerialDbContext> UnitOfWork { get; set; }
        ICurrentUser CurrentUser { get; set; }
    }
}