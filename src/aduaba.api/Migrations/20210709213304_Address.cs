using Microsoft.EntityFrameworkCore.Migrations;

namespace aduaba.api.Migrations
{
    public partial class Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    addressId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserNameId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addresses", x => x.addressId);
                    table.ForeignKey(
                        name: "FK_addresses_AspNetUsers_UserNameId",
                        column: x => x.UserNameId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addresses_UserNameId",
                table: "addresses",
                column: "UserNameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses");
        }
    }
}
