using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class SanctionLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SanctioningLevel",
                schema: "loanflow",
                table: "Applications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SanctioningUserId",
                schema: "loanflow",
                table: "Applications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SanctioningLevel",
                schema: "loanflow",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "SanctioningUserId",
                schema: "loanflow",
                table: "Applications");
        }
    }
}
