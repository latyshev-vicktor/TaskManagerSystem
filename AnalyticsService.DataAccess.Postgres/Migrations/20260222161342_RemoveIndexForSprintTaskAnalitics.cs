using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalyticsService.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIndexForSprintTaskAnalitics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SprintTaskAnalytics_TaskId_SprintId",
                table: "SprintTaskAnalytics");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SprintTaskAnalytics_TaskId_SprintId",
                table: "SprintTaskAnalytics",
                columns: new[] { "TaskId", "SprintId" });
        }
    }
}
