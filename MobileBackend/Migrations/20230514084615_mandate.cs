using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class mandate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_KCCRenewal_ApplicationId",
                schema: "loanflow",
                table: "KCCRenewal");

            migrationBuilder.CreateTable(
                name: "Mandates",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    ApplicantId = table.Column<int>(nullable: false),
                    reference_id = table.Column<string>(nullable: true),
                    debtor_account_type = table.Column<string>(nullable: true),
                    debtor_account_id = table.Column<string>(nullable: true),
                    occurance_sequence_type = table.Column<string>(nullable: true),
                    occurance_frequency_type = table.Column<string>(nullable: true),
                    scheme_reference_number = table.Column<string>(nullable: true),
                    consumer_reference_number = table.Column<string>(nullable: true),
                    debtor_name = table.Column<string>(nullable: true),
                    email_address = table.Column<string>(nullable: true),
                    first_collection_date = table.Column<string>(nullable: true),
                    mobile_number = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    collection_amount_type = table.Column<string>(nullable: true),
                    amount = table.Column<int>(nullable: false),
                    mandate_type_category_code = table.Column<string>(nullable: true),
                    is_until_cancel = table.Column<bool>(nullable: false),
                    quick_invite = table.Column<bool>(nullable: false),
                    authentication_mode = table.Column<string>(nullable: true),
                    instructed_agent_code = table.Column<string>(nullable: true),
                    createDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mandates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mandates_Applications_ApplicationId",
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
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_ApplicationId",
                schema: "loanflow",
                table: "Mandates",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mandates",
                schema: "loanflow");

            migrationBuilder.DropIndex(
                name: "IX_KCCRenewal_ApplicationId",
                schema: "loanflow",
                table: "KCCRenewal");

            migrationBuilder.CreateIndex(
                name: "IX_KCCRenewal_ApplicationId",
                schema: "loanflow",
                table: "KCCRenewal",
                column: "ApplicationId");
        }
    }
}
