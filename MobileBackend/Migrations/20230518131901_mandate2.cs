using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class mandate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "is_cancelled",
                schema: "loanflow",
                table: "Mandates",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_run_date",
                schema: "loanflow",
                table: "Mandates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_run_status",
                schema: "loanflow",
                table: "Mandates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mandate_status",
                schema: "loanflow",
                table: "Mandates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_cancelled",
                schema: "loanflow",
                table: "Mandates");

            migrationBuilder.DropColumn(
                name: "last_run_date",
                schema: "loanflow",
                table: "Mandates");

            migrationBuilder.DropColumn(
                name: "last_run_status",
                schema: "loanflow",
                table: "Mandates");

            migrationBuilder.DropColumn(
                name: "mandate_status",
                schema: "loanflow",
                table: "Mandates");
        }
    }
}
