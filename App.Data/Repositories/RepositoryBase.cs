using App.Data;
using App.Models.DbEntities;
using App.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using App.Infrastructure;

namespace App.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;
        public RepositoryBase(ApplicationDbContext dbContext = null)
        {
            _dbContext = dbContext;
        }

        public virtual bool BulkInsert(List<T> entities)
        {
            _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            try
            {
                _dbContext.Set<T>().AddRange(entities);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().FirstOrDefault(match);
        }

        public virtual List<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().Where(match).ToList();
        }

        public virtual async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _dbContext.Set<T>().Where(match).ToListAsync();
        }

        public virtual List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual T Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public virtual T Update(T entity)
        {
            if (entity == null)
                return null;
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
