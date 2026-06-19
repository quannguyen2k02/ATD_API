using Application.Common;
using System.Linq.Expressions;

namespace Application.IRepositories
{
    public interface IGenericRepository<T> where T: class
    {
        public Task<T> GetByIDAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task<List<T>> Find(Expression<Func<T, bool>> predicate);
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<CursorPaged<T>> GetPagedCursorAsync<TKey>(
        Expression<Func<T, TKey>> orderBy,
        TKey? cursor,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        bool ascending = true
    );
    }
}
