using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using StoreApp.Repositories;
using System.Linq.Expressions;

namespace Repositories.Concretes
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly AppDbContext _context;

        protected RepositoryBase(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges ? _context.Set<T>()
                                : _context.Set<T>().AsNoTracking();
        }
        public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChange)
        {
            return trackChange
                ? _context.Set<T>().Where(expression).SingleOrDefault()
                : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
        }
        //-------------------------------------C-U-D-------------------------------------//
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
