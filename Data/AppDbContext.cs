using Data.Abstraction.MetaData;
using Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;

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
                    .UseSnakeCaseNamingConvention()
                    .UseLazyLoadingProxies()
                    .ConfigureWarnings(w=>w.Ignore(CoreEventId.DetachedLazyLoadingWarning));
        }

        public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) => await base.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public");

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
