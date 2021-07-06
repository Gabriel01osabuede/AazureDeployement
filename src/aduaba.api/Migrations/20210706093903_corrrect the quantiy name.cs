using Microsoft.EntityFrameworkCore.Migrations;

namespace aduaba.api.Migrations
{
    public partial class corrrectthequantiyname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productQuantiyPurchased",
                table: "cart",
                newName: "productQuantityPurchased");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productQuantityPurchased",
                table: "cart",
                newName: "productQuantiyPurchased");
        }
    }
}
