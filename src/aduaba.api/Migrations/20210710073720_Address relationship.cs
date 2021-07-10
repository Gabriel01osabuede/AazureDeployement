using Microsoft.EntityFrameworkCore.Migrations;

namespace aduaba.api.Migrations
{
    public partial class Addressrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "addresses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "addresses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_addresses_UserId",
                table: "addresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_AspNetUsers_UserId",
                table: "addresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_AspNetUsers_UserId",
                table: "addresses");

            migrationBuilder.DropIndex(
                name: "IX_addresses_UserId",
                table: "addresses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "addresses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
