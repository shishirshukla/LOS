using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobileBackend.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "loanflow");

            migrationBuilder.CreateTable(
                name: "AccountInfo",
                schema: "loanflow",
                columns: table => new
                {
                    AccountNo = table.Column<string>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    CIFNo = table.Column<string>(nullable: true),
                    MappedApplicationId = table.Column<int>(nullable: false),
                    AccountType = table.Column<int>(nullable: false),
                    IntCat = table.Column<int>(nullable: false),
                    ProductDesc = table.Column<string>(nullable: true),
                    AccountFacility = table.Column<string>(nullable: true),
                    IracStatus = table.Column<string>(nullable: true),
                    OdLimit = table.Column<decimal>(nullable: false),
                    CurrentOs = table.Column<decimal>(nullable: false),
                    Arrears = table.Column<decimal>(nullable: false),
                    UnitLocation = table.Column<string>(nullable: true),
                    BorrowersLocation = table.Column<string>(nullable: true),
                    AccountStatus = table.Column<string>(nullable: true),
                    AcOpenDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfo", x => x.AccountNo);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BranchName = table.Column<string>(nullable: true),
                    RegionalOffice = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    BrType = table.Column<string>(nullable: true),
                    AMHCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchPerformances",
                schema: "loanflow",
                columns: table => new
                {
                    BranchCode = table.Column<string>(nullable: false),
                    DepositPrevFy = table.Column<float>(nullable: false),
                    DepositLastFy = table.Column<float>(nullable: false),
                    DepositLastQuarter = table.Column<float>(nullable: false),
                    DepositLastMonth = table.Column<float>(nullable: false),
                    DepositLastDay = table.Column<float>(nullable: false),
                    CASAPrevFy = table.Column<float>(nullable: false),
                    CASALastFy = table.Column<float>(nullable: false),
                    CASALastQuarter = table.Column<float>(nullable: false),
                    CASALastMonth = table.Column<float>(nullable: false),
                    CASALastDay = table.Column<float>(nullable: false),
                    NPAPrevFy = table.Column<float>(nullable: false),
                    NPALastFy = table.Column<float>(nullable: false),
                    NPALastQuarter = table.Column<float>(nullable: false),
                    NPALastMonth = table.Column<float>(nullable: false),
                    NPALastDay = table.Column<float>(nullable: false),
                    AdvancePrevFy = table.Column<float>(nullable: false),
                    AdvanceLastFy = table.Column<float>(nullable: false),
                    AdvanceLastQuarter = table.Column<float>(nullable: false),
                    AdvanceLastMonth = table.Column<float>(nullable: false),
                    AdvanceLastDay = table.Column<float>(nullable: false),
                    PerPrevFy = table.Column<float>(nullable: false),
                    PerLastFy = table.Column<float>(nullable: false),
                    PerLastQuarter = table.Column<float>(nullable: false),
                    PerLastMonth = table.Column<float>(nullable: false),
                    PerLastDay = table.Column<float>(nullable: false),
                    AgriPrevFy = table.Column<float>(nullable: false),
                    AgriLastFy = table.Column<float>(nullable: false),
                    AgriLastQuarter = table.Column<float>(nullable: false),
                    AgriLastMonth = table.Column<float>(nullable: false),
                    AgriLastDay = table.Column<float>(nullable: false),
                    SMEPrevFy = table.Column<float>(nullable: false),
                    SMELastFy = table.Column<float>(nullable: false),
                    SMELastQuarter = table.Column<float>(nullable: false),
                    SMELastMonth = table.Column<float>(nullable: false),
                    SMELastDay = table.Column<float>(nullable: false),
                    HLPrevFy = table.Column<float>(nullable: false),
                    HLLastFy = table.Column<float>(nullable: false),
                    HLLastQuarter = table.Column<float>(nullable: false),
                    HLLastMonth = table.Column<float>(nullable: false),
                    HLLastDay = table.Column<float>(nullable: false),
                    CarPrevFy = table.Column<float>(nullable: false),
                    CarLastFy = table.Column<float>(nullable: false),
                    CarLastQuarter = table.Column<float>(nullable: false),
                    CarLastMonth = table.Column<float>(nullable: false),
                    CarLastDay = table.Column<float>(nullable: false),
                    PLPrevFy = table.Column<float>(nullable: false),
                    PLLastFy = table.Column<float>(nullable: false),
                    PLLastQuarter = table.Column<float>(nullable: false),
                    PLLastMonth = table.Column<float>(nullable: false),
                    PLLastDay = table.Column<float>(nullable: false),
                    GoldPrevFy = table.Column<float>(nullable: false),
                    GoldLastFy = table.Column<float>(nullable: false),
                    GoldLastQuarter = table.Column<float>(nullable: false),
                    GoldLastMonth = table.Column<float>(nullable: false),
                    GoldLastDay = table.Column<float>(nullable: false),
                    KCCPrevFy = table.Column<float>(nullable: false),
                    KCCLastFy = table.Column<float>(nullable: false),
                    KCCLastQuarter = table.Column<float>(nullable: false),
                    KCCLastMonth = table.Column<float>(nullable: false),
                    KCCLastDay = table.Column<float>(nullable: false),
                    NRLMPrevFy = table.Column<float>(nullable: false),
                    NRLMLastFy = table.Column<float>(nullable: false),
                    NRLMLastQuarter = table.Column<float>(nullable: false),
                    NRLMLastMonth = table.Column<float>(nullable: false),
                    NRLMLastDay = table.Column<float>(nullable: false),
                    SMELPrevFy = table.Column<float>(nullable: false),
                    SMELLastFy = table.Column<float>(nullable: false),
                    SMELLastQuarter = table.Column<float>(nullable: false),
                    SMELLastMonth = table.Column<float>(nullable: false),
                    SMELLastDay = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchPerformances", x => x.BranchCode);
                });

            migrationBuilder.CreateTable(
                name: "CSPValueStatements",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Statement = table.Column<string>(nullable: false),
                    InputType = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSPValueStatements", x => x.Id);
                });

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
                    branch = table.Column<string>(nullable: true),
                    umrn_mandate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExistingAcMandates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KCCExistingLand",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountNo = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Tehsil = table.Column<string>(nullable: true),
                    RI = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    DharanAdhikar = table.Column<string>(nullable: true),
                    RajaswaNyayalay = table.Column<string>(nullable: true),
                    OtherInfo = table.Column<string>(nullable: true),
                    SourceofIrrigation = table.Column<string>(nullable: true),
                    KhasraNo = table.Column<string>(nullable: true),
                    LandKhata = table.Column<string>(nullable: true),
                    Charge = table.Column<string>(nullable: true),
                    TotalArea = table.Column<decimal>(nullable: false),
                    IrrigatedArea = table.Column<decimal>(nullable: false),
                    UnIrrigatedArea = table.Column<decimal>(nullable: false),
                    NonAgriArea = table.Column<decimal>(nullable: false),
                    DistrictCode = table.Column<string>(nullable: true),
                    TehsilCode = table.Column<string>(nullable: true),
                    VillageCode = table.Column<string>(nullable: true),
                    fbDistrictCode = table.Column<string>(nullable: true),
                    fbTehsilCode = table.Column<string>(nullable: true),
                    fbVillageCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KCCExistingLand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostInspectionVisitRemark",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VisitId = table.Column<int>(nullable: false),
                    ValueStatementId = table.Column<int>(nullable: false),
                    RemarksVisitingOfficial = table.Column<string>(nullable: false),
                    RemarksVerifyingOfficial = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostInspectionVisitRemark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreInspectionValueStatements",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Statement = table.Column<string>(nullable: false),
                    InputType = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreInspectionValueStatements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreIsnpectionRemarks",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VisitId = table.Column<int>(nullable: false),
                    ValueStatementId = table.Column<int>(nullable: false),
                    RemarksVisitingOfficial = table.Column<string>(nullable: false),
                    RemarksVerifyingOfficial = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreIsnpectionRemarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitRemarks",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VisitId = table.Column<int>(nullable: false),
                    ValueStatementId = table.Column<int>(nullable: false),
                    RemarksVisitingOfficial = table.Column<string>(nullable: false),
                    RemarksVerifyingOfficial = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitRemarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExistingApplicant",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountInfoId = table.Column<string>(nullable: true),
                    ExistingCIF = table.Column<string>(nullable: true),
                    Salutation = table.Column<string>(nullable: true),
                    MaritialStatus = table.Column<string>(nullable: true),
                    Applicant_First_Name = table.Column<string>(nullable: true),
                    Applicant_Middle_Name = table.Column<string>(nullable: true),
                    Applicant_Last_Name = table.Column<string>(nullable: true),
                    Relationship = table.Column<string>(nullable: true),
                    Guardian_Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    PAN_No = table.Column<string>(nullable: true),
                    Adhaar_No = table.Column<string>(nullable: true),
                    Voter_Id = table.Column<string>(nullable: true),
                    ElectionId = table.Column<string>(nullable: true),
                    Driving_Lic = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    Date_Of_Birth = table.Column<string>(nullable: true),
                    Address_Line1 = table.Column<string>(nullable: true),
                    Address_Line2 = table.Column<string>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: true),
                    WorkPlaceAddress = table.Column<string>(nullable: true),
                    City_Village = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PINCode = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    WorkPlace = table.Column<string>(nullable: true),
                    Dependent = table.Column<string>(nullable: true),
                    WorkingSince = table.Column<string>(nullable: true),
                    Education = table.Column<string>(nullable: true),
                    LatestIncome = table.Column<string>(nullable: true),
                    RelationWithAppl = table.Column<string>(nullable: true),
                    PrimaryBankingRelation = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    NetWorth = table.Column<string>(nullable: true),
                    NMI = table.Column<int>(nullable: false),
                    GMI = table.Column<int>(nullable: false),
                    AddlIncome = table.Column<int>(nullable: false),
                    RemarkOnNMI = table.Column<string>(nullable: true),
                    RemarkOnAddlIncome = table.Column<string>(nullable: true),
                    RemarkOther = table.Column<string>(nullable: true),
                    Caste = table.Column<string>(nullable: true),
                    Religion = table.Column<string>(nullable: true),
                    TypeofApplicant = table.Column<string>(nullable: true),
                    DocumentExecution = table.Column<string>(nullable: true),
                    CreditScore = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExistingApplicant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExistingApplicant_AccountInfo_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalSchema: "loanflow",
                        principalTable: "AccountInfo",
                        principalColumn: "AccountNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "loanflow",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchId = table.Column<string>(nullable: true),
                    TypeOfApplicant = table.Column<string>(nullable: true),
                    ApplicantShortName = table.Column<string>(nullable: true),
                    LoanScheme = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    SubsidyAmt = table.Column<decimal>(nullable: false),
                    AppliedAmt = table.Column<decimal>(nullable: false),
                    Eligibility = table.Column<decimal>(nullable: false),
                    SponsoringAgency = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    OwnerUser = table.Column<string>(nullable: true),
                    SanctionedLevel = table.Column<string>(nullable: true),
                    ControlStatus = table.Column<string>(nullable: true),
                    ROIType = table.Column<string>(nullable: true),
                    IsLead = table.Column<bool>(nullable: false),
                    LeadStage = table.Column<string>(nullable: true),
                    LeadSourcedBy = table.Column<string>(nullable: true),
                    LeadSourceAgency = table.Column<string>(nullable: true),
                    MappedTLAccount = table.Column<string>(nullable: true),
                    MappedCCAccount = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    ControlRemark = table.Column<string>(nullable: true),
                    InspectionAllotedTo = table.Column<string>(nullable: true),
                    InspectionAllotmentDate = table.Column<DateTime>(nullable: true),
                    InspectionDone = table.Column<string>(nullable: true),
                    PendingAction = table.Column<string>(nullable: true),
                    ApplicationDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    ControlledDate = table.Column<DateTime>(nullable: true),
                    SanctionedDate = table.Column<DateTime>(nullable: true),
                    ForwardedDate = table.Column<DateTime>(nullable: true),
                    SendToControlDate = table.Column<DateTime>(nullable: true),
                    ReviewDate = table.Column<DateTime>(nullable: true),
                    SanctioningUserId = table.Column<string>(nullable: true),
                    SanctioningLevel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "loanflow",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Scale = table.Column<string>(nullable: true),
                    OtherRole = table.Column<string>(nullable: true),
                    ProfileImage = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    FCMId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "loanflow",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Csps",
                schema: "loanflow",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    CSPName = table.Column<string>(nullable: false),
                    CorporateAgency = table.Column<string>(nullable: false),
                    MobileNumber = table.Column<string>(nullable: false),
                    BrCode = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Photo = table.Column<string>(nullable: false),
                    BranchMasterId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Csps", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Csps_Branches_BranchMasterId",
                        column: x => x.BranchMasterId,
                        principalSchema: "loanflow",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicantName = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    LeadDate = table.Column<DateTime>(nullable: false),
                    SourcedBy = table.Column<string>(nullable: true),
                    SourcingAgency = table.Column<string>(nullable: true),
                    ExistingCustomer = table.Column<bool>(nullable: false),
                    PrimaryBank = table.Column<string>(nullable: true),
                    LeadStatus = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    PinCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PANNo = table.Column<string>(nullable: true),
                    AadhaarNo = table.Column<string>(nullable: true),
                    ElectionId = table.Column<string>(nullable: true),
                    OtherIncomeSource = table.Column<string>(nullable: true),
                    OtherEarningMember = table.Column<string>(nullable: true),
                    DetailsofEarningMember = table.Column<string>(nullable: true),
                    UploadPAN = table.Column<string>(nullable: true),
                    UploadAadhaar = table.Column<string>(nullable: true),
                    UploadElectionId = table.Column<string>(nullable: true),
                    UploadPhoto = table.Column<string>(nullable: true),
                    UploadDocument1 = table.Column<string>(nullable: true),
                    UploadDocument2 = table.Column<string>(nullable: true),
                    BranchId = table.Column<string>(nullable: true),
                    AppliedAmt = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    AnyOtherLoan = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Tehsil = table.Column<string>(nullable: true),
                    RI = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    KhasraNo = table.Column<string>(nullable: true),
                    IrrigationSource = table.Column<string>(nullable: true),
                    TypeOfVehichle = table.Column<string>(nullable: true),
                    Make = table.Column<string>(nullable: true),
                    PastExperience = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leads_Branches_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "loanflow",
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KCCCropDetailExisting",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KCCExistingLandId = table.Column<int>(nullable: false),
                    AccountNo = table.Column<string>(nullable: true),
                    Season = table.Column<string>(nullable: true),
                    Crop = table.Column<string>(nullable: true),
                    IrrigatedArea = table.Column<decimal>(nullable: false),
                    UnIrrigatedArea = table.Column<decimal>(nullable: false),
                    SOFIrrigatedArea = table.Column<decimal>(nullable: false),
                    SOFUnIrrigatedArea = table.Column<decimal>(nullable: false),
                    CropInsurance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KCCCropDetailExisting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KCCCropDetailExisting_KCCExistingLand_KCCExistingLandId",
                        column: x => x.KCCExistingLandId,
                        principalSchema: "loanflow",
                        principalTable: "KCCExistingLand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Applicants",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    ExistingCIF = table.Column<string>(nullable: true),
                    Salutation = table.Column<string>(nullable: true),
                    MaritialStatus = table.Column<string>(nullable: true),
                    Applicant_First_Name = table.Column<string>(nullable: true),
                    Applicant_Middle_Name = table.Column<string>(nullable: true),
                    Applicant_Last_Name = table.Column<string>(nullable: true),
                    Relationship = table.Column<string>(nullable: true),
                    Guardian_Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    PAN_No = table.Column<string>(nullable: true),
                    Adhaar_No = table.Column<string>(nullable: true),
                    Voter_Id = table.Column<string>(nullable: true),
                    ElectionId = table.Column<string>(nullable: true),
                    Driving_Lic = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    Date_Of_Birth = table.Column<string>(nullable: true),
                    Address_Line1 = table.Column<string>(nullable: true),
                    Address_Line2 = table.Column<string>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: true),
                    WorkPlaceAddress = table.Column<string>(nullable: true),
                    City_Village = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PINCode = table.Column<string>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    WorkPlace = table.Column<string>(nullable: true),
                    Dependent = table.Column<string>(nullable: true),
                    WorkingSince = table.Column<string>(nullable: true),
                    Education = table.Column<string>(nullable: true),
                    LatestIncome = table.Column<string>(nullable: true),
                    RelationWithAppl = table.Column<string>(nullable: true),
                    PrimaryBankingRelation = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(nullable: true),
                    NetWorth = table.Column<string>(nullable: true),
                    NMI = table.Column<int>(nullable: false),
                    GMI = table.Column<int>(nullable: false),
                    AddlIncome = table.Column<int>(nullable: false),
                    RemarkOnNMI = table.Column<string>(nullable: true),
                    RemarkOnAddlIncome = table.Column<string>(nullable: true),
                    RemarkOther = table.Column<string>(nullable: true),
                    Caste = table.Column<string>(nullable: true),
                    Religion = table.Column<string>(nullable: true),
                    TypeofApplicant = table.Column<string>(nullable: true),
                    DocumentExecution = table.Column<string>(nullable: true),
                    KYC_Upload = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applicants_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Charges",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    Frequency = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    Percentage = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charges_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControlInformation",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    ReceivedDocuments = table.Column<string>(nullable: true),
                    EligibleAmt = table.Column<int>(nullable: false),
                    Statement1 = table.Column<string>(nullable: true),
                    Statement2 = table.Column<string>(nullable: true),
                    Statement3 = table.Column<string>(nullable: true),
                    Statement4 = table.Column<string>(nullable: true),
                    Statement5 = table.Column<string>(nullable: true),
                    Statement6 = table.Column<string>(nullable: true),
                    Statement7 = table.Column<string>(nullable: true),
                    Statement8 = table.Column<string>(nullable: true),
                    Statement9 = table.Column<string>(nullable: true),
                    Statement10 = table.Column<string>(nullable: true),
                    Statement11 = table.Column<string>(nullable: true),
                    Statement12 = table.Column<string>(nullable: true),
                    Statement13 = table.Column<string>(nullable: true),
                    Statement14 = table.Column<string>(nullable: true),
                    Statement15 = table.Column<string>(nullable: true),
                    Statement16 = table.Column<string>(nullable: true),
                    Statement17 = table.Column<string>(nullable: true),
                    AppliedAmt = table.Column<decimal>(nullable: false),
                    SanctionedTL = table.Column<decimal>(nullable: false),
                    SanctionedCC = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControlInformation_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disbursements",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    BankPart = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disbursements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disbursements_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Documents",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KCCLandDetails",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    District = table.Column<string>(nullable: true),
                    Tehsil = table.Column<string>(nullable: true),
                    RI = table.Column<string>(nullable: true),
                    Village = table.Column<string>(nullable: true),
                    OwnerName = table.Column<string>(nullable: true),
                    DharanAdhikar = table.Column<string>(nullable: true),
                    RajaswaNyayalay = table.Column<string>(nullable: true),
                    OtherInfo = table.Column<string>(nullable: true),
                    SourceofIrrigation = table.Column<string>(nullable: true),
                    KhasraNo = table.Column<string>(nullable: true),
                    Charge = table.Column<string>(nullable: true),
                    TotalArea = table.Column<decimal>(nullable: false),
                    IrrigatedArea = table.Column<decimal>(nullable: false),
                    UnIrrigatedArea = table.Column<decimal>(nullable: false),
                    NonAgriArea = table.Column<decimal>(nullable: false),
                    DistrictCode = table.Column<string>(nullable: true),
                    TehsilCode = table.Column<string>(nullable: true),
                    VillageCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KCCLandDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KCCLandDetails_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "LoanApplications",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TypeOfFacility = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    AppliedAmount = table.Column<decimal>(nullable: false),
                    SanctionedAmount = table.Column<decimal>(nullable: false),
                    RateOfInterest = table.Column<decimal>(nullable: false),
                    Spread = table.Column<decimal>(nullable: false),
                    PLR = table.Column<decimal>(nullable: false),
                    Repayment = table.Column<decimal>(nullable: false),
                    RepayTerm = table.Column<decimal>(nullable: false),
                    Moratorium = table.Column<string>(nullable: true),
                    RepayStartDate = table.Column<string>(nullable: true),
                    RenewalDate = table.Column<string>(nullable: true),
                    UtilizationPeriod = table.Column<string>(nullable: true),
                    LoanAccountNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanApplications_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    IpAddress = table.Column<string>(nullable: true),
                    Mode = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    createDate = table.Column<DateTime>(nullable: false),
                    api_response_id = table.Column<string>(nullable: true),
                    emandate_id = table.Column<string>(nullable: true),
                    last_run_date = table.Column<DateTime>(nullable: true),
                    last_run_status = table.Column<string>(nullable: true),
                    is_cancelled = table.Column<string>(nullable: true),
                    mandate_status = table.Column<string>(nullable: true),
                    umrn_mandate = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ProjectCosts",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EligibleForLoan = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    AppliedAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCosts_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Remarks",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    TypeOfRemark = table.Column<string>(nullable: true),
                    TypeOfRecomendation = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    EmpName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    SenderUserId = table.Column<string>(nullable: true),
                    Statment = table.Column<string>(nullable: true),
                    SpecialRemark = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remarks_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Securities",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationId = table.Column<int>(nullable: false),
                    TypeOfSecurity = table.Column<string>(nullable: true),
                    TypeOfCharge = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Boundary = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    PlotNo = table.Column<string>(nullable: true),
                    Advocate1 = table.Column<string>(nullable: true),
                    Advocate2 = table.Column<string>(nullable: true),
                    Valuer1 = table.Column<string>(nullable: true),
                    Valuer2 = table.Column<string>(nullable: true),
                    DateTSR1 = table.Column<string>(nullable: true),
                    DateTSR2 = table.Column<string>(nullable: true),
                    DetailsofTSR1 = table.Column<string>(nullable: true),
                    DetailsofTSR2 = table.Column<string>(nullable: true),
                    DateVal1 = table.Column<string>(nullable: true),
                    DateVal2 = table.Column<string>(nullable: true),
                    GovtVal1 = table.Column<decimal>(nullable: false),
                    MarketVal1 = table.Column<decimal>(nullable: false),
                    DistressVal1 = table.Column<decimal>(nullable: false),
                    GovtVal2 = table.Column<decimal>(nullable: false),
                    MarketVal2 = table.Column<decimal>(nullable: false),
                    DistressVal2 = table.Column<decimal>(nullable: false),
                    Valuation = table.Column<decimal>(nullable: false),
                    RateOfInterest = table.Column<decimal>(nullable: false),
                    DocumentDetails = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Securities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Securities_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    FuelCost = table.Column<int>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "loanflow",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "loanflow",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "loanflow",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "loanflow",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "loanflow",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "loanflow",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "loanflow",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "loanflow",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostInspection",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    AccountNo = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    SystemLogDate = table.Column<DateTime>(nullable: false),
                    ClosureDate = table.Column<DateTime>(nullable: true),
                    ClosureOfficialId = table.Column<string>(nullable: true),
                    VisitRemarks = table.Column<string>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    EmployeeMasterId = table.Column<string>(nullable: true),
                    AccountDataAccountNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostInspection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostInspection_AccountInfo_AccountDataAccountNo",
                        column: x => x.AccountDataAccountNo,
                        principalSchema: "loanflow",
                        principalTable: "AccountInfo",
                        principalColumn: "AccountNo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostInspection_AspNetUsers_EmployeeMasterId",
                        column: x => x.EmployeeMasterId,
                        principalSchema: "loanflow",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreInspection",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    SystemLogDate = table.Column<DateTime>(nullable: false),
                    ClosureDate = table.Column<DateTime>(nullable: true),
                    ClosureOfficialId = table.Column<string>(nullable: true),
                    VisitRemarks = table.Column<string>(nullable: true),
                    EmployeeMasterId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreInspection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreInspection_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreInspection_AspNetUsers_EmployeeMasterId",
                        column: x => x.EmployeeMasterId,
                        principalSchema: "loanflow",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CSPVisits",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    CSPId = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    VisitDate = table.Column<DateTime>(nullable: false),
                    SystemLogDate = table.Column<DateTime>(nullable: false),
                    ClosureDate = table.Column<DateTime>(nullable: true),
                    ClosureOfficialId = table.Column<string>(nullable: true),
                    VisitRemarks = table.Column<string>(nullable: true),
                    Location_lat = table.Column<string>(nullable: true),
                    Location_long = table.Column<string>(nullable: true),
                    EmployeeMasterId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSPVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CSPVisits_Csps_CSPId",
                        column: x => x.CSPId,
                        principalSchema: "loanflow",
                        principalTable: "Csps",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CSPVisits_AspNetUsers_EmployeeMasterId",
                        column: x => x.EmployeeMasterId,
                        principalSchema: "loanflow",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadComments",
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
                    table.PrimaryKey("PK_LeadComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadComments_Leads_LeadDetailsId",
                        column: x => x.LeadDetailsId,
                        principalSchema: "loanflow",
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CIBILRequests",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicantId = table.Column<int>(nullable: false),
                    RequestDate = table.Column<string>(nullable: true),
                    CiibilControlNumber = table.Column<string>(nullable: true),
                    ScoreName1 = table.Column<string>(nullable: true),
                    ScoreName2 = table.Column<string>(nullable: true),
                    ScoreName3 = table.Column<string>(nullable: true),
                    Score1 = table.Column<string>(nullable: true),
                    Score2 = table.Column<string>(nullable: true),
                    Score3 = table.Column<string>(nullable: true),
                    ResponseFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIBILRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CIBILRequests_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalSchema: "loanflow",
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KCCCropDetails",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KCCLandDetailId = table.Column<int>(nullable: false),
                    ApplicationId = table.Column<int>(nullable: false),
                    Season = table.Column<string>(nullable: true),
                    Crop = table.Column<string>(nullable: true),
                    IrrigatedArea = table.Column<decimal>(nullable: false),
                    UnIrrigatedArea = table.Column<decimal>(nullable: false),
                    SOFIrrigatedArea = table.Column<decimal>(nullable: false),
                    SOFUnIrrigatedArea = table.Column<decimal>(nullable: false),
                    CropInsurance = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KCCCropDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KCCCropDetails_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "loanflow",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KCCCropDetails_KCCLandDetails_KCCLandDetailId",
                        column: x => x.KCCLandDetailId,
                        principalSchema: "loanflow",
                        principalTable: "KCCLandDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CibilEnquiries",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CIBILRequestId = table.Column<int>(nullable: false),
                    index = table.Column<string>(nullable: true),
                    enquiryDate = table.Column<string>(nullable: true),
                    memberShortName = table.Column<string>(nullable: true),
                    enquiryPurpose = table.Column<string>(nullable: true),
                    enquiryAmount = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CibilEnquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CibilEnquiries_CIBILRequests_CIBILRequestId",
                        column: x => x.CIBILRequestId,
                        principalSchema: "loanflow",
                        principalTable: "CIBILRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CIBILLoanInfo",
                schema: "loanflow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CIBILRequestId = table.Column<int>(nullable: false),
                    index = table.Column<string>(nullable: true),
                    memberShortName = table.Column<string>(nullable: true),
                    accountType = table.Column<string>(nullable: true),
                    ownershipIndicator = table.Column<string>(nullable: true),
                    dateOpened = table.Column<string>(nullable: true),
                    dateClosed = table.Column<string>(nullable: true),
                    dateReported = table.Column<string>(nullable: true),
                    highCreditAmount = table.Column<string>(nullable: true),
                    currentBalance = table.Column<string>(nullable: true),
                    amountOverdue = table.Column<string>(nullable: true),
                    paymentHistory = table.Column<string>(nullable: true),
                    paymentStartDate = table.Column<string>(nullable: true),
                    paymentEndDate = table.Column<string>(nullable: true),
                    collateralType = table.Column<string>(nullable: true),
                    errorCode = table.Column<string>(nullable: true),
                    cibilRemarksDate = table.Column<string>(nullable: true),
                    cibilRemarksCode = table.Column<string>(nullable: true),
                    errorRemarksCode1 = table.Column<string>(nullable: true),
                    errorRemarksCode2 = table.Column<string>(nullable: true),
                    lastPaymentDate = table.Column<string>(nullable: true),
                    creditLimit = table.Column<string>(nullable: true),
                    EmployeeResponse = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    EMIConsidered = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CIBILLoanInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CIBILLoanInfo_CIBILRequests_CIBILRequestId",
                        column: x => x.CIBILRequestId,
                        principalSchema: "loanflow",
                        principalTable: "CIBILRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountStatments_ApplicationId",
                schema: "loanflow",
                table: "AccountStatments",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_ApplicationId",
                schema: "loanflow",
                table: "Applicants",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_BranchId",
                schema: "loanflow",
                table: "Applications",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "loanflow",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "loanflow",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "loanflow",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "loanflow",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "loanflow",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BranchId",
                schema: "loanflow",
                table: "AspNetUsers",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "loanflow",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "loanflow",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charges_ApplicationId",
                schema: "loanflow",
                table: "Charges",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_CibilEnquiries_CIBILRequestId",
                schema: "loanflow",
                table: "CibilEnquiries",
                column: "CIBILRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CIBILLoanInfo_CIBILRequestId",
                schema: "loanflow",
                table: "CIBILLoanInfo",
                column: "CIBILRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CIBILRequests_ApplicantId",
                schema: "loanflow",
                table: "CIBILRequests",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlInformation_ApplicationId",
                schema: "loanflow",
                table: "ControlInformation",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Csps_BranchMasterId",
                schema: "loanflow",
                table: "Csps",
                column: "BranchMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_CSPVisits_CSPId",
                schema: "loanflow",
                table: "CSPVisits",
                column: "CSPId");

            migrationBuilder.CreateIndex(
                name: "IX_CSPVisits_EmployeeMasterId",
                schema: "loanflow",
                table: "CSPVisits",
                column: "EmployeeMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Disbursements_ApplicationId",
                schema: "loanflow",
                table: "Disbursements",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentLoan_ApplicationDetailId",
                schema: "loanflow",
                table: "DocumentLoan",
                column: "ApplicationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ApplicationId",
                schema: "loanflow",
                table: "Documents",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExistingApplicant_AccountInfoId",
                schema: "loanflow",
                table: "ExistingApplicant",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_KCCCropDetailExisting_KCCExistingLandId",
                schema: "loanflow",
                table: "KCCCropDetailExisting",
                column: "KCCExistingLandId");

            migrationBuilder.CreateIndex(
                name: "IX_KCCCropDetails_ApplicationId",
                schema: "loanflow",
                table: "KCCCropDetails",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_KCCCropDetails_KCCLandDetailId",
                schema: "loanflow",
                table: "KCCCropDetails",
                column: "KCCLandDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_KCCLandDetails_ApplicationId",
                schema: "loanflow",
                table: "KCCLandDetails",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_KCCRenewal_ApplicationId",
                schema: "loanflow",
                table: "KCCRenewal",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadComments_LeadDetailsId",
                schema: "loanflow",
                table: "LeadComments",
                column: "LeadDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_BranchId",
                schema: "loanflow",
                table: "Leads",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_ApplicationId",
                schema: "loanflow",
                table: "LoanApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_ApplicationId",
                schema: "loanflow",
                table: "Logs",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Mandates_ApplicationId",
                schema: "loanflow",
                table: "Mandates",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MudraDetails_ApplicationId",
                schema: "loanflow",
                table: "MudraDetails",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PostInspection_AccountDataAccountNo",
                schema: "loanflow",
                table: "PostInspection",
                column: "AccountDataAccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_PostInspection_EmployeeMasterId",
                schema: "loanflow",
                table: "PostInspection",
                column: "EmployeeMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PreInspection_ApplicationId",
                schema: "loanflow",
                table: "PreInspection",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreInspection_EmployeeMasterId",
                schema: "loanflow",
                table: "PreInspection",
                column: "EmployeeMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCosts_ApplicationId",
                schema: "loanflow",
                table: "ProjectCosts",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_ApplicationId",
                schema: "loanflow",
                table: "Remarks",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Securities_ApplicationId",
                schema: "loanflow",
                table: "Securities",
                column: "ApplicationId");

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
                name: "AccountStatments",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "BranchPerformances",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Charges",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "CibilEnquiries",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "CIBILLoanInfo",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "ControlInformation",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "CSPValueStatements",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "CSPVisits",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Disbursements",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "DocumentLoan",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Documents",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "ExistingAcMandates",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "ExistingApplicant",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "KCCCropDetailExisting",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "KCCCropDetails",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "KCCRenewal",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "LeadComments",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "LoanApplications",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Logs",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Mandates",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "MudraDetails",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "PostInspection",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "PostInspectionVisitRemark",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "PreInspection",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "PreInspectionValueStatements",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "PreIsnpectionRemarks",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "ProjectCosts",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Remarks",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Securities",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "TPLDetails",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "VisitRemarks",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "CIBILRequests",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Csps",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "KCCExistingLand",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "KCCLandDetails",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Leads",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "AccountInfo",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Applicants",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Applications",
                schema: "loanflow");

            migrationBuilder.DropTable(
                name: "Branches",
                schema: "loanflow");
        }
    }
}
