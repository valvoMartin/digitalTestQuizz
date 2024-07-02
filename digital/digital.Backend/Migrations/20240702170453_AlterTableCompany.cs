using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digital.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sectors_Id",
                table: "Sectors");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Cuit",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Cuit",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Cuit_Name",
                table: "Companies",
                columns: new[] { "Cuit", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companies_Cuit_Name",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "Email",
                table: "Companies",
                type: "int",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Cuit",
                table: "Companies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_Id",
                table: "Sectors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Cuit",
                table: "Companies",
                column: "Cuit",
                unique: true);
        }
    }
}
