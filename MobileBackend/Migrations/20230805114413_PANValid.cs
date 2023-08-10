using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class PANValid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadComments_CommonLeads_GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments");

            migrationBuilder.DropIndex(
                name: "IX_LeadComments_GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments");

            migrationBuilder.DropColumn(
                name: "GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments");

            migrationBuilder.CreateTable(
                name: "PANValidation",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: true),
                    PANNo = table.Column<string>(nullable: true),
                    response1 = table.Column<string>(nullable: true),
                    response2 = table.Column<string>(nullable: true),
                    req_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PANValidation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PANValidation",
                schema: "loanflow");

            migrationBuilder.AddColumn<int>(
                name: "GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadComments_GeneralLeadsId",
                schema: "loanflow",
                table: "LeadComments",
                column: "GeneralLeadsId");

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
    }
}
