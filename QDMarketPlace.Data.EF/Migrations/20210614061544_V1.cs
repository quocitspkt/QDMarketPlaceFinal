using Microsoft.EntityFrameworkCore.Migrations;

namespace QDMarketPlace.Data.EF.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Colors");

            migrationBuilder.AddColumn<string>(
                name: "NameColors",
                table: "Colors",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameColors",
                table: "Colors");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Colors",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
