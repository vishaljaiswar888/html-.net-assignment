using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizWiz3.Migrations
{
    public partial class secondupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submits_Quizs_QuizId",
                table: "Submits");

            migrationBuilder.DropIndex(
                name: "IX_Submits_QuizId",
                table: "Submits");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Submits");

            migrationBuilder.DropColumn(
                name: "correct_Answer",
                table: "Submits");

            migrationBuilder.CreateIndex(
                name: "IX_Submits_quiz_Id",
                table: "Submits",
                column: "quiz_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submits_Quizs_quiz_Id",
                table: "Submits",
                column: "quiz_Id",
                principalTable: "Quizs",
                principalColumn: "quiz_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submits_Quizs_quiz_Id",
                table: "Submits");

            migrationBuilder.DropIndex(
                name: "IX_Submits_quiz_Id",
                table: "Submits");

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Submits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "correct_Answer",
                table: "Submits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Submits_QuizId",
                table: "Submits",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Submits_Quizs_QuizId",
                table: "Submits",
                column: "QuizId",
                principalTable: "Quizs",
                principalColumn: "quiz_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
