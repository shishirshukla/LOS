using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class Leads4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MappedBranch",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leads_BranchId",
                schema: "loanflow",
                table: "Leads",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Branches_BranchId",
                schema: "loanflow",
                table: "Leads",
                column: "BranchId",
                principalSchema: "loanflow",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Branches_BranchId",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropIndex(
                name: "IX_Leads_BranchId",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.AddColumn<string>(
                name: "MappedBranch",
                schema: "loanflow",
                table: "Leads",
                type: "text",
                nullable: true);
        }
    }
}
