using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileBackend.Migrations
{
    public partial class Leads5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadRoadMap_Leads_LeadDetailsId",
                schema: "loanflow",
                table: "LeadRoadMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadRoadMap",
                schema: "loanflow",
                table: "LeadRoadMap");

            migrationBuilder.RenameTable(
                name: "LeadRoadMap",
                schema: "loanflow",
                newName: "LeadComments",
                newSchema: "loanflow");

            migrationBuilder.RenameIndex(
                name: "IX_LeadRoadMap_LeadDetailsId",
                schema: "loanflow",
                table: "LeadComments",
                newName: "IX_LeadComments_LeadDetailsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadComments",
                schema: "loanflow",
                table: "LeadComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadComments_Leads_LeadDetailsId",
                schema: "loanflow",
                table: "LeadComments",
                column: "LeadDetailsId",
                principalSchema: "loanflow",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadComments_Leads_LeadDetailsId",
                schema: "loanflow",
                table: "LeadComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadComments",
                schema: "loanflow",
                table: "LeadComments");

            migrationBuilder.RenameTable(
                name: "LeadComments",
                schema: "loanflow",
                newName: "LeadRoadMap",
                newSchema: "loanflow");

            migrationBuilder.RenameIndex(
                name: "IX_LeadComments_LeadDetailsId",
                schema: "loanflow",
                table: "LeadRoadMap",
                newName: "IX_LeadRoadMap_LeadDetailsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadRoadMap",
                schema: "loanflow",
                table: "LeadRoadMap",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadRoadMap_Leads_LeadDetailsId",
                schema: "loanflow",
                table: "LeadRoadMap",
                column: "LeadDetailsId",
                principalSchema: "loanflow",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
