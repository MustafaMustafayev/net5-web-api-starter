using Microsoft.EntityFrameworkCore;
using Entity.Entities;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Threading;

namespace DAL.DatabaseContext
{
    public class DataContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditProperties();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /* migration commands
        * 
          dotnet ef --startup-project ../API migrations add initial --context DataContext
          dotnet ef --startup-project ../inventar-app.API database update initial --context InventarDbContext
          dotnet ef --startup-project ../API database update --context DataContext
        * 
        * 
        */
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }
        public DbSet<ResponseLog> ResponseLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(m => m.Username).IsUnique();
            modelBuilder.Entity<User>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<RequestLog>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<ResponseLog>().HasQueryFilter(m => !m.IsDeleted);
        }

        private void SetAuditProperties()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AuditableEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    //var originalValues = entityEntry.OriginalValues.ToObject();
                    //var currentValues = entityEntry.CurrentValues.ToObject();

                    ((AuditableEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((AuditableEntity)entityEntry.Entity).CreatedBy = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                    Entry((AuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;

                    if (((AuditableEntity)entityEntry.Entity).IsDeleted)
                    {
                        Entry((AuditableEntity)entityEntry.Entity).Property(p => p.ModifiedBy).IsModified = false;
                        Entry((AuditableEntity)entityEntry.Entity).Property(p => p.ModifiedAt).IsModified = false;

                        ((AuditableEntity)entityEntry.Entity).DeletedAt = DateTime.UtcNow;
                        ((AuditableEntity)entityEntry.Entity).DeletedBy = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                    }
                    else
                    {
                        ((AuditableEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
                        ((AuditableEntity)entityEntry.Entity).ModifiedBy = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
                    }
                }
            }
        }
    }
}
