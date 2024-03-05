using DWShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Infrastructure.Context
{
    public class AuditableContext : DbContext
    {
        
        public DbSet<Audit> Audit { get; set; }
    }

}
