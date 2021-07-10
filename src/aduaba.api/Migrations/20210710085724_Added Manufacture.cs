using Microsoft.EntityFrameworkCore.Migrations;

namespace aduaba.api.Migrations
{
    public partial class AddedManufacture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "productAmount",
                table: "Product",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "ManufactureName",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManufactureName",
                table: "Product");

            migrationBuilder.AlterColumn<decimal>(
                name: "productAmount",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money");
        }
    }
}
