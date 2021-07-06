using Microsoft.EntityFrameworkCore.Migrations;

namespace aduaba.api.Migrations
{
    public partial class AddedCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    cartId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    manufacturerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    productAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    productQuantiyPurchased = table.Column<int>(type: "int", nullable: false),
                    productAvailability = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart", x => x.cartId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");
        }
    }
}
