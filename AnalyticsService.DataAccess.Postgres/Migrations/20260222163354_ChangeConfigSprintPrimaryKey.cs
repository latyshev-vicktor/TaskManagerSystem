using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalyticsService.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConfigSprintPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SprintTaskAnalytics_SprintAnalytics_SprintId",
                table: "SprintTaskAnalytics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SprintAnalytics",
                table: "SprintAnalytics");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SprintAnalytics");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SprintAnalytics",
                table: "SprintAnalytics",
                column: "SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_SprintTaskAnalytics_SprintAnalytics_SprintId",
                table: "SprintTaskAnalytics",
                column: "SprintId",
                principalTable: "SprintAnalytics",
                principalColumn: "SprintId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SprintTaskAnalytics_SprintAnalytics_SprintId",
                table: "SprintTaskAnalytics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SprintAnalytics",
                table: "SprintAnalytics");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SprintAnalytics",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SprintAnalytics",
                table: "SprintAnalytics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SprintTaskAnalytics_SprintAnalytics_SprintId",
                table: "SprintTaskAnalytics",
                column: "SprintId",
                principalTable: "SprintAnalytics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
