using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class Leads1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LeadDate",
                schema: "loanflow",
                table: "TPLLeads",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SourcedBy",
                schema: "loanflow",
                table: "TPLLeads",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LeadDate",
                schema: "loanflow",
                table: "KCCLeads",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SourcedBy",
                schema: "loanflow",
                table: "KCCLeads",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LeadDate",
                schema: "loanflow",
                table: "GoldLeads",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SourcedBy",
                schema: "loanflow",
                table: "GoldLeads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeadDate",
                schema: "loanflow",
                table: "TPLLeads");

            migrationBuilder.DropColumn(
                name: "SourcedBy",
                schema: "loanflow",
                table: "TPLLeads");

            migrationBuilder.DropColumn(
                name: "LeadDate",
                schema: "loanflow",
                table: "KCCLeads");

            migrationBuilder.DropColumn(
                name: "SourcedBy",
                schema: "loanflow",
                table: "KCCLeads");

            migrationBuilder.DropColumn(
                name: "LeadDate",
                schema: "loanflow",
                table: "GoldLeads");

            migrationBuilder.DropColumn(
                name: "SourcedBy",
                schema: "loanflow",
                table: "GoldLeads");
        }
    }
}
