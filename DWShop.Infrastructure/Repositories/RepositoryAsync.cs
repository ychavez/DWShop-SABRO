using DWShop.Domain.Contracts;
using DWShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Infrastructure.Repositories
{
    public class RepositoryAsync<T,TId>(AuditableContext context) 
        where T: IAuditableEntity<TId>
    {

        public DbSet<T> Entities { set; }

        public async Task<T> GetByIdAsync(TId id)
            => await context.Set<T>().FindAsync(id);

        public async Task<List<T>> GetAllAsync()
            => await context.Set<T>().ToListAsync();

    }
}
