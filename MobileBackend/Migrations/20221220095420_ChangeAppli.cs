using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class ChangeAppli : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationId",
                schema: "loanflow",
                table: "AccountInfo");

            migrationBuilder.AddColumn<int>(
                name: "MappedApplicationId",
                schema: "loanflow",
                table: "AccountInfo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MappedApplicationId",
                schema: "loanflow",
                table: "AccountInfo");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                schema: "loanflow",
                table: "AccountInfo",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
