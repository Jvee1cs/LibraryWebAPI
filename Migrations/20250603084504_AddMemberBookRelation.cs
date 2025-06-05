using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librarykuno.Migrations
{
    /// <inheritdoc />
    public partial class AddMemberBookRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_BorrowerId",
                table: "Books");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_BorrowerId",
                table: "Books",
                column: "BorrowerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Members_BorrowerId",
                table: "Books");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Members_BorrowerId",
                table: "Books",
                column: "BorrowerId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
