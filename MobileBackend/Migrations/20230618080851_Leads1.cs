using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class Leads1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OtherInfo1",
                schema: "loanflow",
                table: "CommonLeads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OtherInfo2",
                schema: "loanflow",
                table: "CommonLeads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RefNoCRGB",
                schema: "loanflow",
                table: "CommonLeads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherInfo1",
                schema: "loanflow",
                table: "CommonLeads");

            migrationBuilder.DropColumn(
                name: "OtherInfo2",
                schema: "loanflow",
                table: "CommonLeads");

            migrationBuilder.DropColumn(
                name: "RefNoCRGB",
                schema: "loanflow",
                table: "CommonLeads");
        }
    }
}
