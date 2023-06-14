using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class KycInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KycInfoExisting",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExistingApplicantId = table.Column<string>(nullable: true),
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
                    request = table.Column<string>(nullable: true),
                    ApplicantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KycInfoExisting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KycInfoExisting_ExistingApplicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalSchema: "loanflow",
                        principalTable: "ExistingApplicant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KycInfoExisting_ApplicantId",
                schema: "loanflow",
                table: "KycInfoExisting",
                column: "ApplicantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KycInfoExisting",
                schema: "loanflow");
        }
    }
}
