using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class BCDetails1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherInfo",
                schema: "loanflow",
                table: "KCCLandDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RajaswaNyayalay",
                schema: "loanflow",
                table: "KCCLandDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InspectionAllotedTo",
                schema: "loanflow",
                table: "Applications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InspectionAllotmentDate",
                schema: "loanflow",
                table: "Applications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InspectionDone",
                schema: "loanflow",
                table: "Applications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherInfo",
                schema: "loanflow",
                table: "KCCLandDetails");

            migrationBuilder.DropColumn(
                name: "RajaswaNyayalay",
                schema: "loanflow",
                table: "KCCLandDetails");

            migrationBuilder.DropColumn(
                name: "InspectionAllotedTo",
                schema: "loanflow",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "InspectionAllotmentDate",
                schema: "loanflow",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "InspectionDone",
                schema: "loanflow",
                table: "Applications");
        }
    }
}
