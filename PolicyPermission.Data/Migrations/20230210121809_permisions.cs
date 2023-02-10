using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolicyPermission.Data.Migrations
{
    /// <inheritdoc />
    public partial class permisions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Permissions",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Permissions",
                table: "roles",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "roles");
        }
    }
}
