using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digital.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesRubroAndSector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rubro",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "EmployesLimit",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "Companies",
                newName: "SectorId");

            migrationBuilder.AddColumn<int>(
                name: "SectorId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rubros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: false),
                    RubroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sectors_Rubros_RubroId",
                        column: x => x.RubroId,
                        principalTable: "Rubros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SectorId",
                table: "Companies",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SectorId",
                table: "Categories",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_RubroId",
                table: "Sectors",
                column: "RubroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Sectors_SectorId",
                table: "Categories",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Sectors_SectorId",
                table: "Companies",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Sectors_SectorId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Sectors_SectorId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Rubros");

            migrationBuilder.DropIndex(
                name: "IX_Companies_SectorId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SectorId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SectorId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "SectorId",
                table: "Companies",
                newName: "Sector");

            migrationBuilder.AddColumn<int>(
                name: "Rubro",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployesLimit",
                table: "Categories",
                type: "int",
                maxLength: 5,
                nullable: false,
                defaultValue: 0);
        }
    }
}
