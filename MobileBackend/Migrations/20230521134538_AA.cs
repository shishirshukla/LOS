using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class AA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountStatments",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    ApplicantId = table.Column<int>(nullable: false),
                    MobileNumber = table.Column<string>(nullable: true),
                    status_request = table.Column<string>(nullable: true),
                    Consent_Handle = table.Column<string>(nullable: true),
                    request_status = table.Column<string>(nullable: true),
                    statement = table.Column<string>(nullable: true),
                    requestDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountStatments_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatments_ApplicationId",
                schema: "loanflow",
                table: "AccountStatments",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountStatments",
                schema: "loanflow");
        }
    }
}
