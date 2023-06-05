using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class Kcciss1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LandKhata",
                schema: "loanflow",
                table: "KCCExistingLand",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fbDistrictCode",
                schema: "loanflow",
                table: "KCCExistingLand",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fbTehsilCode",
                schema: "loanflow",
                table: "KCCExistingLand",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fbVillageCode",
                schema: "loanflow",
                table: "KCCExistingLand",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LandKhata",
                schema: "loanflow",
                table: "KCCExistingLand");

            migrationBuilder.DropColumn(
                name: "fbDistrictCode",
                schema: "loanflow",
                table: "KCCExistingLand");

            migrationBuilder.DropColumn(
                name: "fbTehsilCode",
                schema: "loanflow",
                table: "KCCExistingLand");

            migrationBuilder.DropColumn(
                name: "fbVillageCode",
                schema: "loanflow",
                table: "KCCExistingLand");
        }
    }
}
