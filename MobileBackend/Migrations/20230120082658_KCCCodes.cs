using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class KCCCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                schema: "loanflow",
                table: "KCCLandDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TehsilCode",
                schema: "loanflow",
                table: "KCCLandDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VillageCode",
                schema: "loanflow",
                table: "KCCLandDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictCode",
                schema: "loanflow",
                table: "KCCExistingLand",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TehsilCode",
                schema: "loanflow",
                table: "KCCExistingLand",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VillageCode",
                schema: "loanflow",
                table: "KCCExistingLand",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictCode",
                schema: "loanflow",
                table: "KCCLandDetails");

            migrationBuilder.DropColumn(
                name: "TehsilCode",
                schema: "loanflow",
                table: "KCCLandDetails");

            migrationBuilder.DropColumn(
                name: "VillageCode",
                schema: "loanflow",
                table: "KCCLandDetails");

            migrationBuilder.DropColumn(
                name: "DistrictCode",
                schema: "loanflow",
                table: "KCCExistingLand");

            migrationBuilder.DropColumn(
                name: "TehsilCode",
                schema: "loanflow",
                table: "KCCExistingLand");

            migrationBuilder.DropColumn(
                name: "VillageCode",
                schema: "loanflow",
                table: "KCCExistingLand");
        }
    }
}
