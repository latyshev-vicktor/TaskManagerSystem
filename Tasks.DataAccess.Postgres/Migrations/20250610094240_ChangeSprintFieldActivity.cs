using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tasks.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSprintFieldActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Targets_Sprints_SprintId",
                table: "Targets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sprint_FieldActivities",
                table: "Sprint_FieldActivities");

            migrationBuilder.RenameColumn(
                name: "SprintId",
                table: "Targets",
                newName: "SprintFieldActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Targets_SprintId",
                table: "Targets",
                newName: "IX_Targets_SprintFieldActivityId");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Sprint_FieldActivities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sprint_FieldActivities",
                table: "Sprint_FieldActivities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sprint_FieldActivities_SprintId",
                table: "Sprint_FieldActivities",
                column: "SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_Sprint_FieldActivities_SprintFieldActivityId",
                table: "Targets",
                column: "SprintFieldActivityId",
                principalTable: "Sprint_FieldActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Targets_Sprint_FieldActivities_SprintFieldActivityId",
                table: "Targets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sprint_FieldActivities",
                table: "Sprint_FieldActivities");

            migrationBuilder.DropIndex(
                name: "IX_Sprint_FieldActivities_SprintId",
                table: "Sprint_FieldActivities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sprint_FieldActivities");

            migrationBuilder.RenameColumn(
                name: "SprintFieldActivityId",
                table: "Targets",
                newName: "SprintId");

            migrationBuilder.RenameIndex(
                name: "IX_Targets_SprintFieldActivityId",
                table: "Targets",
                newName: "IX_Targets_SprintId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sprint_FieldActivities",
                table: "Sprint_FieldActivities",
                columns: new[] { "SprintId", "FieldActivityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_Sprints_SprintId",
                table: "Targets",
                column: "SprintId",
                principalTable: "Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
