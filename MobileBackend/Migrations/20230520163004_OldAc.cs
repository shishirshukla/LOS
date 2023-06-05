using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class OldAc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExistingApplicant",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<string>(nullable: true),
                    ExistingCIF = table.Column<string>(nullable: true),
                    Salutation = table.Column<string>(nullable: true),
                    MaritialStatus = table.Column<string>(nullable: true),
                    Applicant_First_Name = table.Column<string>(nullable: true),
                    Applicant_Middle_Name = table.Column<string>(nullable: true),
                    Applicant_Last_Name = table.Column<string>(nullable: true),
                    Relationship = table.Column<string>(nullable: true),
                    Guardian_Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    PAN_No = table.Column<string>(nullable: true),
                    Adhaar_No = table.Column<string>(nullable: true),
                    Voter_Id = table.Column<string>(nullable: true),
                    ElectionId = table.Column<string>(nullable: true),
                    Driving_Lic = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    Date_Of_Birth = table.Column<string>(nullable: true),
                    Address_Line1 = table.Column<string>(nullable: true),
                    Address_Line2 = table.Column<string>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: true),
                    WorkPlaceAddress = table.Column<string>(nullable: true),
                    City_Village = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PINCode = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    WorkPlace = table.Column<string>(nullable: true),
                    Dependent = table.Column<string>(nullable: true),
                    WorkingSince = table.Column<string>(nullable: true),
                    Education = table.Column<string>(nullable: true),
                    LatestIncome = table.Column<string>(nullable: true),
                    RelationWithAppl = table.Column<string>(nullable: true),
                    PrimaryBankingRelation = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    NetWorth = table.Column<string>(nullable: true),
                    NMI = table.Column<int>(nullable: false),
                    GMI = table.Column<int>(nullable: false),
                    AddlIncome = table.Column<int>(nullable: false),
                    RemarkOnNMI = table.Column<string>(nullable: true),
                    RemarkOnAddlIncome = table.Column<string>(nullable: true),
                    RemarkOther = table.Column<string>(nullable: true),
                    Caste = table.Column<string>(nullable: true),
                    Religion = table.Column<string>(nullable: true),
                    TypeofApplicant = table.Column<string>(nullable: true),
                    DocumentExecution = table.Column<string>(nullable: true),
                    CreditScore = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExistingApplicant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExistingApplicant_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalSchema: "loanflow",
                        principalTable: "AccountInfo",
                        principalColumn: "AccountNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExistingApplicant_AccountInfoId",
                schema: "loanflow",
                table: "ExistingApplicant",
                column: "AccountInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExistingApplicant",
                schema: "loanflow");
        }
    }
}
