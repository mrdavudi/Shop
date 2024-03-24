using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    public interface IRepository<TKey, T> where T : class
    {
        public void Create(T entity);
        public T Get(TKey id);
        List<T> Get();
        bool Exist(Expression<Func<T, bool>> expression);
        void SaveChange();
    }
}
