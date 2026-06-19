using Application.Common;
using Application.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Infrastructure.ExternalServices.Helper;
namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<CursorPaged<T>> GetPagedCursorAsync<TKey>(Expression<Func<T, TKey>> orderBy, TKey? cursor, int pageSize, Expression<Func<T, bool>>? predicate = null, bool ascending = true)
        {
            var query = _dbSet.AsQueryable();
            if(predicate != null)
            {
                query = query.Where(predicate);
            }
            if(cursor != null)
            {
                if(ascending)
                {
                    query = query.Where(ExpressionHelper.GreaterThan(orderBy, cursor));
                }
                else
                {
                    query = query.Where(ExpressionHelper.LessThan(orderBy, cursor));
                }
            }
            if (ascending)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);
            // Lấy thêm 1 bản ghi để xác định có trang tiếp theo không
            var items = await query.Take(pageSize + 1).ToListAsync();

            bool hasNext = items.Count > pageSize;
            var resultItems = hasNext ? items.Take(pageSize).ToList() : items;

            // Lấy con trỏ cho trang tiếp theo (dựa vào bản ghi cuối cùng của trang hiện tại)
            object? nextCursor = null;
            if (hasNext && resultItems.Any())
            {
                var lastItem = resultItems.Last();
                var compiled = orderBy.Compile();
                nextCursor = compiled(lastItem);
            }

            return new CursorPaged<T>
            {
                Items = resultItems,
                NextCursor = nextCursor,
                HasNextPage = hasNext,
                PageSize = pageSize
            };
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return (T)entity;
        }

    }
}
