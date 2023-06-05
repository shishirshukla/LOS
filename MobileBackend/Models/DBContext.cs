using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MobileBackend.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Branch> Branches { get; set; }
        public DbSet<AccountStatments> AccountStatments { get; set; }
        public DbSet<ExistingAcMandate> ExistingAcMandates { get; set; }
        public DbSet<Mandate> Mandates { get; set; }
        public DbSet<VillageMaster> VillageMaster { get; set; }
        public DbSet<KCCInfo> KCCInfo { get; set; }
        public DbSet<BranchPerformance> BranchPerformances { get; set; }
        public DbSet<CSPVisit> CSPVisits { get; set; }
        public DbSet<ValueStatement> CSPValueStatements { get; set; }
        public DbSet<PreInspection> PreInspection { get; set; }
        public DbSet<Csp> Csps { get; set; }
        public DbSet<AppReport> Report { get; set; }
        public DbSet<SchemeReport> SchemeReport { get; set; }
        public DbSet<DocumentLoan> DocumentLoan { get; set; }
        public DbSet<Cust360> Cust360s { get; set; }
        public DbSet<AccountData> AccountData { get; set; }
        public DbSet<ChartReport> ChartReports { get; set; }
        public DbSet<KeyValue> KeyValues { get; set; }

        public DbSet<PreVisitRemark> PreIsnpectionRemarks { get; set; }
        public DbSet<PreValueStatement> PreInspectionValueStatements { get; set; }
        public DbSet<VisitRemark> VisitRemarks { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<Disbursement> Disbursements { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<CIBILRequest> CIBILRequests { get; set; }
        public DbSet<CIBILLoanInfo> CIBILLoanInfo { get; set; }
        public DbSet<CibilEnquiry> CibilEnquiries { get; set; }
        public DbSet<ProjectCost> ProjectCosts { get; set; }
        public DbSet<KCCLandDetail> KCCLandDetails { get; set; }
        public DbSet<PostInspection> PostInspection { get; set; }
        public DbSet<PostInspectionVisitRemark> PostInspectionVisitRemark { get; set; }
        public DbSet<AccountInfo> AccountInfo { get; set; }
        public DbSet<ExistingApplicant> ExistingApplicant { get; set; }

        public DbSet<KCCExistingLand> KCCExistingLand { get; set; }
        public DbSet<KCCCropDetailExisting> KCCCropDetailExisting { get; set; }
        public DbSet<KCCCropDetail> KCCCropDetails { get; set; }

        public DbSet<KCCRenewal> KCCRenewal { get; set; }

        public DbSet<KCCElig> KCCEligillity { get; set; }
        public DbSet<CropDetail> CropDetails { get; set; }

        public DbSet<CIFData> CIFDatas { get; set; }
        public DbSet<ControlInformation> ControlInformation { get; set; }
        public DbSet<KCCLeads> KCCLeads { get; set; }
        public DbSet<TPLLeads> TPLLeads { get; set; }
        public DbSet<GoldLoanLeads> GoldLeads { get; set; }
        public DbSet<LeadRoadMap> LeadComments { get; set; }
        public DbSet<TPLDetail> TPLDetails { get; set; }
        public DbSet<MudraDetail> MudraDetails { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           builder.HasDefaultSchema("loanflow");
            builder.Entity<AppReport>().HasNoKey().ToView("Null");
            builder.Entity<CIFData>().HasNoKey().ToView("Null1");
            builder.Entity<KeyValue>().HasNoKey().ToView("Null2");
            builder.Entity<KCCElig>().HasNoKey().ToView("Null11");
            builder.Entity<CropDetail>().HasNoKey().ToView("Null12");
            builder.Entity<Cust360>().HasNoKey().ToView("Cust360");
            builder.Entity<SchemeReport>().HasNoKey().ToView("Scheme");
            builder.Entity<ChartReport>().HasNoKey().ToView("ChartReps");
            builder.Entity<AccountData>().HasNoKey().ToView("AccntDt");
            builder.Entity<VillageMaster>().HasNoKey().ToView("VillMaster");
            builder.Entity<KCCInfo>().HasNoKey().ToView("KccInf");
        }
    }
}
