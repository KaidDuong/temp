using Rikkonbi.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rikkonbi.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        T GetById(int id);

        List<T> ListAll();

        List<T> List(ISpecification<T> spec);

        IQueryable<T> List(Expression<Func<T, bool>> predicate);

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void UpdateAndNotSave(T entity);

        T GetSingleByCondition(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}