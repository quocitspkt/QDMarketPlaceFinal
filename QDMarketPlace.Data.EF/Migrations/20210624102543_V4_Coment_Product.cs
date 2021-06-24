using Microsoft.EntityFrameworkCore.Migrations;

namespace QDMarketPlace.Data.EF.Migrations
{
    public partial class V4_Coment_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CommentId",
                table: "Products",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Comments_CommentId",
                table: "Products",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Comments_CommentId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CommentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
