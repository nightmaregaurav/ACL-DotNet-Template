using System.Reflection;
using Data.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Seeders
{
    internal static class UserSeeder
    {
        public static void SeedDefaultData(this EntityTypeBuilder<User> builder)
        {
            var createdOn = new DateTime(2023, 3, 14, 15, 38, 2, 740, DateTimeKind.Utc).AddTicks(1710);

            var data = new List<User> { };

            data = data.Select(x =>
            {
                x.GetType().GetProperty(nameof(User.CreatedAt), BindingFlags.NonPublic)?.SetValue(x, new DateTime(2023, 3, 14, 15, 38, 2, 740, DateTimeKind.Utc).AddTicks(1710));
                return x;
            }).ToList();

            builder.HasData(data);
        }
    }
}
