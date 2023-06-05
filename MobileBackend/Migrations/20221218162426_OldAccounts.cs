using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class OldAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountInfo",
                schema: "loanflow",
                columns: table => new
                {
                    AccountNo = table.Column<string>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    CIFNo = table.Column<string>(nullable: true),
                    ApplicationId = table.Column<int>(nullable: false),
                    AccountType = table.Column<int>(nullable: false),
                    IntCat = table.Column<int>(nullable: false),
                    ProductDesc = table.Column<string>(nullable: true),
                    AccountFacility = table.Column<string>(nullable: true),
                    IracStatus = table.Column<string>(nullable: true),
                    OdLimit = table.Column<decimal>(nullable: false),
                    CurrentOs = table.Column<decimal>(nullable: false),
                    Arrears = table.Column<decimal>(nullable: false),
                    UnitLocation = table.Column<string>(nullable: true),
                    BorrowersLocation = table.Column<string>(nullable: true),
                    AccountStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfo", x => x.AccountNo);
                });

            migrationBuilder.CreateTable(
                name: "KCCExistingLand",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountNo = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Tehsil = table.Column<string>(nullable: true),
                    RI = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    DharanAdhikar = table.Column<string>(nullable: true),
                    RajaswaNyayalay = table.Column<string>(nullable: true),
                    OtherInfo = table.Column<string>(nullable: true),
                    SourceofIrrigation = table.Column<string>(nullable: true),
                    KhasraNo = table.Column<string>(nullable: true),
                    Charge = table.Column<string>(nullable: true),
                    TotalArea = table.Column<decimal>(nullable: false),
                    IrrigatedArea = table.Column<decimal>(nullable: false),
                    UnIrrigatedArea = table.Column<decimal>(nullable: false),
                    NonAgriArea = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KCCExistingLand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostInspectionVisitRemark",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VisitId = table.Column<int>(nullable: false),
                    ValueStatementId = table.Column<int>(nullable: false),
                    RemarksVisitingOfficial = table.Column<string>(nullable: false),
                    RemarksVerifyingOfficial = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostInspectionVisitRemark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostInspection",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    AccountNo = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    SystemLogDate = table.Column<DateTime>(nullable: false),
                    ClosureDate = table.Column<DateTime>(nullable: true),
                    ClosureOfficialId = table.Column<string>(nullable: true),
                    VisitRemarks = table.Column<string>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    EmployeeMasterId = table.Column<string>(nullable: true),
                    AccountDataAccountNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostInspection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostInspection_AccountInfo_AccountDataAccountNo",
                        column: x => x.AccountDataAccountNo,
                        principalSchema: "loanflow",
                        principalTable: "AccountInfo",
                        principalColumn: "AccountNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostInspection_AspNetUsers_EmployeeMasterId",
                        column: x => x.EmployeeMasterId,
                        principalSchema: "loanflow",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KCCCropDetailExisting",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KCCExistingLandId = table.Column<int>(nullable: false),
                    AccountNo = table.Column<string>(nullable: true),
                    Season = table.Column<string>(nullable: true),
                    Crop = table.Column<string>(nullable: true),
                    IrrigatedArea = table.Column<decimal>(nullable: false),
                    UnIrrigatedArea = table.Column<decimal>(nullable: false),
                    SOFIrrigatedArea = table.Column<decimal>(nullable: false),
                    SOFUnIrrigatedArea = table.Column<decimal>(nullable: false),
                    CropInsurance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KCCCropDetailExisting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KCCCropDetailExisting_KCCExistingLand_KCCExistingLandId",
                        column: x => x.KCCExistingLandId,
                        principalSchema: "loanflow",
                        principalTable: "KCCExistingLand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KCCCropDetailExisting_KCCExistingLandId",
                schema: "loanflow",
                table: "KCCCropDetailExisting",
                column: "KCCExistingLandId");

            migrationBuilder.CreateIndex(
                name: "IX_PostInspection_AccountDataAccountNo",
                schema: "loanflow",
                table: "PostInspection",
                column: "AccountDataAccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_PostInspection_EmployeeMasterId",
                schema: "loanflow",
                table: "PostInspection",
                column: "EmployeeMasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KCCCropDetailExisting",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "PostInspection",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "PostInspectionVisitRemark",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "KCCExistingLand",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "AccountInfo",
                schema: "loanflow");
        }
    }
}
