using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManager.Migrations
{
    /// <inheritdoc />
    public partial class startupDirectory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StartupPrograms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProgramType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartupPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Startups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdReferente = table.Column<long>(type: "bigint", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Technology = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Funding = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EvolutionState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FundStage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LegalNature = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PitchDeck = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startups", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StartupPrograms");

            migrationBuilder.DropTable(
                name: "Startups");
        }
    }
}
