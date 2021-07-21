// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity>
        where TEntity : AuditableEntity, new()
    {
        private readonly ApplicationDbContext _context;

        private ICustomerRepository _customers;
        private IProductRepository _products;
        private IOrdersRepository _orders;
        private IRepository<TEntity> _generic;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerRepository(_context);

                return _customers;
            }
        }

        public IProductRepository Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(_context);

                return _products;
            }
        }

        public IRepository<TEntity> Generic
        {
            get
            {
                if (_generic == null)
                {
                    _generic = new Repository<TEntity>(_context);
                }

                return _generic;
            }
        }

        public IOrdersRepository Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new OrdersRepository(_context);

                return _orders;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}