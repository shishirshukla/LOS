using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class Leads3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoldLeads",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "KCCLeads",
                schema: "loanflow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TPLLeads",
                schema: "loanflow",
                table: "TPLLeads");

            migrationBuilder.RenameTable(
                name: "TPLLeads",
                schema: "loanflow",
                newName: "Leads",
                newSchema: "loanflow");

            migrationBuilder.AddColumn<string>(
                name: "AnyOtherLoan",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IrrigationSource",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KhasraNo",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RI",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tehsil",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Village",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "loanflow",
                table: "Leads",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ExistingCustomer",
                schema: "loanflow",
                table: "Leads",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LeadStatus",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MappedBranch",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryBank",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourcingAgency",
                schema: "loanflow",
                table: "Leads",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leads",
                schema: "loanflow",
                table: "Leads",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LeadRoadMap",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LeadId = table.Column<int>(nullable: false),
                    EntryType = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Attachement = table.Column<string>(nullable: true),
                    LeadDetailsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadRoadMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadRoadMap_Leads_LeadDetailsId",
                        column: x => x.LeadDetailsId,
                        principalSchema: "loanflow",
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadRoadMap_LeadDetailsId",
                schema: "loanflow",
                table: "LeadRoadMap",
                column: "LeadDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadRoadMap",
                schema: "loanflow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leads",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "AnyOtherLoan",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "District",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "IrrigationSource",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "KhasraNo",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "RI",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Tehsil",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Village",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "ExistingCustomer",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "LeadStatus",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "MappedBranch",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "PrimaryBank",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "SourcingAgency",
                schema: "loanflow",
                table: "Leads");

            migrationBuilder.RenameTable(
                name: "Leads",
                schema: "loanflow",
                newName: "TPLLeads",
                newSchema: "loanflow");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TPLLeads",
                schema: "loanflow",
                table: "TPLLeads",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GoldLeads",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AadhaarNo = table.Column<string>(type: "text", nullable: true),
                    AddressLine1 = table.Column<string>(type: "text", nullable: true),
                    AnyOtherLoan = table.Column<string>(type: "text", nullable: true),
                    ApplicantName = table.Column<string>(type: "text", nullable: true),
                    AppliedAmt = table.Column<int>(type: "integer", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    DOB = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DetailsofEarningMember = table.Column<string>(type: "text", nullable: true),
                    ElectionId = table.Column<string>(type: "text", nullable: true),
                    LeadDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MobileNumber = table.Column<string>(type: "text", nullable: true),
                    OtherEarningMember = table.Column<string>(type: "text", nullable: true),
                    OtherIncomeSource = table.Column<string>(type: "text", nullable: true),
                    PANNo = table.Column<string>(type: "text", nullable: true),
                    PinCode = table.Column<string>(type: "text", nullable: true),
                    SourcedBy = table.Column<string>(type: "text", nullable: true),
                    UploadAadhaar = table.Column<string>(type: "text", nullable: true),
                    UploadDocument1 = table.Column<string>(type: "text", nullable: true),
                    UploadDocument2 = table.Column<string>(type: "text", nullable: true),
                    UploadElectionId = table.Column<string>(type: "text", nullable: true),
                    UploadPAN = table.Column<string>(type: "text", nullable: true),
                    UploadPhoto = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoldLeads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KCCLeads",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AadhaarNo = table.Column<string>(type: "text", nullable: true),
                    AddressLine1 = table.Column<string>(type: "text", nullable: true),
                    ApplicantName = table.Column<string>(type: "text", nullable: true),
                    AppliedAmt = table.Column<int>(type: "integer", nullable: false),
                    City = table.Column<string>(type: "text", nullable: true),
                    DOB = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DetailsofEarningMember = table.Column<string>(type: "text", nullable: true),
                    District = table.Column<string>(type: "text", nullable: true),
                    ElectionId = table.Column<string>(type: "text", nullable: true),
                    IrrigationSource = table.Column<string>(type: "text", nullable: true),
                    KhasraNo = table.Column<string>(type: "text", nullable: true),
                    LeadDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MobileNumber = table.Column<string>(type: "text", nullable: true),
                    OtherEarningMember = table.Column<string>(type: "text", nullable: true),
                    OtherIncomeSource = table.Column<string>(type: "text", nullable: true),
                    PANNo = table.Column<string>(type: "text", nullable: true),
                    PinCode = table.Column<string>(type: "text", nullable: true),
                    RI = table.Column<string>(type: "text", nullable: true),
                    SourcedBy = table.Column<string>(type: "text", nullable: true),
                    Tehsil = table.Column<string>(type: "text", nullable: true),
                    UploadAadhaar = table.Column<string>(type: "text", nullable: true),
                    UploadDocument1 = table.Column<string>(type: "text", nullable: true),
                    UploadDocument2 = table.Column<string>(type: "text", nullable: true),
                    UploadElectionId = table.Column<string>(type: "text", nullable: true),
                    UploadPAN = table.Column<string>(type: "text", nullable: true),
                    UploadPhoto = table.Column<string>(type: "text", nullable: true),
                    Village = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KCCLeads", x => x.Id);
                });
        }
    }
}
