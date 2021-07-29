using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IDataRepository<TEntity, TDto> : IBaseRepository<TEntity, TDto>
        where TEntity : AuditableEntity, new()
    {
    }
}