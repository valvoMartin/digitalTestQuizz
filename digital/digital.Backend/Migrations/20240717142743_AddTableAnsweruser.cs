using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digital.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddTableAnsweruser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_Text_Id",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Cuit_Name",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name_Id",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Answers_Text_Id",
                table: "Answers");

            migrationBuilder.CreateTable(
                name: "AnswersUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateFinished = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersUsers", x => new { x.UserId, x.QuestionId, x.AnswerId });
                    table.ForeignKey(
                        name: "FK_AnswersUsers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswersUsers_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswersUsers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Text",
                table: "Questions",
                column: "Text",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Cuit",
                table: "Companies",
                column: "Cuit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Text",
                table: "Answers",
                column: "Text",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnswersUsers_AnswerId",
                table: "AnswersUsers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswersUsers_QuestionId",
                table: "AnswersUsers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswersUsers_UserId1",
                table: "AnswersUsers",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswersUsers");

            migrationBuilder.DropIndex(
                name: "IX_Questions_Text",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Cuit",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Answers_Text",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Text_Id",
                table: "Questions",
                columns: new[] { "Text", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Cuit_Name",
                table: "Companies",
                columns: new[] { "Cuit", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name_Id",
                table: "Categories",
                columns: new[] { "Name", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Text_Id",
                table: "Answers",
                columns: new[] { "Text", "Id" },
                unique: true);
        }
    }
}
