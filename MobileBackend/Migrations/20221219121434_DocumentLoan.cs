using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class DocumentLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentLoan",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountNo = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    ApplicationDetailId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentLoan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentLoan_Applications_ApplicationDetailId",
                        column: x => x.ApplicationDetailId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentLoan_ApplicationDetailId",
                schema: "loanflow",
                table: "DocumentLoan",
                column: "ApplicationDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentLoan",
                schema: "loanflow");
        }
    }
}
