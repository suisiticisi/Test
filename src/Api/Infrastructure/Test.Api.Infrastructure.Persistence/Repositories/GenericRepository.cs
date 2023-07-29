using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Api.Application.Interfaces.Repositories;
using Test.Api.Domain.Models;

namespace Test.Api.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;

        protected DbSet<TEntity> entity => dbContext.Set<TEntity>();
        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region Insert Methods
        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }
        public virtual int Add(TEntity entity)
        {
            this.entity.Add(entity);
            return dbContext.SaveChanges();
        }

       

        #endregion

        #region Get Methods
        public IQueryable<TEntity> AsQueryable() => entity.AsQueryable();

        public async Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            var entity = await this.entity.ToListAsync();

            await dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity found = await entity.FindAsync(id);
            if (found == null)
            {
                return null;
            }
            if (noTracking)
            {
                dbContext.Entry(found).State = EntityState.Detached;
            }
            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                dbContext.Entry(found).Reference(include).Load();//find ile buylduğum entry i load edebilmek için
            }

            return found;
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate,
            bool noTracking = true,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
           

            IQueryable<TEntity> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }



        #endregion

        #region SaveChanges Methods
        public Task<int> SaveChangeAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public int SaveChange()
        {
            return dbContext.SaveChanges();
        }

       
        #endregion




    }
}
