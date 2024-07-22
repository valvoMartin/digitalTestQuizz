using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digital.Backend.Migrations
{
    /// <inheritdoc />
    public partial class editedAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswersUsers_AspNetUsers_UserId1",
                table: "AnswersUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswersUsers",
                table: "AnswersUsers");

            migrationBuilder.DropIndex(
                name: "IX_AnswersUsers_UserId1",
                table: "AnswersUsers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AnswersUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFinished",
                table: "AnswersUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AnswersUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AnswersUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswersUsers",
                table: "AnswersUsers",
                columns: new[] { "Email", "QuestionId", "AnswerId" });

            migrationBuilder.CreateIndex(
                name: "IX_AnswersUsers_UserId",
                table: "AnswersUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswersUsers_AspNetUsers_UserId",
                table: "AnswersUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswersUsers_AspNetUsers_UserId",
                table: "AnswersUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswersUsers",
                table: "AnswersUsers");

            migrationBuilder.DropIndex(
                name: "IX_AnswersUsers_UserId",
                table: "AnswersUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AnswersUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AnswersUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFinished",
                table: "AnswersUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "AnswersUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswersUsers",
                table: "AnswersUsers",
                columns: new[] { "UserId", "QuestionId", "AnswerId" });

            migrationBuilder.CreateIndex(
                name: "IX_AnswersUsers_UserId1",
                table: "AnswersUsers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswersUsers_AspNetUsers_UserId1",
                table: "AnswersUsers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
