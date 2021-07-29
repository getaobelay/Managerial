using Infrastructure.Interfaces;

namespace Infrastructure.Context
{
    public class DataContext : IDataContext
    {
        public DataContext(IUnitOfWorkRepository<ManagerialDbContext> unitOfWork, ICurrentUser currentUser)
        {
            UnitOfWork = unitOfWork;
            CurrentUser = currentUser;
        }

        public IUnitOfWorkRepository<ManagerialDbContext> UnitOfWork { get; set; }
        public ICurrentUser CurrentUser { get; set; }
    }
}