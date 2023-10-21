using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class emandate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_until_cancel",
                schema: "loanflow",
                table: "Mandates");

            migrationBuilder.AddColumn<bool>(
                name: "untill_30_years",
                schema: "loanflow",
                table: "Mandates",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "untill_30_years",
                schema: "loanflow",
                table: "Mandates");

            migrationBuilder.AddColumn<bool>(
                name: "is_until_cancel",
                schema: "loanflow",
                table: "Mandates",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
