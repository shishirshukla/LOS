using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class KycInfo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KycInfoExisting_ExistingApplicant_ApplicantId",
                schema: "loanflow",
                table: "KycInfoExisting");

            migrationBuilder.DropIndex(
                name: "IX_KycInfoExisting_ApplicantId",
                schema: "loanflow",
                table: "KycInfoExisting");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                schema: "loanflow",
                table: "KycInfoExisting");

            migrationBuilder.AddColumn<int>(
                name: "ExistingApplicantId1",
                schema: "loanflow",
                table: "KycInfoExisting",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KycInfo",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicantId = table.Column<int>(nullable: false),
                    IdType = table.Column<string>(nullable: true),
                    IdNumber = table.Column<string>(nullable: true),
                    IdSource = table.Column<string>(nullable: true),
                    VerificationStatus = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    response = table.Column<string>(nullable: true),
                    request = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KycInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KycInfo_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalSchema: "loanflow",
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KycInfoExisting_ExistingApplicantId1",
                schema: "loanflow",
                table: "KycInfoExisting",
                column: "ExistingApplicantId1");

            migrationBuilder.CreateIndex(
                name: "IX_KycInfo_ApplicantId",
                schema: "loanflow",
                table: "KycInfo",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_KycInfoExisting_ExistingApplicant_ExistingApplicantId1",
                schema: "loanflow",
                table: "KycInfoExisting",
                column: "ExistingApplicantId1",
                principalSchema: "loanflow",
                principalTable: "ExistingApplicant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KycInfoExisting_ExistingApplicant_ExistingApplicantId1",
                schema: "loanflow",
                table: "KycInfoExisting");

            migrationBuilder.DropTable(
                name: "KycInfo",
                schema: "loanflow");

            migrationBuilder.DropIndex(
                name: "IX_KycInfoExisting_ExistingApplicantId1",
                schema: "loanflow",
                table: "KycInfoExisting");

            migrationBuilder.DropColumn(
                name: "ExistingApplicantId1",
                schema: "loanflow",
                table: "KycInfoExisting");

            migrationBuilder.AddColumn<int>(
                name: "ApplicantId",
                schema: "loanflow",
                table: "KycInfoExisting",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KycInfoExisting_ApplicantId",
                schema: "loanflow",
                table: "KycInfoExisting",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_KycInfoExisting_ExistingApplicant_ApplicantId",
                schema: "loanflow",
                table: "KycInfoExisting",
                column: "ApplicantId",
                principalSchema: "loanflow",
                principalTable: "ExistingApplicant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
