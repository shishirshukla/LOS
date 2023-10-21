using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class Psv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostSanctionVisits",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    AccountNo = table.Column<string>(nullable: true),
                    latlong = table.Column<string>(nullable: true),
                    image1 = table.Column<string>(nullable: true),
                    image2 = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    VisitDate = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSanctionVisits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostSanctionVisits",
                schema: "loanflow");
        }
    }
}
