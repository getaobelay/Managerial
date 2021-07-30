using AutoMapper;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IBaseRepository<TEntity, TDto>
        where TEntity : AuditableEntity, new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync(IConfigurationProvider configuration);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, IConfigurationProvider configuration);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, IConfigurationProvider configuration);

        Task<bool> DeleteAsync(TEntity entityToDelete);

        Task<bool> DeleteAsync(int Id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entityToUpdate, TEntity exists);
    }
}