using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tasks.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddSprintWeekTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Targets_Sprint_FieldActivities_SprintFieldActivityId",
                table: "Targets");

            migrationBuilder.RenameColumn(
                name: "SprintFieldActivityId",
                table: "Targets",
                newName: "SprintId");

            migrationBuilder.RenameIndex(
                name: "IX_Targets_SprintFieldActivityId",
                table: "Targets",
                newName: "IX_Targets_SprintId");

            migrationBuilder.AddColumn<long>(
                name: "WeekId",
                table: "Tasks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "SprintWeeks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SprintId = table.Column<long>(type: "bigint", nullable: false),
                    WeekNumber = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SprintWeeks_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_WeekId",
                table: "Tasks",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_SprintWeeks_SprintId",
                table: "SprintWeeks",
                column: "SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_Sprints_SprintId",
                table: "Targets",
                column: "SprintId",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_SprintWeeks_WeekId",
                table: "Tasks",
                column: "WeekId",
                principalTable: "SprintWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Targets_Sprints_SprintId",
                table: "Targets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_SprintWeeks_WeekId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "SprintWeeks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_WeekId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "WeekId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "SprintId",
                table: "Targets",
                newName: "SprintFieldActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Targets_SprintId",
                table: "Targets",
                newName: "IX_Targets_SprintFieldActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_Sprint_FieldActivities_SprintFieldActivityId",
                table: "Targets",
                column: "SprintFieldActivityId",
                principalTable: "Sprint_FieldActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
