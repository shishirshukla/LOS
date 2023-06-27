using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class LeadCommentGen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadCommentsGen",
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
                    table.PrimaryKey("PK_LeadCommentsGen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadCommentsGen_CommonLeads_LeadDetailsId",
                        column: x => x.LeadDetailsId,
                        principalSchema: "loanflow",
                        principalTable: "CommonLeads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadCommentsGen_LeadDetailsId",
                schema: "loanflow",
                table: "LeadCommentsGen",
                column: "LeadDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadCommentsGen",
                schema: "loanflow");
        }
    }
}
