using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class Leads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommonLeads",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicantName = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    LeadDate = table.Column<DateTime>(nullable: false),
                    SourcedBy = table.Column<string>(nullable: true),
                    SourcingAgency = table.Column<string>(nullable: true),
                    ExistingCustomer = table.Column<bool>(nullable: false),
                    PrimaryBank = table.Column<string>(nullable: true),
                    LeadStatus = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    PinCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PANNo = table.Column<string>(nullable: true),
                    AadhaarNo = table.Column<string>(nullable: true),
                    ElectionId = table.Column<string>(nullable: true),
                    OtherIncomeSource = table.Column<string>(nullable: true),
                    OtherEarningMember = table.Column<string>(nullable: true),
                    DetailsofEarningMember = table.Column<string>(nullable: true),
                    UploadPAN = table.Column<string>(nullable: true),
                    UploadAadhaar = table.Column<string>(nullable: true),
                    UploadElectionId = table.Column<string>(nullable: true),
                    UploadPhoto = table.Column<string>(nullable: true),
                    UploadDocument1 = table.Column<string>(nullable: true),
                    UploadDocument2 = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    LoanScheme = table.Column<string>(nullable: true),
                    LoanPurpose = table.Column<string>(nullable: true),
                    AppliedAmt = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonLeads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonLeads_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "loanflow",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadComments_GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments",
                column: "GeneralLeadsId");

            migrationBuilder.CreateIndex(
                name: "IX_CommonLeads_BranchId",
                schema: "loanflow",
                table: "CommonLeads",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadComments_CommonLeads_GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments",
                column: "GeneralLeadsId",
                principalSchema: "loanflow",
                principalTable: "CommonLeads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadComments_CommonLeads_GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments");

            migrationBuilder.DropTable(
                name: "CommonLeads",
                schema: "loanflow");

            migrationBuilder.DropIndex(
                name: "IX_LeadComments_GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments");

            migrationBuilder.DropColumn(
                name: "GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments");
        }
    }
}
