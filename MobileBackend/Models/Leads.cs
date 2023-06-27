using System;
using System.Collections.Generic;

namespace MobileBackend.Models
{
    public class CustomerMaster
    {
        public string  FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PAN { get; set; }
        public string Adhaar { get; set; }
        public string Mobile { get; set; }
        public DateTime DOB  { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }


    }
    public class LeadCollection
    {
        public List<KCCLeads> KCCLead { get; set; }
        public List<TPLLeads> TPLLeads { get; set; }
        public List<GoldLoanLeads> GoldLoanLeads { get; set; }
        public List<GeneralLeads> GeneralLeads { get; set; }    
    }
    public class Leads
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public string AddressLine1 { get; set; }
        public string MobileNumber { get; set; }
        public DateTime LeadDate { get; set; }
        public string SourcedBy { get; set; }
        public string SourcingAgency { get; set; }
        public bool ExistingCustomer { get; set; }

        public string PrimaryBank { get; set; }

        public string LeadStatus { get; set; }
        public DateTime DOB { get; set; }
        public string PinCode { get; set; }
        public string  City { get; set; }
        public string PANNo { get; set; }
        public string AadhaarNo { get; set; }
        public string ElectionId { get; set; }
        public string OtherIncomeSource { get; set; }
        public string OtherEarningMember  { get; set; }
        public string DetailsofEarningMember { get; set; }

        public string UploadPAN { get; set; }
        public string UploadAadhaar { get; set; }

        public string UploadElectionId { get; set; }

        public string UploadPhoto { get; set; }

        public string UploadDocument1 { get; set; }
        public string UploadDocument2 { get; set; }
        public string BranchId { get; set; }

        public int AppliedAmt { get; set; }
        public List<LeadRoadMap> Comments { get; set; }
        public Branch Branch { get; set; }
    }

    public class GeneralLeads
    {
        public int Id { get; set; }
        public string ApplicantName { get; set; }
        public string AddressLine1 { get; set; }
        public string MobileNumber { get; set; }
        public DateTime LeadDate { get; set; }
        public string SourcedBy { get; set; }
        public string SourcingAgency { get; set; }
        public bool ExistingCustomer { get; set; }
        public string PrimaryBank { get; set; }

        public string LeadStatus { get; set; }
        public DateTime DOB { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }
        public string PANNo { get; set; }
        public string AadhaarNo { get; set; }
        public string ElectionId { get; set; }
        public string OtherIncomeSource { get; set; }
        public string OtherEarningMember { get; set; }
        public string DetailsofEarningMember { get; set; }

        public string UploadPAN { get; set; }
        public string UploadAadhaar { get; set; }

        public string UploadElectionId { get; set; }

        public string UploadPhoto { get; set; }

        public string UploadDocument1 { get; set; }
        public string UploadDocument2 { get; set; }
        public string BranchId { get; set; }

        public string LoanScheme { get; set; }
        public string LoanPurpose { get; set; }

        public int AppliedAmt { get; set; }

        public int OtherInfo1 { get; set; }
        public int OtherInfo2 { get; set; }

        public string RefNoCRGB { get; set; }


        public List<LeadRoadMapGen> Comments { get; set; }
        public Branch Branch { get; set; }
    }

    public class LeadRoadMapGen
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public string EntryType { get; set; }
        public string UserId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Remark { get; set; }
        public string Attachement { get; set; }
        public GeneralLeads LeadDetails { get; set; }
    }

    public class LeadRoadMap
    {
        public int Id { get; set; }
        public int LeadId { get; set; }
        public string EntryType { get; set; }
        public string UserId { get; set; }
        public DateTime ActionDate { get; set; }
        public string Remark { get; set; }
        public string Attachement { get; set; }
        public Leads LeadDetails { get; set; }
    }
    public class KCCLeads : Leads
    {
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string RI { get; set; }

        public string Village { get; set; }
        public string  KhasraNo { get; set; }
        public string IrrigationSource { get; set; }
    }
    public class TPLLeads : Leads
    {
        public string TypeOfVehichle { get; set; }
        public string Make { get; set; }
        public string PastExperience { get; set; }

        
    }
    public class GoldLoanLeads : Leads
    {
        public string AnyOtherLoan { get; set; }
        


    }
    
}
