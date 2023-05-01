using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlackOverload.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "QuestionComment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AnswerComment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionComment_ApplicationUserId",
                table: "QuestionComment",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerComment_ApplicationUserId",
                table: "AnswerComment",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerComment_AspNetUsers_ApplicationUserId",
                table: "AnswerComment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionComment_AspNetUsers_ApplicationUserId",
                table: "QuestionComment",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerComment_AspNetUsers_ApplicationUserId",
                table: "AnswerComment");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionComment_AspNetUsers_ApplicationUserId",
                table: "QuestionComment");

            migrationBuilder.DropIndex(
                name: "IX_QuestionComment_ApplicationUserId",
                table: "QuestionComment");

            migrationBuilder.DropIndex(
                name: "IX_AnswerComment_ApplicationUserId",
                table: "AnswerComment");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "QuestionComment");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AnswerComment");
        }
    }
}
