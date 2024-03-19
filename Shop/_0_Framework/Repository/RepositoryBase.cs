using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Repository
{
    public class RepositoryBase<TKey, T> : IRepository<TKey, T> where T : class
    {
        private readonly DbContext _dbContext;

        public RepositoryBase(DbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public void Create(T entity)
        {
            _dbContext.Add(entity);
        }

        public bool Exist(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Any(expression);
        }

        public T Get(TKey id)
        {
            return _dbContext.Find<T>(id);
        }

        public List<T> Get()
        {
            return _dbContext.Set<T>().ToList();
        }

        public void SaveChange()
        {
            _dbContext.SaveChanges();
        }
    }
}
