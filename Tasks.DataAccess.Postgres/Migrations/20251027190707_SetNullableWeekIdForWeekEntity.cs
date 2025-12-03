using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasks.DataAccess.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class SetNullableWeekIdForWeekEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_SprintWeeks_WeekId",
                table: "Tasks");

            migrationBuilder.AlterColumn<long>(
                name: "WeekId",
                table: "Tasks",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_SprintWeeks_WeekId",
                table: "Tasks",
                column: "WeekId",
                principalTable: "SprintWeeks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_SprintWeeks_WeekId",
                table: "Tasks");

            migrationBuilder.AlterColumn<long>(
                name: "WeekId",
                table: "Tasks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_SprintWeeks_WeekId",
                table: "Tasks",
                column: "WeekId",
                principalTable: "SprintWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
