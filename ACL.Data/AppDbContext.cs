using ACL.Abstraction.MetaData;
using ACL.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace ACL.Data
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;

        private static string GetConnString(IDbMeta option)
        {
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = option.Host,
                Username = option.UserName,
                Database = option.Database,
                Password = option.Password,
                CommandTimeout = 500,
                ApplicationName = "ACL",
                MaxPoolSize = 100,
                MinPoolSize = 1
            };
            return builder.ConnectionString;
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, IDbMeta dbMetaOptions) : base(options)
        {
            _connectionString = GetConnString(dbMetaOptions);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserCredentialConfiguration());
        }
    }
}