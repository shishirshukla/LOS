using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class mandate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "api_response_id",
                schema: "loanflow",
                table: "Mandates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "emandate_id",
                schema: "loanflow",
                table: "Mandates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "api_response_id",
                schema: "loanflow",
                table: "Mandates");

            migrationBuilder.DropColumn(
                name: "emandate_id",
                schema: "loanflow",
                table: "Mandates");
        }
    }
}
