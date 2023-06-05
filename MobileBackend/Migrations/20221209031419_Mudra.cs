using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class Mudra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MudraDetails",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    Parameter = table.Column<string>(nullable: true),
                    PrevYear = table.Column<decimal>(nullable: false),
                    CurrentYear = table.Column<decimal>(nullable: false),
                    Estimate1 = table.Column<decimal>(nullable: false),
                    Estimate2 = table.Column<decimal>(nullable: false),
                    ParameterType = table.Column<string>(nullable: true),
                    ParameterClass = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MudraDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MudraDetails_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MudraDetails_ApplicationId",
                schema: "loanflow",
                table: "MudraDetails",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MudraDetails",
                schema: "loanflow");
        }
    }
}
