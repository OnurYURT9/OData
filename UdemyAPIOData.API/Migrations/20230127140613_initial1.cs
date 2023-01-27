using Microsoft.EntityFrameworkCore.Migrations;

namespace UdemyAPIOData.API.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categor_Id",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Categor_Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
