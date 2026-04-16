using LB_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace LB_POS.Infrastructure.BaseInfrastructure
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        #region Vars / Props

        protected readonly ApplicationDBContext _dbContext;

        #endregion

        #region Constructor(s)
        public GenericRepositoryAsync(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion


        #region Methods

        #endregion

        #region Actions
        public virtual async Task<T> GetByIdAsync(int id)
        {

            return await _dbContext.Set<T>().FindAsync(id);
        }


        public IQueryable<T> GetTableNoTracking()
        {
            return _dbContext.Set<T>().AsNoTracking().AsQueryable();
        }


        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();

        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }



        public IDbContextTransaction BeginTransaction()
        {


            return _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dbContext.Database.CommitTransaction();

        }

        public void RollBack()
        {
            _dbContext.Database.RollbackTransaction();

        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _dbContext.Set<T>().AsQueryable();

        }

        public virtual async Task UpdateRangeAsync(ICollection<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsUniqueAsync(
                Expression<Func<T, bool>> predicate,
                int? excludeId = null,
                CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<T>().AsNoTracking().Where(predicate);

            if (excludeId.HasValue)
            {
                // نفترض أن كل Entity يحتوي على Id من نوع Guid
                var param = Expression.Parameter(typeof(T), "x");
                var idProp = Expression.PropertyOrField(param, "Id");
                var notEqual = Expression.NotEqual(idProp, Expression.Constant(excludeId.Value));
                var lambda = Expression.Lambda<Func<T, bool>>(notEqual, param);
                query = query.Where(lambda);
            }

            return !await query.AnyAsync(cancellationToken);
        }
        #endregion

    }
}
