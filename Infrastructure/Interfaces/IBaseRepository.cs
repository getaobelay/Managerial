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
        Task<IEnumerable<TDto>> GetAllAsync(IConfigurationProvider configuration);

        Task<IEnumerable<TDto>> FindAsync(Expression<Func<TEntity, bool>> expression, IConfigurationProvider configuration);

        Task<TDto> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, IConfigurationProvider configuration);

        Task<bool> DeleteAsync(TEntity entityToDelete);

        Task<bool> DeleteAsync(int Id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entityToUpdate, TEntity exists);
    }
}