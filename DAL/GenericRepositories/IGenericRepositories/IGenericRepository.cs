using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.GenericRepositories.IGenericRepositories
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetAsNoTrackingAsync(Expression<Func<T, bool>> filter = null);
        IQueryable<T> GetList(Expression<Func<T, bool>> filter = null);
        IQueryable<T> GetAsNoTrackingList(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<List<T>> AddRangeAsync(List<T> entity);
        List<T> UpdateRange(List<T> entity);
    }
}
