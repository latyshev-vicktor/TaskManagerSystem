using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalyticsService.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueForInsights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Insights_SprintId_UserId_Type",
                table: "Insights");

            migrationBuilder.CreateIndex(
                name: "IX_Insights_SprintId_UserId_Type",
                table: "Insights",
                columns: new[] { "SprintId", "UserId", "Type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Insights_SprintId_UserId_Type",
                table: "Insights");

            migrationBuilder.CreateIndex(
                name: "IX_Insights_SprintId_UserId_Type",
                table: "Insights",
                columns: new[] { "SprintId", "UserId", "Type" });
        }
    }
}
