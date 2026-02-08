using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnalyticsService.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Insights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false),
                    Confidence = table.Column<double>(type: "double precision", nullable: false),
                    SprintId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SprintAnalytics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    SprintId = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TotalTasks = table.Column<int>(type: "integer", nullable: false),
                    CompletedTasks = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintAnalytics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SprintTaskAnalytics",
                columns: table => new
                {
                    TaskId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SprintId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintTaskAnalytics", x => x.TaskId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Insights_SprintId_UserId_Type",
                table: "Insights",
                columns: new[] { "SprintId", "UserId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_SprintAnalytics_UserId_SprintId",
                table: "SprintAnalytics",
                columns: new[] { "UserId", "SprintId" });

            migrationBuilder.CreateIndex(
                name: "IX_SprintTaskAnalytics_TaskId_SprintId",
                table: "SprintTaskAnalytics",
                columns: new[] { "TaskId", "SprintId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insights");

            migrationBuilder.DropTable(
                name: "SprintAnalytics");

            migrationBuilder.DropTable(
                name: "SprintTaskAnalytics");
        }
    }
}
