using Microsoft.EntityFrameworkCore.Migrations;

namespace aduaba.api.Migrations
{
    public partial class Addresslocalgovernmanet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "localGovernmentArea",
                table: "addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "localGovernmentArea",
                table: "addresses");
        }
    }
}
