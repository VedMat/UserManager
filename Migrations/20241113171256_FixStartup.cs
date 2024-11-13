using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManager.Migrations
{
    /// <inheritdoc />
    public partial class FixStartup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Startups_StartupPrograms_StartupProgramsId",
                table: "Startups");

            migrationBuilder.RenameColumn(
                name: "StartupProgramsId",
                table: "Startups",
                newName: "StartupProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Startups_StartupProgramsId",
                table: "Startups",
                newName: "IX_Startups_StartupProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Startups_StartupPrograms_StartupProgramId",
                table: "Startups",
                column: "StartupProgramId",
                principalTable: "StartupPrograms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Startups_StartupPrograms_StartupProgramId",
                table: "Startups");

            migrationBuilder.RenameColumn(
                name: "StartupProgramId",
                table: "Startups",
                newName: "StartupProgramsId");

            migrationBuilder.RenameIndex(
                name: "IX_Startups_StartupProgramId",
                table: "Startups",
                newName: "IX_Startups_StartupProgramsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Startups_StartupPrograms_StartupProgramsId",
                table: "Startups",
                column: "StartupProgramsId",
                principalTable: "StartupPrograms",
                principalColumn: "Id");
        }
    }
}
