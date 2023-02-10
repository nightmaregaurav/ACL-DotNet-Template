using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using PolicyPermission.Abstraction.MetaData;
using PolicyPermission.Data.Configurations;

namespace PolicyPermission.Data
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
                ApplicationName = "PolicyPermission",
                MaxPoolSize = 100,
                MinPoolSize = 1
            };
            return builder.ConnectionString;
        }

        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<IDbMeta> dbMetaOptions)
        {
            _connectionString = GetConnString(dbMetaOptions.Value);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}