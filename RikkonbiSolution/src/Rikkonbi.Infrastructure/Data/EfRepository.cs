using Microsoft.EntityFrameworkCore;
using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rikkonbi.Infrastructure.Data
{
    public class EfRepository<T> : IRepository<T>, IRepositoryAsync<T> where T : BaseEntity, IAggregateRoot
    {
        protected readonly RikkonbiDbContext _dbContext;

        public EfRepository(RikkonbiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public List<T> ListAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public List<T> List(ISpecification<T> spec)
        {
            return ApplySpecification(spec).ToList();
        }

        public IQueryable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public T GetSingleByCondition(
           Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().FirstOrDefault<T>(expression);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbContext.Set<T>().Where(where).ToList();
        }

        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return _dbContext.Set<T>().Count(where);
        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Count<T>(predicate) > 0;
        }

        public void UpdateAndNotSave(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        #region Async methods

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        #endregion Async methods

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}