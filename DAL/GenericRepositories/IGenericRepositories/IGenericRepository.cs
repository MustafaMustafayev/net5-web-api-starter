using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.GenericRepositories.IGenericRepositories
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<T> Get(Expression<Func<T, bool>> filter = null);
        Task<IQueryable<T>> GetList(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task Delete(T entity);
        Task<List<T>> AddRange(List<T> entity);
        Task<List<T>> UpdateRange(List<T> entity);
    }
}
