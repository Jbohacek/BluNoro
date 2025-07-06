using System.Linq.Expressions;
using BluNoro.Core.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BluNoro.Core.Common.Services
{
    public abstract class Repo<T> where T : class , ITable
    {
        public DbContext Context;
        internal DbSet<T> _dbSet;

        protected Repo(DbContext context)
        {
            Context = context;
            _dbSet = context.Set<T>();
        }

        public T GetFirst(Expression<Func<T, bool>> filterExpression) => _dbSet.First(filterExpression);

        public T? GetFirstOrDefault(Expression<Func<T, bool>> filterExpression) =>
            _dbSet.FirstOrDefault(filterExpression);

        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filterExpression) => _dbSet.Where(filterExpression);

        public IEnumerable<T> GetAll(params string[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        public virtual void Add(T item)
        {
            if (item.Id == Guid.Empty)
            {
                item.Id = new Guid();
            }
            Context.Add(item);
        }

        public void Remove(T item)
        {
            Context.Remove(item);
        }

        public virtual void Update(T item)
        {
            Context.Update(item);
        }

    }
}
