using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasks.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddSprintFieldActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sprints_FieldActivities_FieldActivityId",
                table: "Sprints");

            migrationBuilder.DropIndex(
                name: "IX_Sprints_FieldActivityId",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "FieldActivityId",
                table: "Sprints");

            migrationBuilder.CreateTable(
                name: "Sprint_FieldActivities",
                columns: table => new
                {
                    FieldActivityId = table.Column<long>(type: "bigint", nullable: false),
                    SprintId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprint_FieldActivities", x => new { x.SprintId, x.FieldActivityId });
                    table.ForeignKey(
                        name: "FK_Sprint_FieldActivities_FieldActivities_FieldActivityId",
                        column: x => x.FieldActivityId,
                        principalTable: "FieldActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sprint_FieldActivities_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sprint_FieldActivities_FieldActivityId",
                table: "Sprint_FieldActivities",
                column: "FieldActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sprint_FieldActivities");

            migrationBuilder.AddColumn<long>(
                name: "FieldActivityId",
                table: "Sprints",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
    }
}
