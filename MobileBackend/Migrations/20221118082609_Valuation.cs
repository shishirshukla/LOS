using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class Valuation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Advocate1",
                schema: "loanflow",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Advocate2",
                schema: "loanflow",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTSR1",
                schema: "loanflow",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTSR2",
                schema: "loanflow",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateVal1",
                schema: "loanflow",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateVal2",
                schema: "loanflow",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailsofTSR1",
                schema: "loanflow",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailsofTSR2",
                schema: "loanflow",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DistressVal1",
                schema: "loanflow",
                table: "Securities",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DistressVal2",
                schema: "loanflow",
                table: "Securities",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GovtVal1",
                schema: "loanflow",
                table: "Securities",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GovtVal2",
                schema: "loanflow",
                table: "Securities",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MarketVal1",
                schema: "loanflow",
                table: "Securities",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MarketVal2",
                schema: "loanflow",
                table: "Securities",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Valuer1",
                schema: "loanflow",
                table: "Securities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Valuer2",
                schema: "loanflow",
                table: "Securities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Advocate1",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "Advocate2",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "DateTSR1",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "DateTSR2",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "DateVal1",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "DateVal2",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "DetailsofTSR1",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "DetailsofTSR2",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "DistressVal1",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "DistressVal2",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "GovtVal1",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "GovtVal2",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "MarketVal1",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "MarketVal2",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "Valuer1",
                schema: "loanflow",
                table: "Securities");

            migrationBuilder.DropColumn(
                name: "Valuer2",
                schema: "loanflow",
                table: "Securities");
        }
    }
}
