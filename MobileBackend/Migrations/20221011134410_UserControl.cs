using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class UserControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Designation",
                schema: "loanflow",
                table: "Remarks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpName",
                schema: "loanflow",
                table: "Remarks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfRecomendation",
                schema: "loanflow",
                table: "Remarks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerUser",
                schema: "loanflow",
                table: "Applications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation",
                schema: "loanflow",
                table: "Remarks");

            migrationBuilder.DropColumn(
                name: "EmpName",
                schema: "loanflow",
                table: "Remarks");

            migrationBuilder.DropColumn(
                name: "TypeOfRecomendation",
                schema: "loanflow",
                table: "Remarks");

            migrationBuilder.DropColumn(
                name: "OwnerUser",
                schema: "loanflow",
                table: "Applications");
        }
    }
}
