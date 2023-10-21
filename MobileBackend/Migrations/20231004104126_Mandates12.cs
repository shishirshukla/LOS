using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class Mandates12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_until_cancel",
                schema: "loanflow",
                table: "ExistingAcMandates");

            migrationBuilder.AddColumn<bool>(
                name: "untill_30_years",
                schema: "loanflow",
                table: "ExistingAcMandates",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "untill_30_years",
                schema: "loanflow",
                table: "ExistingAcMandates");

            migrationBuilder.AddColumn<bool>(
                name: "is_until_cancel",
                schema: "loanflow",
                table: "ExistingAcMandates",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
