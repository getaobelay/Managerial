using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Common;
using Infrastructure.Context;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class DataRepository<TEntity, TDto> : IDataRepository<TEntity, TDto>, IDisposable
        where TEntity : AuditableEntity, new()
        where TDto: class, new()
    {
        private DbSet<TEntity> _entities;
        private bool _isDisposed;

        public DataRepository(IUnitOfWorkRepository<ManagerialDbContext> unitOfWork)
            : this(unitOfWork.Context)
        {
            UnitOfWork = unitOfWork;
        }

        public DataRepository(ManagerialDbContext context)
        {
            _isDisposed = true;
            Context = context;
        }

        public virtual ManagerialDbContext Context { get; set; }

        protected virtual DbSet<TEntity> Entities => _entities ??= Context.Set<TEntity>();
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IUnitOfWorkRepository<ManagerialDbContext> UnitOfWork { get; }
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            try
            {
                if (entity != null)
                {
                    await Entities.AddAsync(entity);

                    if (Context == null || _isDisposed)
                        Context = new ManagerialDbContext();

                    return entity;
                }
                else
                    throw new ArgumentNullException("object is not set");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task BulkInsertAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("object is not set");
            }
            else
            {
                using (var entity = entities.GetEnumerator())
                {
                    while (entity.MoveNext())
                    {
                        Context.Entry(entity.Current).State = EntityState.Added;
                    }
                }

                Context.Set<TEntity>().AddRange(entities);
                await Context.SaveChangesAsync();
            }
        }
        public virtual async void Dispose()
        {
            if (Context != null)
                await Context.DisposeAsync();
            _isDisposed = true;
        }
        [Obsolete]
        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, IConfigurationProvider configuration)
        {
            var query = Entities.IncludeAll().AsQueryable();

           return await query.Where(expression)
                             .ToListAsync();

        }
        [Obsolete]
        public async Task<IEnumerable<TEntity>> GetAllAsync(IConfigurationProvider configuration)
        {
            var query = Entities.IncludeAll().AsQueryable();

            return await query.ToListAsync();
        }
        [Obsolete]
        public virtual async Task<TEntity> GetAsync(object Id, IConfigurationProvider configuration)
        {
            var query = Entities.IncludeAll().AsQueryable();

            return await query.SingleOrDefaultAsync(e => e.GetType()
                                                          .GetProperty("Id")
                                                          .GetValue(e, null).Equals(Id));
                           

        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entityUpdate, TEntity exists)
        {
            try
            {
                if (entityUpdate == null)
                    throw new ArgumentNullException($"{nameof(TEntity)} not found ");

                if (Context == null || _isDisposed)
                    Context = new ManagerialDbContext();

                Context.Entry(entityUpdate).State = EntityState.Modified;
                Context.Entry(exists).CurrentValues.SetValues(entityUpdate);

                return await Task.FromResult(entityUpdate);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException();
            }
        }
        [Obsolete]
        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression, IConfigurationProvider configuration)
        {
            var query = Entities.IncludeAll().AsQueryable();

            return await query.Where(expression)
                              .SingleOrDefaultAsync();

        }
        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Entities.Where(expression)
                                 .SingleOrDefaultAsync();

        }
        public virtual async Task<bool> DeleteAsync(TEntity entityToDelete)
        {
            try
            {
                if (entityToDelete == null)
                    throw new ArgumentNullException(nameof(entityToDelete));

                if (Context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    Entities.Attach(entityToDelete);
                }

                Entities.Remove(entityToDelete);

                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                TEntity entityToDelete = await SingleOrDefaultAsync(e => e.Id == Id);

                if (entityToDelete == null)
                    throw new ArgumentNullException(nameof(entityToDelete));
                else
                {
                    if (Context == null || _isDisposed)
                        Context = new ManagerialDbContext();

                    return await DeleteAsync(entityToDelete);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}