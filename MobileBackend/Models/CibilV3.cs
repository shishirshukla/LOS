using System.Collections.Generic;

namespace MobileBackend.Models.Cibil3
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        public string AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string AddressLine5 { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string ResidenceType { get; set; }
        public string StateCode { get; set; }
    }

    public class Addresses
    {
        public Address Address { get; set; }
    }

    public class Applicant
    {
        public string ApplicantFirstName { get; set; }
        public object ApplicantMiddleName { get; set; }
        public string ApplicantLastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
       // public Identifiers Identifiers { get; set; }
       // public Telephones Telephones { get; set; }
      //  public Addresses Addresses { get; set; }
        public Services Services { get; set; }
        public string ApplicantIdentifier { get; set; }
    }

    public class Applicants
    {
        public Applicant Applicant { get; set; }
    }

    public class ApplicationData
    {
        public string GSTStateCode { get; set; }
        public Services Services { get; set; }
        public string GoSolutionSetId { get; set; }
        public string GoSolutionSetVersion { get; set; }
    }

    public class Attribute
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }

    public class AvailableFlags
    {
        public Telephone2Category Telephone2Category { get; set; }
        public string Telephone1Category { get; set; }
        public string Address2Category { get; set; }
        public string Address1Category { get; set; }
        public string TelOfficeInfo { get; set; }
        public string TelResidantInfo { get; set; }
        public string TelMobileInfo { get; set; }
        public string AddrOfficeInfo { get; set; }
        public string AddrResidantInfo { get; set; }
        public string AddrPermanentInfo { get; set; }
        public string IDAadharInfo { get; set; }
        public string IDRationCardInfo { get; set; }
        public string IDDriverLicInfo { get; set; }
        public string IDVoterInfo { get; set; }
        public string IDPassportInfo { get; set; }
        public string IDNSDLInfo { get; set; }
    }

    public class CIBILReport
    {
        public Telephones Telephones { get; set; }
        public Identifiers Identifiers { get; set; }
        public string FID { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string EnquiryControlNumber { get; set; }
        public string ConsumerName { get; set; }
    }

    public class CPVAttributes
    {
        public Match Match { get; set; }
        public EnquiryInfo EnquiryInfo { get; set; }
        public CIBILReport CIBILReport { get; set; }
        public VerificationScore VerificationScore { get; set; }
        public Velocity Velocity { get; set; }
        public ECR ECR { get; set; }
        public WilfulDefaultDetails WilfulDefaultDetails { get; set; }
        public MemberAttributes MemberAttributes { get; set; }
        public AvailableFlags AvailableFlags { get; set; }
    }

    public class Data
    {
        public Response Response { get; set; }
    }

    public class DateReported
    {
    }

    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DsIDVision
    {
       // public CPVAttributes CPVAttributes { get; set; }
        public string ReturnMessage { get; set; }
        public string IDVApplicationID { get; set; }
        public string SpecialMessages { get; set; }
        public Document Document { get; set; }
    }

    public class ECR
    {
        public ProductInfo1month ProductInfo1month { get; set; }
        public ProductInfo3months ProductInfo3months { get; set; }
        public ProductInfo6months ProductInfo6months { get; set; }
    }

    public class EmailID
    {
    }

    public class EnquiryInfo
    {
        public string MobileNumber { get; set; }
        public string PermanentAddressCity { get; set; }
        public string PermanentAddressState { get; set; }
        public string PermanentAddressStateCode { get; set; }
        public string PermanentAddressPinCode { get; set; }
        public string PermanentAddressLine { get; set; }
        public UID UID { get; set; }
        public string PAN { get; set; }
        public EmailID EmailID { get; set; }
        public string DateofBirth { get; set; }
        public string Gender { get; set; }
        public string LastName { get; set; }
        public object MiddleName { get; set; }
        public string FirstName { get; set; }
    }

    public class ExceptionMessage
    {
    }

    public class Fields
    {
        public Applicants Applicants { get; set; }
        //public ApplicationData ApplicationData { get; set; }
        public string Decision { get; set; }
        public string ApplicationId { get; set; }
    }

    public class HRAAttributes
    {
        public List<Attribute> Attribute { get; set; }
    }

    public class HRARuleAttributes
    {
        public List<Attribute> Attribute { get; set; }
    }

    public class Identifier
    {
        public string IdNumber { get; set; }
        public string IdType { get; set; }
        public DateReported DateReported { get; set; }
        public string EnrichedThroughEnquiry { get; set; }
        public string FID { get; set; }
        public string SNo { get; set; }
    }

    public class Identifiers
    {
        public List<Identifier> Identifier { get; set; }
    }

    public class Match
    {
        public string ContactabilityAadhaarTelephone1Match { get; set; }
        public string AddressAadhaarPermanentMatch { get; set; }
        public string AddressAadhaarResidenceMatch { get; set; }
        public string IDAadhaarIdentifierMatch { get; set; }
        public string IDAadharGenderMatch { get; set; }
        public string IDAadharDOBYearMatch { get; set; }
        public string IDAadharNameMatch { get; set; }
        public string AddressVoterPermanentMatch { get; set; }
        public string AddressVoterResidenceMatch { get; set; }
        public string IDVoterIdentifierMatch { get; set; }
        public string IDVoterGenderMatch { get; set; }
        public string IDVoterNameMatch { get; set; }
        public string IDNSDLIdentifierMatch { get; set; }
        public string IDNSDLNameMatch { get; set; }
        public string ContactabilityOfficeNumberMatch { get; set; }
        public string ContactabilityResidenceNumberMatch { get; set; }
        public string ContactabilityMobileNumberMatch { get; set; }
        public string AddressCIBILOfficeMatch { get; set; }
        public string AddressCIBILPermanentMatch { get; set; }
        public string AddressCIBILResidenceMatch { get; set; }
        public string IDCIBILDrivingLicenseIDMatch { get; set; }
        public string IDCIBILRationCardIDMatch { get; set; }
        public string IDCIBILPassportIDMatch { get; set; }
        public string IDCIBILAadhaarIDMacth { get; set; }
        public string IDCIBILVoterIDMatch { get; set; }
        public string IDCIBILPANMatch { get; set; }
        public string IDCIBILGenderMatch { get; set; }
        public string IDCIBILDOBMatch { get; set; }
        public string IDCIBILNameMatch { get; set; }
        public ExceptionMessage ExceptionMessage { get; set; }
    }

    public class MemberAttributes
    {
        public string AppEnvironment { get; set; }
        public string ExternalApplicationId { get; set; }
        public string GSTStateCode { get; set; }
        public string IDVision_EndTime { get; set; }
        public int ApplicationId { get; set; }
        public string MemberID { get; set; }
        public string ShortName { get; set; }
        public string CreatedUser { get; set; }
    }

    public class Operation
    {
        public string Name { get; set; }
        public Params Params { get; set; }
        public string Id { get; set; }
        public Data Data { get; set; }
        public string Status { get; set; }
    }

    public class Operations
    {
        public List<Operation> Operation { get; set; }
    }

    public class Param
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Params
    {
        public List<Param> Param { get; set; }
    }

    public class ProductInfo1month
    {
        public string NoOfEnquiries { get; set; }
        public string ProductType { get; set; }
        public string ECRValue { get; set; }
        public string NoOfTradeLinesOpened { get; set; }
    }

    public class ProductInfo3months
    {
        public string NoOfEnquiries { get; set; }
        public string ProductType { get; set; }
        public string ECRValue { get; set; }
        public string NoOfTradeLinesOpened { get; set; }
    }

    public class ProductInfo6months
    {
        public string NoOfEnquiries { get; set; }
        public string ProductType { get; set; }
        public string ECRValue { get; set; }
        public string NoOfTradeLinesOpened { get; set; }
    }

    public class RawResponse
    {
        public string BureauResponseXml { get; set; }
        public string SecondaryReportXml { get; set; }
        public Document Document { get; set; }
        public DsIDVision DsIDVision { get; set; }
        public HRAAttributes HRAAttributes { get; set; }
        public HRARuleAttributes HRARuleAttributes { get; set; }
    }

    public class Response
    {
        public RawResponse RawResponse { get; set; }
    }

    public class ResponseInfo
    {
        public int ApplicationId { get; set; }
        public string SolutionSetInstanceId { get; set; }
    }

    public class Root
    {
        public string Status { get; set; }
        public ResponseInfo ResponseInfo { get; set; }
        public Fields Fields { get; set; }
    }

    public class Service
    {
        public string Id { get; set; }
        public Operations Operations { get; set; }
        public object Skip { get; set; }
        public object Consent { get; set; }
        public object EnableSimulation { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
    }

    public class Services
    {
        public List<Service> Service { get; set; }
    }

    public class SuitFiled
    {
        public string Status { get; set; }
    }

    public class SuitfiledandWilfuldefault
    {
        public string Status { get; set; }
    }

    public class Telephone
    {
        public string TelephoneNumber { get; set; }
        public string TelephoneType { get; set; }
        public string TelephoneCountryCode { get; set; }
        public DateReported DateReported { get; set; }
        public string EnrichedThroughEnquiry { get; set; }
        public Match Match { get; set; }
        public string SegmentTag { get; set; }
        public string FID { get; set; }
        public string SNo { get; set; }
    }

    public class Telephone2Category
    {
    }

    public class Telephones
    {
        public Telephone Telephone { get; set; }
    }

    public class UID
    {
    }

    public class Velocity
    {
        public ProductInfo1month ProductInfo1month { get; set; }
        public ProductInfo3months ProductInfo3months { get; set; }
        public ProductInfo6months ProductInfo6months { get; set; }
        public string TotalAccount { get; set; }
        public string TotalEnquiry { get; set; }
    }

    public class VerificationScore
    {
        public double IDNameScore { get; set; }
        public object IDNameStatus { get; set; }
        public double IDAltNameScore { get; set; }
        public double IDAltDOBScore { get; set; }
        public double IDDOBScore { get; set; }
        public object IDDOBStatus { get; set; }
        public double IDAltGenderScore { get; set; }
        public double IDGenderScore { get; set; }
        public object IDGenderStatus { get; set; }
        public double IDAltIdentifierScore { get; set; }
        public double IDIdentifierScore { get; set; }
        public object IDIdentifierStatus { get; set; }
        public double AddAltResScore { get; set; }
        public double AddressResidenceScore { get; set; }
        public object AddressResidenceStatus { get; set; }
        public double AddAltPerScore { get; set; }
        public double AddressPermanentScore { get; set; }
        public object AddressPermanentStatus { get; set; }
        public double AddressOfficeScore { get; set; }
        public object AddressOfficeStatus { get; set; }
        public double ConAltPhoneScore { get; set; }
        public double ContactabilityTelephone1Score { get; set; }
        public object ContactabilityTelephone1Status { get; set; }
        public double ContactabilityTelephone2Score { get; set; }
        public object ContactabilityTelephone2Status { get; set; }
        public double ContactabilityTelephone3Score { get; set; }
        public object ContactabilityTelephone3Status { get; set; }
        public double FinalIdentityScore { get; set; }
        public object FinalIdentityStatus { get; set; }
        public double FinalAddressScore { get; set; }
        public object FinalAddressStatus { get; set; }
        public double FinalContactabilityScore { get; set; }
        public object FinalContactabilityStatus { get; set; }
        public double FinalVerificationScore { get; set; }
        public object FinalVerificationStatus { get; set; }
    }

    public class WilfulDefault
    {
        public string Status { get; set; }
    }

    public class WilfulDefaultDetails
    {
        public SuitfiledandWilfuldefault SuitfiledandWilfuldefault { get; set; }
        public WilfulDefault WilfulDefault { get; set; }
        public SuitFiled SuitFiled { get; set; }
    }


}
