using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class Leads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoldLeads",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicantName = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
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
                    AppliedAmt = table.Column<int>(nullable: false),
                    AnyOtherLoan = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicantName = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
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
                    AppliedAmt = table.Column<int>(nullable: false),
                    District = table.Column<string>(nullable: true),
                    Tehsil = table.Column<string>(nullable: true),
                    RI = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    KhasraNo = table.Column<string>(nullable: true),
                    IrrigationSource = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KCCLeads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TPLLeads",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicantName = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
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
                    AppliedAmt = table.Column<int>(nullable: false),
                    TypeOfVehichle = table.Column<string>(nullable: true),
                    Make = table.Column<string>(nullable: true),
                    PastExperience = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPLLeads", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoldLeads",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "KCCLeads",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "TPLLeads",
                schema: "loanflow");
        }
    }
}
