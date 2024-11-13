using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManager.Migrations
{
    /// <inheritdoc />
    public partial class AddContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Startups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Startups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNotes",
                table: "Startups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Startups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactRole",
                table: "Startups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastContacted",
                table: "Startups",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Startups");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Startups");

            migrationBuilder.DropColumn(
                name: "ContactNotes",
                table: "Startups");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Startups");

            migrationBuilder.DropColumn(
                name: "ContactRole",
                table: "Startups");

            migrationBuilder.DropColumn(
                name: "LastContacted",
                table: "Startups");
        }
    }
}
