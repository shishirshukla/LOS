using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class TPL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TPLDetails",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    TypeOfVehichle = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true),
                    ReferenceUnit = table.Column<string>(nullable: true),
                    Mileage = table.Column<decimal>(nullable: false),
                    DepriciationRate = table.Column<int>(nullable: false),
                    TripsPerDay = table.Column<int>(nullable: false),
                    UsePerTrip = table.Column<int>(nullable: false),
                    ReceiptPerTrip = table.Column<int>(nullable: false),
                    WorkingDaysInMonth = table.Column<int>(nullable: false),
                    ServiceCost = table.Column<int>(nullable: false),
                    TireReplacementPeriod = table.Column<int>(nullable: false),
                    TireReplacementCost = table.Column<int>(nullable: false),
                    OtherCost = table.Column<int>(nullable: false),
                    Insurance = table.Column<int>(nullable: false),
                    Taxes = table.Column<int>(nullable: false),
                    DriverCost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPLDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TPLDetails_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TPLDetails_ApplicationId",
                schema: "loanflow",
                table: "TPLDetails",
                column: "ApplicationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TPLDetails",
                schema: "loanflow");
        }
    }
}
