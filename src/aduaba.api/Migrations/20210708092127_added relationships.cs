using Microsoft.EntityFrameworkCore.Migrations;

namespace aduaba.api.Migrations
{
    public partial class addedrelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "manufacturerName",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "productAmount",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "productAvailability",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "productImageUrl",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "productName",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "productQuantityPurchased",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "cart");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "cart",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "cart",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "productId",
                table: "cart",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_productId",
                table: "cart",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_cart_UserId",
                table: "cart",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_AspNetUsers_UserId",
                table: "cart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_Product_productId",
                table: "cart",
                column: "productId",
                principalTable: "Product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_AspNetUsers_UserId",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_Product_productId",
                table: "cart");

            migrationBuilder.DropIndex(
                name: "IX_cart_productId",
                table: "cart");

            migrationBuilder.DropIndex(
                name: "IX_cart_UserId",
                table: "cart");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "cart",
                newName: "userId");

            migrationBuilder.AlterColumn<string>(
                name: "productId",
                table: "cart",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "cart",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "manufacturerName",
                table: "cart",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "productAmount",
                table: "cart",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "productAvailability",
                table: "cart",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "productImageUrl",
                table: "cart",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productName",
                table: "cart",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productQuantityPurchased",
                table: "cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "cart",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
