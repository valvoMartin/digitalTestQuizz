using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digital.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddAreaAndSubArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubAreaId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubArea_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SubAreaId",
                table: "Questions",
                column: "SubAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubArea_AreaId",
                table: "SubArea",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_SubArea_SubAreaId",
                table: "Questions",
                column: "SubAreaId",
                principalTable: "SubArea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_SubArea_SubAreaId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "SubArea");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SubAreaId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SubAreaId",
                table: "Questions");
        }
    }
}
