using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class umrn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "umrn_mandate",
                schema: "loanflow",
                table: "Mandates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "umrn_mandate",
                schema: "loanflow",
                table: "ExistingAcMandates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "umrn_mandate",
                schema: "loanflow",
                table: "Mandates");

            migrationBuilder.DropColumn(
                name: "umrn_mandate",
                schema: "loanflow",
                table: "ExistingAcMandates");
        }
    }
}
