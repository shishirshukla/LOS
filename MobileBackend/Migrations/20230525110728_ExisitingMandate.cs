using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class ExisitingMandate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExistingAcMandates",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LoanAc = table.Column<string>(nullable: true),
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
                    createDate = table.Column<DateTime>(nullable: false),
                    api_response_id = table.Column<string>(nullable: true),
                    emandate_id = table.Column<string>(nullable: true),
                    last_run_date = table.Column<DateTime>(nullable: true),
                    last_run_status = table.Column<string>(nullable: true),
                    is_cancelled = table.Column<string>(nullable: true),
                    mandate_status = table.Column<string>(nullable: true),
                    created_by = table.Column<string>(nullable: true),
                    branch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExistingAcMandates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExistingAcMandates",
                schema: "loanflow");
        }
    }
}
