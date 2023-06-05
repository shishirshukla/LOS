using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class Elig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Eligibility",
                schema: "loanflow",
                table: "Applications",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eligibility",
                schema: "loanflow",
                table: "Applications");
        }
    }
}
