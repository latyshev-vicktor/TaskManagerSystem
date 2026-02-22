using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalyticsService.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkagesTaskForSprint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedTasks",
                table: "SprintAnalytics");

            migrationBuilder.DropColumn(
                name: "TotalTasks",
                table: "SprintAnalytics");

            migrationBuilder.CreateIndex(
                name: "IX_SprintTaskAnalytics_SprintId",
                table: "SprintTaskAnalytics",
                column: "SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_SprintTaskAnalytics_SprintAnalytics_SprintId",
                table: "SprintTaskAnalytics",
                column: "SprintId",
                principalTable: "SprintAnalytics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SprintTaskAnalytics_SprintAnalytics_SprintId",
                table: "SprintTaskAnalytics");

            migrationBuilder.DropIndex(
                name: "IX_SprintTaskAnalytics_SprintId",
                table: "SprintTaskAnalytics");

            migrationBuilder.AddColumn<int>(
                name: "CompletedTasks",
                table: "SprintAnalytics",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalTasks",
                table: "SprintAnalytics",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
