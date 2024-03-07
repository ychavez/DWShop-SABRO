using DWShop.Domain.Contracts;
using DWShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DWShop.Infrastructure.Repositories
{
    public class RepositoryAsync<T, TId>(AuditableContext context) 
        : IRepositoryAsync<T, TId> where T : AuditableEntity<TId>
    {

        public DbSet<T> Entities { get; }

        public async Task<T> GetByIdAsync(TId id)
            => await context.Set<T>().FindAsync(id);

        public async Task<List<T>> GetAllAsync()
            => await context.Set<T>().ToListAsync();

        public async Task<List<T>> GetPagedAsync(int pageNumber, int pageSize,
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            params string[] IncludeArgs)
        {

            IQueryable<T> query = context.Set<T>();
            //agregamos los join
            query = IncludeArgs.Aggregate(query, (current, itemInclude) =>
            current.Include(itemInclude));
            //si hubo predicado (where) lo agregamos
            if (predicate is not null)
                query = query.Where(predicate);
            //Paginacion
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            //Regresamos ordenado o no
            return await (orderBy is not null ? orderBy(query) : query).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
            => await context.SaveChangesAsync();
    }
}
