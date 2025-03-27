using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationService.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassWordHash",
                table: "Users",
                newName: "PasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "PassWordHash");
        }
    }
}
