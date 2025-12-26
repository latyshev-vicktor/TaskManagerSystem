using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasks.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sprints_UserId",
                table: "Sprints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldActivities_UserId",
                table: "FieldActivities",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sprints_UserId",
                table: "Sprints");

            migrationBuilder.DropIndex(
                name: "IX_FieldActivities_UserId",
                table: "FieldActivities");
        }
    }
}
