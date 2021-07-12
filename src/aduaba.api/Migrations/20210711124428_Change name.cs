using Microsoft.EntityFrameworkCore.Migrations;

namespace aduaba.api.Migrations
{
    public partial class Changename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productId",
                table: "Product",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "Category",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Product",
                newName: "productId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "categoryId");
        }
    }
}
