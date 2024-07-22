using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digital.Backend.Migrations
{
    /// <inheritdoc />
    public partial class NewAtributeLastQuestionUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Questions_LastQuestionId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastQuestionId",
                table: "AspNetUsers",
                newName: "LastQuestionActiveId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_LastQuestionId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_LastQuestionActiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Questions_LastQuestionActiveId",
                table: "AspNetUsers",
                column: "LastQuestionActiveId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Questions_LastQuestionActiveId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LastQuestionActiveId",
                table: "AspNetUsers",
                newName: "LastQuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_LastQuestionActiveId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_LastQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Questions_LastQuestionId",
                table: "AspNetUsers",
                column: "LastQuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
