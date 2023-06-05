using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class KCCRenewal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KCCRenewal",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    CreditBalanceDate = table.Column<string>(nullable: true),
                    CBSSancDate = table.Column<string>(nullable: true),
                    SanctionedLimit = table.Column<int>(nullable: false),
                    AppliedAmt = table.Column<int>(nullable: false),
                    AcNumber = table.Column<string>(nullable: true),
                    Product = table.Column<string>(nullable: true),
                    AcOpenDate = table.Column<DateTime>(nullable: false),
                    CBSLimit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KCCRenewal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KCCRenewal_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KCCRenewal_ApplicationId",
                schema: "loanflow",
                table: "KCCRenewal",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KCCRenewal",
                schema: "loanflow");
        }
    }
}
