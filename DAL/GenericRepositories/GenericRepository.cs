using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.DatabaseContext;
using DAL.GenericRepositories.IGenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.GenericRepositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly DataContext _ctx;
        public GenericRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            TEntity newEntity = _ctx.CreateProxy<TEntity>();
            _ctx.Entry(newEntity).CurrentValues.SetValues(entity);
            _ctx.Entry(entity).State = EntityState.Detached;
            _ctx.Add(newEntity);
           // await _ctx.SaveChangesAsync();
            return newEntity;
        }

        public async Task<List<TEntity>> AddRange(List<TEntity> entity)
        {
            await _ctx.AddRangeAsync(entity);
            //await _ctx.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            _ctx.Remove(entity);
           // return await _ctx.SaveChangesAsync();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return await _ctx.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<IQueryable<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return (filter == null ? _ctx.Set<TEntity>() : _ctx.Set<TEntity>().Where(filter));
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            TEntity updatedEntity = _ctx.CreateProxy<TEntity>();
            _ctx.Entry(updatedEntity).CurrentValues.SetValues(entity);
            _ctx.Entry(entity).State = EntityState.Detached;
            _ctx.Update(updatedEntity);
           // await _ctx.SaveChangesAsync();
            return updatedEntity;
        }

        public async Task<List<TEntity>> UpdateRange(List<TEntity> entity)
        {
            _ctx.UpdateRange(entity);
           // await _ctx.SaveChangesAsync();
            return entity;
        }

        //public TEntity Add(TEntity entity)
        //{
        //    TEntity newEntity = _ctx.CreateProxy<TEntity>();
        //    _ctx.Entry(newEntity).CurrentValues.SetValues(entity);
        //    _ctx.Entry(entity).State = EntityState.Detached;
        //    _ctx.Add(newEntity);
        //    _ctx.SaveChanges();
        //    return newEntity;
        //}

        //public List<TEntity> AddRange(List<TEntity> entity)
        //{
        //    _ctx.AddRange(entity);
        //    _ctx.SaveChanges();
        //    return entity;
        //}

        //public int Delete(TEntity entity)
        //{
        //    var addedEntry = _ctx.Remove(entity);
        //    return _ctx.SaveChanges();
        //}

        //public List<TEntity> DeleteRange(List<TEntity> entity)
        //{
        //    _ctx.RemoveRange(entity);
        //    _ctx.SaveChanges();
        //    return entity;
        //}

        //public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        //{
        //    return _ctx.Set<TEntity>().FirstOrDefault(filter);
        //}

        //public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        //{
        //    return (filter == null ? _ctx.Set<TEntity>().ToList() : _ctx.Set<TEntity>().Where(filter).ToList());
        //}

        //public TEntity Update(TEntity entity)
        //{
        //    TEntity updatedEntity = _ctx.CreateProxy<TEntity>();
        //    _ctx.Entry(updatedEntity).CurrentValues.SetValues(entity);
        //    _ctx.Entry(entity).State = EntityState.Detached;
        //    _ctx.Update(updatedEntity);
        //    _ctx.SaveChanges();
        //    return updatedEntity;
        //}

        //public List<TEntity> UpdateRange(List<TEntity> entity)
        //{
        //    _ctx.UpdateRange(entity);
        //    _ctx.SaveChanges();
        //    return entity;
        //}
    }
}
