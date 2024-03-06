using DWShop.Domain.Contracts;
using DWShop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Infrastructure.Context
{
    public class AuditableContext : IdentityDbContext
    {
        public AuditableContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Audit> Audit { get; set; }
        public DbSet<Catalog> Catalog { get; set; }
        public DbSet<Basket> Basket { get; set; }

        private List<AuditEntry> onBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit
                    || entry.State == EntityState.Detached
                    || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = userId,
                };

                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    var propertyname = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyname] = property.CurrentValue!;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyname] = property.CurrentValue!;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyname] = property.OriginalValue!;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified && property.OriginalValue?.Equals(property.CurrentValue) == false)
                            {
                                auditEntry.ChangedColumns.Add(propertyname);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.NewValues[propertyname] = property.CurrentValue;
                                auditEntry.OldValues[propertyname] = property.OriginalValue;
                            }
                            break;
                    }
                   

                }
            }


        }
        // private Task OnAfterSaveChanges(List<AuditEntry>)

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "User";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "User";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }

}
