using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileBackend.Models
{
    public class PostInspection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [Required]
        [ForeignKey("AccountInfo")]
        public string AccountNo { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime VisitDate { get; set; }
        [Required]
        public DateTime SystemLogDate { get; set; }

        public DateTime? ClosureDate { get; set; }

        public string ClosureOfficialId { get; set; }

        public string VisitRemarks { get; set; }
        public string VisitType { get; set; }
        public virtual ApplicationUser EmployeeMaster { get; set; }
        public virtual AccountInfo AccountData { get; set; }


    }
    public class PostInspectionVisitRemark
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VisitId { get; set; }
        [Required]
        public int ValueStatementId { get; set; }
        [Required]
        public string RemarksVisitingOfficial { get; set; }
        [Required]
        public string RemarksVerifyingOfficial { get; set; }
        [Required]
        public string Status { get; set; }


    }

    public class VillageMasterBhuiyan
    {
        [Key]
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string Village { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilCode { get; set; }
        public string VillageCode { get; set; }
        public string DistrictFB { get; set; }
        public string TehsilFB { get; set; }
        public string VillageFB { get; set; }
        public string DistrictCodeFB { get; set; }
        public string TehsilCodeFB { get; set; }
        public string VillageCodeFB { get; set; }

    }


    public class VillageMaster
    {
        [Key]
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string Village { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilCode { get; set; }
        public string VillageCode { get; set; }

    }

    public class KCCInfo
    {
        [Key]
        public string beneficiaryName { get; set; }
        public string aadhaarNumber { get; set; }
        public string beneficiaryPassbookName { get; set; }
        public string mobile { get; set; }
        public string dob { get; set; }
        public int farmerCategory { get; set; }
        public int farmerType { get; set; }
        public int socialCategory { get; set; }
        public string residential_vill { get; set; }

    }

    public class KYCInfoExisting
    {
        [Key]
        public int Id { get; set; }
        public string ExistingApplicantId { get; set; }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public string IdSource { get; set; }
        public string VerificationStatus { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string response { get; set; }
        public string request { get; set; }
        public ExistingApplicant ExistingApplicant { get; set; }

    }
    public class KYCInfo
    {
        [Key]
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public string IdSource { get; set; }
        public string VerificationStatus { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string response { get; set; }
        public string request { get; set; }
        public Applicant Applicant { get; set; }

    }


    public class AccountInfo
    {
        [Key]
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string BranchId { get; set; }
        public string CIFNo { get; set; }
        public int  MappedApplicationId { get; set; }
        public int AccountType { get; set; }
        public int IntCat { get; set; }
        public string ProductDesc { get; set; }
        public string AccountFacility { get; set; }
        public string IracStatus { get; set; }
        public decimal OdLimit { get; set; }
        public decimal CurrentOs { get; set; }
        public decimal Arrears { get; set; }
        public string UnitLocation { get; set; }
        public string BorrowersLocation { get; set; }
        public string AccountStatus { get; set; }
        public DateTime AcOpenDate { get; set; }
        public List<ExistingApplicant> Applicants { get; set; }

    }
    public class KCCExistingLand
    {

        [Key]
        public int Id { get; set; }
       
        public string AccountNo { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string RI { get; set; }
        public string Village { get; set; }
        public string OwnerName { get; set; }
        public string DharanAdhikar { get; set; }
        public string RajaswaNyayalay { get; set; }
        public string OtherInfo { get; set; }
        public string SourceofIrrigation { get; set; }
        public string KhasraNo { get; set; }
        public string LandKhata { get; set; }
        public string Charge { get; set; }
        public decimal TotalArea { get; set; }
        public decimal IrrigatedArea { get; set; }
        public decimal UnIrrigatedArea { get; set; }
        public decimal NonAgriArea { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilCode { get; set; }
        public string VillageCode { get; set; }
        public string fbDistrictCode { get; set; }
        public string fbTehsilCode { get; set; }
        public string fbVillageCode { get; set; }
        public List<KCCCropDetailExisting> KCCCrops { get; set; }
    }
    public class KCCCropDetailExisting
    {
        [Key]
        public int Id { get; set; }
        public int KCCExistingLandId { get; set; }
        public string AccountNo { get; set; }
        public string Season { get; set; }
        public string Crop { get; set; }

        public decimal IrrigatedArea { get; set; }
        public decimal UnIrrigatedArea { get; set; }

        public decimal SOFIrrigatedArea { get; set; }
        public decimal SOFUnIrrigatedArea { get; set; }

        public string CropInsurance { get; set; }

        public KCCExistingLand KCCExistingLand { get; set; }
       

    }
    public class Payment
    {
        public int PaymentNumber { get; set; }
        public double Interest { get; set; }
        public double Principal { get; set; }
        public double RemainingBalance { get; set; }
    }
    public class Bhuiyan
    {
        public string Bhuswami { get; set; }
        public string LandDetails { get; set; }
        public string DharanAdhikar { get; set; }
        public string rajaswa { get; set; }
        public string Charge { get; set; }
        public string B1Signed { get; set; }
        public string B1Number { get; set; }

    }

    public class ApplicantBCDetail
    {
        public string ApplicantName { get; set; }
        public string DOB { get; set; }
        public string Designation { get; set; }
        public string Occupation { get; set; }
        public string WorkPlaceName { get; set; }
        public string WorkPlaceAddress { get; set; }
        public string PresentAddress { get; set; }
        public string WorkingSince { get; set; }

    }

    public class CropDetail
    {
        public string District { get; set; }
        public string Village { get; set; }
        public string Khasra { get; set; }
        public string Kharif { get; set; }
        public string Rabi { get; set; }
        public string Summer { get; set; }

        public float irrigateda { get; set; }
        public float unirrigateda { get; set; }

    }

    public class KCCElig
    {
        public string District { get; set; }
        public string Crop { get; set; }
        public string cropins { get; set; }
        public float sofirri { get; set; }
        public float sofunirri { get; set; }
        public float irrigateda { get; set; }
        public float unirrigateda { get; set; }
        public int irrigated { get; set; }
        public int unirrigated { get; set; }

    }

    public class UniqueCode
    {
        public readonly string BankIdRouteValue = "ShishirShuklaCRGB";
    }

    public class ApiUser
    {
        public string Status { get; set; }
        public  string UserId { get; set; }
        public string EmpName { get; set; }
        public string Designation { get; set; }
        public string Branch { get; set; }
    }

    public class CustomIDataProtection
    {
        private readonly IDataProtector protector;
        public CustomIDataProtection(IDataProtectionProvider dataProtectionProvider, UniqueCode uniqueCode)
        {
            protector = dataProtectionProvider.CreateProtector(uniqueCode.BankIdRouteValue);
        }
        public string Decode(string data)
        {
            return protector.Protect(data);
        }
        public string Encode(string data)
        {
            return protector.Unprotect(data);
        }
        
    }
    public class AppReport {
        public string AMHCode { get; set; }
        public int Pending { get; set; }
        public int Forwarded { get; set; }
        public int Sanctioned { get; set; }
    }
    public class SchemeReport
    {
        public string LoanScheme { get; set; }
        public int Pending { get; set; }
        public int Forwarded { get; set; }
        public int Sanctioned { get; set; }
        public int Pre_Sanction_done { get; set; }
    }
    public class ChartReport
    {
        public DateTime  date { get; set; }
        public int pl { get; set; }
        public int kcc { get; set; }
        public int car { get; set; }
        public int hl { get; set; }
        public int other { get; set; }
    }
    public class Cust360
    {
        public string ACCOUNT_NO { get; set; }
        public string TYPE { get; set; }
        public string PMJJBY { get; set; }
        public string PMSBY { get; set; }
        public string APY { get; set; }
        public string MB { get; set; }
        public string ATM { get; set; }
        public string SBIGPAI { get; set; }
        public string STATUS { get; set; }
        public string INB { get; set; }
       
    }

    public class KeyValue
    {
        public string code { get; set; }
        public string value { get; set; }
        
    }

    public class CIFData
    {
        public string cif { get; set; }
        public string cname { get; set; }
        public string father_name { get; set; }
        public string pan { get; set; }
        public string aadhaar { get; set; }
        public string dob { get; set; }
        public string add1 { get; set; }
        public string add2 { get; set; }
        public string add3 { get; set; }
        public string add4 { get; set; }
        public string pincode { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
    }
    public class AccountData
    {
        public string account_no { get; set; }
        public string acctopendate { get; set; }
        public string currentbalance { get; set; }
        public string odlimit { get; set; }
        public string accounttype { get; set; }
        public string interestcat { get; set; }
        public string intrate { get; set; }
        public string name { get; set; }
        public string cust_name { get; set; }

    }
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public string BranchId { get; set; }
        public string TypeOfApplicant { get; set; }
        public string ApplicantShortName { get; set; }
        public string LoanScheme { get; set; }
        public string Purpose { get; set; }
        public decimal SubsidyAmt{ get; set; }
        public decimal AppliedAmt { get; set; }
        public decimal Eligibility { get; set; }
        public string SponsoringAgency { get; set; }
        public string Owner { get; set; }
        public string OwnerUser { get; set; }
        public string SanctionedLevel { get; set; }
        public string ControlStatus { get; set; }
        public string ROIType { get; set; }
        public bool IsLead { get; set; }
        public string LeadStage { get; set; }
        public string  LeadSourcedBy { get; set; }
        public string LeadSourceAgency { get; set; }
       

        public string MappedTLAccount { get; set; }
        public string MappedCCAccount { get; set; }
        public string Remark { get; set; }

        public string ControlRemark { get; set; }

        public string InspectionAllotedTo { get; set; }
        public System.DateTime? InspectionAllotmentDate { get; set; }
        public string InspectionDone { get; set; }

        public string PendingAction { get; set; }

        public System.DateTime ApplicationDate { get; set; }
        public string Status { get; set; }
        public System.DateTime? ControlledDate { get; set; }
        public System.DateTime? SanctionedDate { get; set; }
        public System.DateTime? ForwardedDate { get; set; }
        public System.DateTime? SendToControlDate { get; set; }
        public System.DateTime? ReviewDate { get; set; }
        public string SanctioningUserId { get; set; }
        public string SanctioningLevel { get; set; }

        public Branch Branch { get; set; }
        public List<AccountStatments> AccountStatments { get; set; }
        public List<LoanApplication> LoanApplications { get; set; }
        public List<Applicant> Applicants { get; set; }
        public List<Security> Securities { get; set; }
        public List<Disbursement> Disbursements { get; set; }
        public List<Document> Documents { get; set; }
        public List<Log> Logs { get; set; }
        public List<PreInspection> Inspections { get; set; }
        public List<Charge> Charges { get; set; }
        public List<Remark> Remarks { get; set; }
        public List<ProjectCost> ProjectCost { get; set; }
        public List<KCCLandDetail> KCCLandDetails { get; set; }
        public List<KCCCropDetail> KCCCropDetails { get; set; }
        public List<MudraDetail> MudraDetails { get; set; }
        public List<Mandate> Mandates { get; set; }
        public ControlInformation ControlInformations { get; set; }
        public TPLDetail TPLDetails { get; set; }

        public KCCRenewal Renewal { get; set; }

    }

    public class ProjectCost
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Description { get; set; }
        public string EligibleForLoan { get; set; }
        public decimal Cost { get; set; }
        public decimal AppliedAmount { get; set; }
        public Application ApplicationDetail { get; set; }
    }

    public class MandateResponse
    {
        public string status { get; set; }
        public string api_response_id { get; set; }
        public string reference_id { get; set; }
        public string error { get; set; }
        public string error_code { get; set; }
        public string emandate_id { get; set; }
        public DateTime response_time_stamp { get; set; }
        public string email_trigger { get; set; }
        public string sms_trigger { get; set; }
    }
    public class Mandate
{
        public Mandate()
        {
            reference_id = Guid.NewGuid().ToString().Replace("-","");
            occurance_sequence_type = "RCUR";
            occurance_frequency_type = "MNTH";
            
           
            collection_amount_type = "FIXED";
            
            mandate_type_category_code = "L001";
            is_until_cancel = true;
            quick_invite = true;
            authentication_mode = "netBanking";
           
        }
        [Key]
        [JsonIgnore]
        public int Id { get; set; } 

        [JsonIgnore]
        public int ApplicationId { get; set; }
        [JsonIgnore]
        public int ApplicantId { get; set; }
        public string reference_id { get; set; }
        public string debtor_account_type { get; set; }
        public string debtor_account_id { get; set; }
        public string occurance_sequence_type { get; set; }
        public string occurance_frequency_type { get; set; }
        public string scheme_reference_number { get; set; }
        public string consumer_reference_number { get; set; }
        public string debtor_name { get; set; }
        public string email_address { get; set; }
        public string first_collection_date { get; set; }
        public string mobile_number { get; set; }
        public string phone_number { get; set; }
        public string collection_amount_type { get; set; }
        public int amount { get; set; }
        public string mandate_type_category_code { get; set; }
        public bool is_until_cancel { get; set; }
        public bool quick_invite { get; set; }
        public string authentication_mode { get; set; }
        public string instructed_agent_code { get; set; }
        [JsonIgnore]
        public DateTime createDate { get; set; }
        [JsonIgnore]
        public string api_response_id { get; set; }
        [JsonIgnore]
        public string emandate_id { get; set; }
        [JsonIgnore]
        public DateTime? last_run_date { get; set; }
        [JsonIgnore]
        public string last_run_status { get; set; }
        [JsonIgnore]
        public string is_cancelled { get; set; }
        [JsonIgnore]
        public string mandate_status { get; set; }
        [JsonIgnore]
        public string umrn_mandate { get; set; }
        [JsonIgnore]
        public Application ApplicationDetail { get; set; }
    }

    public class ControlInformation
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ReceivedDocuments { get; set; }
        public int EligibleAmt { get; set; }
        public string Statement1 { get; set; }
        public string Statement2 { get; set; }
        public string Statement3 { get; set; }
        public string Statement4 { get; set; }
        public string Statement5 { get; set; }
        public string Statement6 { get; set; }
        public string Statement7 { get; set; }
        public string Statement8 { get; set; }
        public string Statement9 { get; set; }
       
        public string Statement10 { get; set; }
        public string Statement11 { get; set; }
        public string Statement12 { get; set; }

        public string Statement13 { get; set; }
        public string Statement14 { get; set; }
        public string Statement15 { get; set; }

        public string Statement16 { get; set; }
        public string Statement17 { get; set; }
        public decimal AppliedAmt { get; set; }
        public decimal SanctionedTL { get; set; }
        public decimal SanctionedCC { get; set; }

        public Application ApplicationDetail { get; set; }
    }



    public class KCCRenewal
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string CreditBalanceDate { get; set; }
        public string CBSSancDate { get; set; }
        public int SanctionedLimit { get; set; }
        public int AppliedAmt { get; set; }
        public string AcNumber { get; set; }
        public string Product { get; set; }
        public string RenFY { get; set; }
        public DateTime AcOpenDate { get; set; }
        public int CBSLimit { get; set; }

        public Application ApplicationDetail { get; set; }
    }

    public class LoanApplication
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Description { get; set; }
        public string TypeOfFacility { get; set; }
        public decimal Cost { get; set; }
        public decimal AppliedAmount { get; set; }
        public decimal SanctionedAmount { get; set; }
        public decimal RateOfInterest { get; set; }
        public decimal Spread { get; set; }
        public decimal PLR { get; set; }
        public decimal Repayment { get; set; }
        public decimal RepayTerm { get; set; }
        public string Moratorium { get; set; }
        public string RepayStartDate { get; set; }
        public string RenewalDate { get; set; }
        public string UtilizationPeriod { get; set; }
        public string LoanAccountNumber { get; set; }
        public Application ApplicationDetail { get; set; }
    }

    public class CIBILRequest
    {
        [Key]
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string RequestDate { get; set; }
        public string CiibilControlNumber { get; set; }
        public string ScoreName1 { get; set; }
        public string ScoreName2 { get; set; }
        public string ScoreName3 { get; set; }
        public string Score1 { get; set; }
        public string Score2 { get; set; }
        public string Score3 { get; set; }
        public string ResponseFileName { get; set; }
        public Applicant ApplicantDetail { get; set; }
        public List<CIBILLoanInfo> CibilLoans { get; set; }
    }

    public class CIBILLoanInfo
    {
        [Key]
        public int Id { get; set; }
        public int CIBILRequestId { get; set; }
        public string index { get; set; }
        public string memberShortName { get; set; }
        public string accountType { get; set; }
        public string ownershipIndicator { get; set; }
        public string dateOpened { get; set; }
        public string dateClosed { get; set; }
        public string dateReported { get; set; }
        public string highCreditAmount { get; set; }
        public string currentBalance { get; set; }
        public string amountOverdue { get; set; }
        public string paymentHistory { get; set; }
        public string paymentStartDate { get; set; }
        public string paymentEndDate { get; set; }
        public string collateralType { get; set; }
        public string errorCode { get; set; }
        public string cibilRemarksDate { get; set; }
        public string cibilRemarksCode { get; set; }
        public string errorRemarksCode1 { get; set; }
        public string errorRemarksCode2 { get; set; }
        public string lastPaymentDate { get; set; }
        public string creditLimit { get; set; }
        public string EmployeeResponse { get; set; }
        public string UserId { get; set; }
        public int EMIConsidered { get; set; }
        public CIBILRequest CIBILRequestDetail { get; set; }
    }

    public class CibilEnquiry
    {
        [Key]
        public int Id { get; set; }
        public int CIBILRequestId { get; set; }
        public string index { get; set; }
        public string enquiryDate { get; set; }
        public string memberShortName { get; set; }
        public string enquiryPurpose { get; set; }
        public string enquiryAmount { get; set; }
        
        public CIBILRequest CIBILRequestDetail { get; set; }
    }

    public class Log
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
        public string IpAddress { get; set; }
        public string Mode { get; set; }
        public DateTime Timestamp { get; set; }
        
    }
    public class Remark
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string TypeOfRemark { get; set; }
        public string TypeOfRecomendation { get; set; }
        public string Designation { get; set; }
        public string EmpName { get; set; }
        public string UserId { get; set; }
        public string SenderUserId { get; set; }
        public string Statment { get; set; }
        public string SpecialRemark { get; set; }
        public string DataType { get; set; }
        public DateTime Timestamp { get; set; }

    }
    public class Security
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string TypeOfSecurity { get; set; }
        public string TypeOfCharge { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Boundary { get; set; }
        public string Owner { get; set; }
        public string PlotNo { get; set; }
        public string Advocate1 { get; set; }
        public string Advocate2 { get; set; }
        public string Valuer1 { get; set; }
        public string Valuer2 { get; set; }

        public string DateTSR1 { get; set; }
        public string DateTSR2 { get; set; }

        public string DetailsofTSR1 { get; set; }
        public string DetailsofTSR2 { get; set; }
        public string DateVal1 { get; set; }
        public string DateVal2 { get; set; }
        public decimal GovtVal1 { get; set; }
        public decimal MarketVal1 { get; set; }
        public decimal DistressVal1 { get; set; }
        public decimal GovtVal2 { get; set; }
        public decimal MarketVal2 { get; set; }
        public decimal DistressVal2 { get; set; }



        public decimal Valuation { get; set; }
        
        public decimal RateOfInterest { get; set; }
        public decimal DocumentDetails { get; set; }
        public Application ApplicationDetail { get; set; }
    }
    public class KCCLandDetail
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string RI { get; set; }
        public string Village { get; set; }
        public string OwnerName { get; set; }
        public string DharanAdhikar { get; set; }
        public string RajaswaNyayalay { get; set; }
        public string OtherInfo { get; set; }
        public string SourceofIrrigation { get; set; }
        public string KhasraNo { get; set; }
        public string Charge { get; set; }
        public decimal TotalArea { get; set; }
        public decimal IrrigatedArea { get; set; }
        public decimal UnIrrigatedArea { get; set; }
        public decimal NonAgriArea { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilCode { get; set; }
        public string VillageCode { get; set; }
        public Application ApplicationDetail { get; set; }
    }
    public class TPLDetail
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string TypeOfVehichle { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string ReferenceUnit { get; set; }
        public decimal Mileage { get; set; }
        public int FuelCost { get; set; }
        public int DepriciationRate { get; set; }
        public int TripsPerDay { get; set; }
        public int UsePerTrip { get; set; }
        public int ReceiptPerTrip { get; set; }
        public int WorkingDaysInMonth { get; set; }
        public int ServiceCost { get; set; }
        public int TireReplacementPeriod { get; set; }
        public int TireReplacementCost { get; set; }
        public int OtherCost { get; set; }
        public int Insurance { get; set; }
        public int Taxes { get; set; }
        public int DriverCost { get; set; }

        public Application ApplicationDetail { get; set; }
    }

    public class MudraDetail
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Parameter { get; set; }
        public decimal PrevYear { get; set; }
        public decimal CurrentYear { get; set; }
        public decimal Estimate1 { get; set; }
        public decimal Estimate2 { get; set; }
        public string ParameterType { get; set; }
        public string ParameterClass { get; set; }

        public Application ApplicationDetail { get; set; }
    }

    public class KCCCropDetail
    {
        [Key]
        public int Id { get; set; }
        public int KCCLandDetailId { get; set; }
        public int ApplicationId { get; set; }
        public string Season { get; set; }
        public string Crop { get; set; }
        
        public decimal IrrigatedArea { get; set; }
        public decimal UnIrrigatedArea { get; set; }

        public decimal SOFIrrigatedArea { get; set; }
        public decimal SOFUnIrrigatedArea { get; set; }

        public string CropInsurance { get; set; }

        public KCCLandDetail KCCLandDetails { get; set; }
        public Application ApplicationDetail { get; set; }

    }
    public class Disbursement
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Details { get; set; }
       
        public decimal TotalAmount { get; set; }
        public decimal BankPart { get; set; }
        
        public Application ApplicationDetail { get; set; }
    }
    public class Charge
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Details { get; set; }
        public string Frequency { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Percentage { get; set; }

        public Application ApplicationDetail { get; set; }
    }
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Details { get; set; }

        public string FilePath { get; set; }
        public string UserId { get; set; }
        public System.DateTime UploadDate { get; set; }


        public Application ApplicationDetail { get; set; }
    }
    public class DocumentLoan
    {
        [Key]
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string Details { get; set; }

        public string FilePath { get; set; }
        public string UserId { get; set; }
        public System.DateTime UploadDate { get; set; }


        public Application ApplicationDetail { get; set; }
    }

    public class ExistingApplicant
    {
        [Key]
        public int Id { get; set; }
        public string AccountInfoId { get; set; }
        public string ExistingCIF { get; set; }
        public string Salutation { get; set; }
        public string MaritialStatus { get; set; }

        public string Applicant_First_Name { get; set; }
        public string Applicant_Middle_Name { get; set; }
        public string Applicant_Last_Name { get; set; }
        public string Relationship { get; set; }
        public string Guardian_Name { get; set; }
        public string Gender { get; set; }
        public string PAN_No { get; set; }
        public string Adhaar_No { get; set; }
        public string Voter_Id { get; set; }

        public string ElectionId { get; set; }
        public string Driving_Lic { get; set; }
        public string Passport { get; set; }
        public string Date_Of_Birth { get; set; }

        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string PermanentAddress { get; set; }
        public string WorkPlaceAddress { get; set; }
        public string City_Village { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PINCode { get; set; }
        public string Occupation { get; set; }

        public string WorkPlace { get; set; }
        public string Dependent { get; set; }
        public string WorkingSince { get; set; }
        public string Education { get; set; }

        public string LatestIncome { get; set; }
        public string RelationWithAppl { get; set; }

        public string PrimaryBankingRelation { get; set; }
        public string AccountNumber { get; set; }

        public string NetWorth { get; set; }

        public int NMI { get; set; }
        public int GMI { get; set; }
        public int AddlIncome { get; set; }
        public string RemarkOnNMI { get; set; }
        public string RemarkOnAddlIncome { get; set; }
        public string RemarkOther { get; set; }
        public string Caste { get; set; }
        public string Religion { get; set; }
        public string TypeofApplicant { get; set; }
        public string DocumentExecution { get; set; }
        public string CreditScore { get; set; }
        public string MobileNumber { get; set; }

        public AccountInfo Account { get; set; }
        public List<KYCInfoExisting> ExistingKYCs { get; set; }
    }

    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ExistingCIF { get; set; }
        public string Salutation { get; set; }
        public string MaritialStatus { get; set; }

        public string Applicant_First_Name { get; set; }
        public string Applicant_Middle_Name { get; set; }
        public string Applicant_Last_Name { get; set; }
        public string Relationship { get; set; }
        public string Guardian_Name { get; set; }
        public string Gender { get; set; }
        public string PAN_No { get; set; }
        public string Adhaar_No { get; set; }
        public string Voter_Id { get; set; }

        public string ElectionId { get; set; }
        public string Driving_Lic { get; set; }
        public string Passport { get; set; }
        public string Date_Of_Birth { get; set; }
        
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string PermanentAddress { get; set; }
        public string WorkPlaceAddress { get; set; }
        public string City_Village { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PINCode { get; set; }
        public string Occupation { get; set; }

        public string WorkPlace { get; set; }
        public string Dependent { get; set; }
        public string WorkingSince { get; set; }
        public string Education { get; set; }

        public string LatestIncome { get; set; }
        public string RelationWithAppl { get; set; }

        public string PrimaryBankingRelation { get; set; }
        public string AccountNumber { get; set; }

        public string NetWorth { get; set; }

        public int NMI { get; set; }
        public int GMI { get; set; }
        public int AddlIncome { get; set; }
        public string RemarkOnNMI { get; set; }
        public string RemarkOnAddlIncome { get; set; }
        public string RemarkOther { get; set; }
        public string Caste { get; set; }
        public string Religion { get; set; }
        public string TypeofApplicant { get; set; }
        public string DocumentExecution { get; set; }
        public string KYC_Upload { get; set; }
        public string MobileNumber { get; set; }
        public List<CIBILRequest> CibilRequests { get; set; }
        public List<KYCInfo> KYCInfos { get; set; }

    }

}
