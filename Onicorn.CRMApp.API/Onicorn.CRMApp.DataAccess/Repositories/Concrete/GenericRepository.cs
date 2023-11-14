using Microsoft.EntityFrameworkCore;
using Onicorn.CRMApp.DataAccess.Contexts.EntityFramework;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.DataAccess.Repositories.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly AppDbContext _appDbContext;
        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<T> AsNoTrackingGetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _appDbContext.Set<T>().Where(filter).AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public T Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _appDbContext.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _appDbContext.Set<T>().Where(filter).SingleOrDefaultAsync(filter);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQuery()
        {
            return _appDbContext.Set<T>().AsQueryable();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
            return entity;
        }
    }
}
