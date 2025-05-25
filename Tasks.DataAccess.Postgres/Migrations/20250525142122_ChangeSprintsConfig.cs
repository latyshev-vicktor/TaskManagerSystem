using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasks.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSprintsConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Sprints",
                newName: "StatusName");

            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                table: "Sprints",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusDescription",
                table: "Sprints");

            migrationBuilder.RenameColumn(
                name: "StatusName",
                table: "Sprints",
                newName: "Status");
        }
    }
}
