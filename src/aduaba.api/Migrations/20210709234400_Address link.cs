using Microsoft.EntityFrameworkCore.Migrations;

namespace aduaba.api.Migrations
{
    public partial class Addresslink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_AspNetUsers_UserNameId",
                table: "addresses");

            migrationBuilder.DropIndex(
                name: "IX_addresses_UserNameId",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "UserNameId",
                table: "addresses");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "addresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "addresses");

            migrationBuilder.AddColumn<string>(
                name: "UserNameId",
                table: "addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_addresses_UserNameId",
                table: "addresses",
                column: "UserNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_AspNetUsers_UserNameId",
                table: "addresses",
                column: "UserNameId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
