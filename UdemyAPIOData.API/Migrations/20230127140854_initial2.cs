using Microsoft.EntityFrameworkCore.Migrations;

namespace UdemyAPIOData.API.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Products");

          

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
