using Data.Abstraction.MetaData;
using Data.Configurations;
using Data.Configurations.Base;
using Data.Extensions;
using Data.Models.Entities;
using Data.Models.Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;
using Shared.Helpers;

namespace Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IDbMeta dbMeta) : DbContext(options)
    {
        private readonly string _connectionString = GetConnString(dbMeta);

        private static string GetConnString(IDbMeta meta)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Username = meta.Username,
                Password = meta.Password,
                Host = meta.Host,
                Port = meta.Port,
                Database = meta.Database,
                ApplicationName = "ACL",
                CommandTimeout = 500,
                MaxPoolSize = 100,
                MinPoolSize = 1
            };
            return builder.ConnectionString;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseNpgsql(_connectionString,x=>x.MigrationsHistoryTable("__AppMigrationsHistory", "public"))
                    // .UseSnakeCaseNamingConvention()
                    .UseLazyLoadingProxies()
                    .ConfigureWarnings(w=>w.Ignore(CoreEventId.DetachedLazyLoadingWarning));
        }

        public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            using var tx = TransactionScopeHelper.GetInstance();
            ChangeTracker.DetectChanges();
            var now = DateTime.UtcNow;
            var userGuid = this.GetService<IHttpContextAccessor>().HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "uid")?.Value ?? "";
            var user = await Set<User>().Where(x => x.IsDeleted == false && x.Guid == userGuid).FirstOrDefaultAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().Where(entry => entry.State is EntityState.Added))
            {
                entry.Entity.SaveCreationTime(now);
                entry.Entity.SaveUpdateTime(now);
                entry.Entity.SetAuditUserDetails(user, user);
            }
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().Where(e => e.State is EntityState.Modified))
            {
                entry.Entity.SaveUpdateTime(now);
                entry.Entity.SetAuditUserDetails(user);
            }

            var entries = ChangeTracker.Entries<AuditableEntity>().Where(e => e.State is EntityState.Added or EntityState.Deleted or EntityState.Modified).ToList();
            await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            var logEntries = entries.Select(e => new AuditEntry(
                    userGuid,
                    e.State.ToString(),
                    e.Entity.GetType().Name,
                    0,
                    e.OriginalValues.ToDictionary(),
                    e.CurrentValues.ToDictionary()
                )
            );

            await Set<AuditEntry>().AddRangeAsync(logEntries, cancellationToken).ConfigureAwait(false);
            var returnValue = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            tx.Complete();

            return returnValue;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EntityBase>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<KeylessEntity>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration(new AuditEntryConfiguration());

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<decimal>().HaveColumnType("numeric(30,4)");
            configurationBuilder.Properties<decimal?>().HaveColumnType("numeric(30,4)");
        }
    }
}
