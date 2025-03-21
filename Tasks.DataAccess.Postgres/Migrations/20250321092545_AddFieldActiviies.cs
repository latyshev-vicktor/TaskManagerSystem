using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tasks.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldActiviies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FieldActivityId",
                table: "Sprints",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "FieldActivities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldActivities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_FieldActivityId",
                table: "Sprints",
                column: "FieldActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprints_FieldActivities_FieldActivityId",
                table: "Sprints",
                column: "FieldActivityId",
                principalTable: "FieldActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sprints_FieldActivities_FieldActivityId",
                table: "Sprints");

            migrationBuilder.DropTable(
                name: "FieldActivities");

            migrationBuilder.DropIndex(
                name: "IX_Sprints_FieldActivityId",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "FieldActivityId",
                table: "Sprints");
        }
    }
}
